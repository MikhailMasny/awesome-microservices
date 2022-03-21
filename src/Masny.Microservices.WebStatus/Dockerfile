#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Masny.Microservices.WebStatus/Masny.Microservices.WebStatus.csproj", "Masny.Microservices.WebStatus/"]
RUN dotnet restore "Masny.Microservices.WebStatus/Masny.Microservices.WebStatus.csproj"
COPY . .
WORKDIR "/src/Masny.Microservices.WebStatus"
RUN dotnet build "Masny.Microservices.WebStatus.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Masny.Microservices.WebStatus.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Masny.Microservices.WebStatus.dll"]