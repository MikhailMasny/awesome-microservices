#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Masny.Microservices.Identity.Api/Masny.Microservices.Identity.Api.csproj", "Masny.Microservices.Identity.Api/"]
COPY ["Masny.Microservices.Auth/Masny.Microservices.Auth.csproj", "Masny.Microservices.Auth/"]
COPY ["Masny.Microservices.EventBus/Masny.Microservices.EventBus.csproj", "Masny.Microservices.EventBus/"]
RUN dotnet restore "Masny.Microservices.Identity.Api/Masny.Microservices.Identity.Api.csproj"
COPY . .
WORKDIR "/src/Masny.Microservices.Identity.Api"
RUN dotnet build "Masny.Microservices.Identity.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Masny.Microservices.Identity.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Masny.Microservices.Identity.Api.dll"]