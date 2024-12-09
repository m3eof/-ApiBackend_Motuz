# Base image for deployment
FROM mcr.microsoft.com/dotnet/aspnet:6.0. AS base
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app

# Build image to compile the application
FROM mcr.microsoft.com/dotnet/sdk:6.0. AS build
WORKDIR /src

# Copy project files and restore dependencies
COPY ["ApiBackend/ApiBackend.csproj", "ApiBackend/"]
COPY ["ConsoleApp2/ConsoleApp2.csproj", "ConsoleApp2/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
RUN dotnet restore "ApiBackend/ApiBackend.csproj"

# Copy the rest of the files and publish the app
COPY . .
FROM build AS publish
RUN dotnet publish "ApiBackend/ApiBackend.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiBackend.dll"]