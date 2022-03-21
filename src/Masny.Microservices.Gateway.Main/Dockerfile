#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Masny.Microservices.Gateway.Main/Masny.Microservices.Gateway.Main.csproj", "Masny.Microservices.Gateway.Main/"]
COPY ["Masny.Microservices.Auth/Masny.Microservices.Auth.csproj", "Masny.Microservices.Auth/"]
RUN dotnet restore "Masny.Microservices.Gateway.Main/Masny.Microservices.Gateway.Main.csproj"
COPY . .
WORKDIR "/src/Masny.Microservices.Gateway.Main"
RUN dotnet build "Masny.Microservices.Gateway.Main.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Masny.Microservices.Gateway.Main.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Masny.Microservices.Gateway.Main.dll"]