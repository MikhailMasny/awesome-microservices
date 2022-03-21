#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Masny.Microservices.Profile.Api/Masny.Microservices.Profile.Api.csproj", "Masny.Microservices.Profile.Api/"]
COPY ["Masny.Microservices.EventBus/Masny.Microservices.EventBus.csproj", "Masny.Microservices.EventBus/"]
RUN dotnet restore "Masny.Microservices.Profile.Api/Masny.Microservices.Profile.Api.csproj"
COPY . .
WORKDIR "/src/Masny.Microservices.Profile.Api"
RUN dotnet build "Masny.Microservices.Profile.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Masny.Microservices.Profile.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Masny.Microservices.Profile.Api.dll"]