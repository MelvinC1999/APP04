#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AbstracFactory_RedSocial_DAO_DTO_Docker/AbstracFactory_RedSocial_DAO_DTO_Docker.csproj", "AbstracFactory_RedSocial_DAO_DTO_Docker/"]
RUN dotnet restore "AbstracFactory_RedSocial_DAO_DTO_Docker/AbstracFactory_RedSocial_DAO_DTO_Docker.csproj"
COPY . .
WORKDIR "/src/AbstracFactory_RedSocial_DAO_DTO_Docker"
RUN dotnet build "AbstracFactory_RedSocial_DAO_DTO_Docker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AbstracFactory_RedSocial_DAO_DTO_Docker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AbstracFactory_RedSocial_DAO_DTO_Docker.dll"]