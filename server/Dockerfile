FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PasswordManagerAPI/PasswordManagerAPI.csproj", "PasswordManagerAPI/"]
COPY ["PasswordManagerAPI.Services/PasswordManagerAPI.Services.csproj", "PasswordManagerAPI.Services/"]
COPY ["PasswordManagerAPI.Repository/PasswordManagerAPI.Repository.csproj", "PasswordManagerAPI.Repository/"]
COPY ["PasswordManagerAPI.Tests/PasswordManagerAPI.Tests.csproj", "PasswordManagerAPI.Tests/"]
RUN dotnet restore "PasswordManagerAPI/PasswordManagerAPI.csproj"
COPY . .
WORKDIR "/src/PasswordManagerAPI"
RUN dotnet build "PasswordManagerAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PasswordManagerAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "PasswordManagerAPI.dll" ]