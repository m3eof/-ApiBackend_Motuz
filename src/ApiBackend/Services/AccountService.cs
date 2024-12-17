using ApiBackend.Models.Accounts;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ApiBackend.Authorization;
using ApiBackend.Models.Accounts;
using ApiBackend.Helpers;
using ApiBackend.Models;
using System.Security.Cryptography;


namespace ApiBackend.Services
{
    public class AccountsService : IAccountService
    {
        private readonly recensiiContext _repositoryWrapper;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;

        public AccountsService(
            recensiiContext repositoryWrapper,
            IJwtUtils jwtUtils,
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            IEmailService emailService)
        {
            _repositoryWrapper = repositoryWrapper;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _emailService = emailService;
        }

        private void removeOldRefreshTokens(User account)
        {
            account.RefreshTokens.RemoveAll(x =>
            !x.IsActive && 
            x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var account = await _repositoryWrapper.Users.Include(x => x.ResetToken).AsNoTracking().FirstOrDefaultAsync(x => x.Email == model.Email);

            if (account == null || !account.IsVerified || !BCrypt.Net.BCrypt.Verify(model.Password, account.UserPassword))
                throw new AppException("Email or password is incorrect");
            var jwtToken = _jwtUtils.GenerateJwtToken(account);
           
            var refreshToken = await _jwtUtils.GenerateRefreshToken(ipAddress);
            account.RefreshTokens.Add(refreshToken); // Теперь всё в порядке

            removeOldRefreshTokens(account);


            _repositoryWrapper.Users.Update(account);
            await _repositoryWrapper.SaveChangesAsync();

            var response = _mapper.Map<AuthenticateResponse>(account);
            response.JwtToken = jwtToken;
            response.RefreshToken = refreshToken.Token;
            return response;
        }

        public async Task<AccountResponse> Create(CreateRequest model)
        {
            //if (( await _repositoryWrapper.Users.FindByCondition(x => x.Email == model.Email)).Count > 0) throw new AppException("Email' {model.Email} 'is already registered");

            //var account = _mapper.Map<User>(model);
            //account.Created = DateTime.UtcNow;
            //account.Verified = DateTime.UtcNow;

            //account.UserPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // _repositoryWrapper.Users.Update(account);
            //await _repositoryWrapper.SaveChangesAsync();

            //return _mapper.Map<AccountResponse>(account);

            var existingUserCount = await _repositoryWrapper.Users
                .Where(x => x.Email == model.Email) // Фильтруем по условию
                .CountAsync(); 

            if (existingUserCount > 0)
            {
                throw new AppException($"Email '{model.Email}' is already registered");
            }

            var account = _mapper.Map<User>(model);
            account.Created = DateTime.UtcNow;
            account.Verified = DateTime.UtcNow;

            account.UserPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            _repositoryWrapper.Users.Update(account);
            await _repositoryWrapper.SaveChangesAsync();

            return _mapper.Map<AccountResponse>(account);
        }

        public async Task Delete(int id)
        {
           var account = await getAccount(id);
            _repositoryWrapper.Users.Remove(account);
            await _repositoryWrapper.SaveChangesAsync();
        }

        private async Task<string> generateResetToken()
        {
            //var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
            //var tokenIsUnique = (await _repositoryWrapper.Users.FindByCondition(x => x.ResetToken == token)).Count == 0;
            //if (!tokenIsUnique)
            //    return await generateResetToken();

            //return token;

            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
            var tokenIsUnique = !(await _repositoryWrapper.Users
                .AnyAsync(x => x.ResetToken == token)); 

            if (!tokenIsUnique)
                return await generateResetToken(); 

            return token; 
        }

        public async Task ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            //var account = (await _repositoryWrapper.Users.FindByCondition(x => x.Email == model.Email)).FirstOrDefault();

            //if (account == null) return;

            //account.ResetToken = await generateResetToken();
            //account.ResetTokenExpires = DateTime.UtcNow.AddDays(1);

            //_repositoryWrapper.Users.Update(account);
            //await _repositoryWrapper.SaveChangesAsync();

            var account = await _repositoryWrapper.Users
        .FirstOrDefaultAsync(x => x.Email == model.Email); // Асинхронно ищем учетную запись по email

            if (account == null) return; // Если учетная запись не найдена, выходим из метода

            account.ResetToken = await generateResetToken(); // Генерируем уникальный токен сброса пароля
            account.ResetTokenExpires = DateTime.UtcNow.AddDays(1); // Устанавливаем время истечения токена

            _repositoryWrapper.Users.Update(account); // Обновляем учетную запись
            await _repositoryWrapper.SaveChangesAsync(); // Сохраняем изменения в базе данных

        }

        public async Task<IEnumerable<AccountResponse>> GetAll()
        {
            var accounts = await _repositoryWrapper.Users.ToListAsync();
            return _mapper.Map<IList<AccountResponse>>(accounts);
        }

        public async Task<AccountResponse> GetById(int id)
        {
            var account = await getAccount(id);
            return _mapper.Map<AccountResponse>(account);
        }

        public Task<AuthenticateResponse> RefreshToken(string token, string ipAddress)
        {
            throw new NotImplementedException();
        }

        public Task Register(RegisterRequest model, string origin)
        {
            throw new NotImplementedException();
        }

        public Task ResetPassword(ResetPasswordRequest model)
        {
            throw new NotImplementedException();
        }

        public Task RevokeToken(string token, string ipAddress)
        {
            throw new NotImplementedException();
        }

        private async Task<User> getAccount(int id)
        {
            //     var account = await _repositoryWrapper.Users
            //.FirstOrDefaultAsync(x => x.UsersId == account.UsersId); // Асинхронно ищем учетную запись по email
            //var account = await _repositoryWrapper.Users.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
            var account = await getAccount(id);
            if (account == null) throw new KeyNotFoundException("Account not found");
            return account;
        }

        public async Task<AccountResponse> Update(int id, UpdateRequest model)
        {
           var account = await getAccount(id);

            var existingUserCount = await _repositoryWrapper.Users
                 .Where(x => x.Email == model.Email) // Фильтруем по условиюs
                 .CountAsync();

            if (existingUserCount > 0)
            {
                throw new AppException($"Email '{model.Email}' is already registered");
            }

            if (!string.IsNullOrEmpty(model.Password))
                account.UserPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            _mapper.Map(model, account);
            account.Updated = DateTime.UtcNow;
             _repositoryWrapper.Users.Update(account);
            await _repositoryWrapper.SaveChangesAsync();

            return _mapper.Map<AccountResponse>(account);
        }

        public Task ValidateResetToken(ValidateResetTokenRequest model)
        {
            throw new NotImplementedException();
        }

        public Task VerifyEmail(string token)
        {
            throw new NotImplementedException();
        }
    }
    
}
