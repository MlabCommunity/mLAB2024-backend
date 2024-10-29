# Use the official .NET SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 5276

# Copy the project files
COPY QuizBackend.sln .
COPY src/QuizBackend.Api/QuizBackend.Api.csproj ./src/QuizBackend.Api/
COPY src/QuizBackend.Application/QuizBackend.Application.csproj ./src/QuizBackend.Application/
COPY src/QuizBackend.Domain/QuizBackend.Domain.csproj ./src/QuizBackend.Domain/
COPY src/QuizBackend.Infrastructure/QuizBackend.Infrastructure.csproj ./src/QuizBackend.Infrastructure/

# Restore the NuGet packages
RUN dotnet restore

# Copy everything else and build the API
COPY . .
WORKDIR /app/src/QuizBackend.Api
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/src/QuizBackend.Api/out ./
ENTRYPOINT ["dotnet", "QuizBackend.Api.dll"]