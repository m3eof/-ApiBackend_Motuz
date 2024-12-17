using ApiBackend.Models.Accounts;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ApiBackend.Authorization;
using ApiBackend.Entities;
using ApiBackend.Helpers;
using ApiBackend.Models;
using ApiBackend.Controllers;
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

        private async Task<User> getAccountByRefreshToken(string token)
        {
              var account = await _repositoryWrapper.Users
                .Where(x => x.ResetToken == token && x.ResetTokenExpires > DateTime.UtcNow)
                .SingleOrDefaultAsync(); // Используем SingleOrDefaultAsync для асинхронного выполнения

            // Проверяем, существует ли пользователь
            if (account == null) throw new AppException("Invalid token");

            return account;


        }
        private async Task<RefreshToken> rotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            // Ожидаем результат от асинхронного метода
            var newRefreshToken = await _jwtUtils.GenerateRefreshToken(ipAddress);

            // Отзываем старый токен
            revokeRefreshToken(refreshToken, ipAddress, "Replaced by new token");

            // Возвращаем новый refresh token
            return newRefreshToken;
        }

        private void revokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken; // хз если честно
        }
        private void revokeDescendantRefreshTokens(RefreshToken refreshToken, User account, string ipAddress, string reason)
        {
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = account.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                if (childToken.IsActive)
                {
                    revokeRefreshToken(childToken, ipAddress, reason);
                }
                else
                {
                    revokeDescendantRefreshTokens(childToken, account, ipAddress, reason);
                }
            }
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
                .Where(x => x.Email == model.Email) 
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

        private async Task<User> getAccountByResetToken(string token)
        {
            // Находим пользователя с соответствующим токеном сброса пароля и проверяем, не истек ли срок действия токена
            var account = await _repositoryWrapper.Users
                .Where(x => x.ResetToken == token && x.ResetTokenExpires > DateTime.UtcNow)
                .SingleOrDefaultAsync(); // Используем SingleOrDefaultAsync для асинхронного выполнения

            // Проверяем, существует ли пользователь
            if (account == null) throw new AppException("Invalid token");

            return account;
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
        .FirstOrDefaultAsync(x => x.Email == model.Email); 
            if (account == null) return; 

            account.ResetToken = await generateResetToken(); 
            account.ResetTokenExpires = DateTime.UtcNow.AddDays(1); 

            _repositoryWrapper.Users.Update(account); 
            await _repositoryWrapper.SaveChangesAsync(); 

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

        public async Task<AuthenticateResponse> RefreshToken(string token, string ipAddress)
        {
            var account = await getAccountByRefreshToken(token);
            var refreshToken = account.RefreshTokens.Single(x => x.Token == token);

            if (refreshToken.IsRevoked)
            {
                revokeDescendantRefreshTokens(refreshToken, account, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");
                _repositoryWrapper.Users.Update(account);
                await _repositoryWrapper.SaveChangesAsync();
            }

            if (!refreshToken.IsActive)
                throw new AppException("Invalid token");

            var newRefreshToken = await rotateRefreshToken(refreshToken, ipAddress);
            account.RefreshTokens.Add(newRefreshToken);

            removeOldRefreshTokens(account);

            _repositoryWrapper.Users.Update(account);
            await _repositoryWrapper.SaveChangesAsync();

            var jwtToken = _jwtUtils.GenerateJwtToken(account);

            var response = _mapper.Map<AuthenticateResponse>(account);
            response.JwtToken = jwtToken;
            response.RefreshToken = newRefreshToken.Token;
            return response;
        }

        private async Task<string> generateVerificationToken()
        {
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
            var tokenIsUnique = !(await _repositoryWrapper.Users
                .AnyAsync(x => x.VerificationToken == token));

            if (!tokenIsUnique)
                return await generateVerificationToken();

            return token;
        }

        public async Task Register(RegisterRequest model, string origin)
        {
            
            var existingUserCount = await _repositoryWrapper.Users
                .Where(x => x.Email == model.Email)
                .CountAsync();

           
            if (existingUserCount > 0)
                return;

          
            var account = _mapper.Map<User>(model);

            
            var isFirstAccount = await _repositoryWrapper.Users
                .CountAsync() == 0; 
            account.Role = isFirstAccount ? Role.Admin : Role.User;

            
            account.Created = DateTime.UtcNow;
            account.Verified = DateTime.UtcNow;
            account.VerificationToken = await generateVerificationToken();

            
            account.UserPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

          
            await _repositoryWrapper.Users.AddAsync(account); 
            await _repositoryWrapper.SaveChangesAsync(); 
        }

        public async Task ResetPassword(ResetPasswordRequest model)
        {
            var account = await getAccountByResetToken(model.Token);

            account.UserPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
            account.PasswordReset = DateTime.UtcNow;
            account.ResetToken = null;
            account.ResetTokenExpires = null;

            _repositoryWrapper.Users.Update(account);
            await _repositoryWrapper.SaveChangesAsync();

        }

        public async Task RevokeToken(string token, string ipAddress)
        {
            var account = await getAccountByRefreshToken(token);
            var refreshToken = account.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
                throw new AppException("Invalid token");

            revokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
            _repositoryWrapper.Users.Update(account);
            await _repositoryWrapper.SaveChangesAsync();
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
                 .Where(x => x.Email == model.Email) 
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

        public async Task ValidateResetToken(ValidateResetTokenRequest model)
        {
            await getAccountByResetToken(model.Token);
        }

        public async Task VerifyEmail(string token)
        {
            // Находим пользователя с данным токеном подтверждения
            var account = await _repositoryWrapper.Users
                .Where(x => x.VerificationToken == token)
                .FirstOrDefaultAsync(); // Используем FirstOrDefaultAsync для асинхронного выполнения

            // Проверяем, существует ли аккаунт
            if (account == null)
                throw new AppException("Verification failed");

            // Обновляем данные аккаунта
            account.Verified = DateTime.UtcNow;
            account.VerificationToken = null;

            _repositoryWrapper.Users.Update(account);
            await _repositoryWrapper.SaveChangesAsync();
        }
    }
    
}
