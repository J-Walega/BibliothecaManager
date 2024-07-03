#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#ENV ASPNETCORE_URLS=https://+:5006;http://+:5005
WORKDIR /app
EXPOSE 80
EXPOSE 433

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Apps/BibliothecaManager.Api/BibliothecaManager.Api.csproj", "src/Apps/BibliothecaManager.Api/"]
COPY ["src/Common/BibliothecaManager.Infrastructure/BibliothecaManager.Infrastructure.csproj", "src/Common/BibliothecaManager.Api.Infrastructure/"]
COPY ["src/Common/BibliothecaManager.Application/BibliothecaManager.Application.csproj", "src/Common/BibliothecaManager.Api.Application/"]
COPY ["src/Common/BibliothecaManager.Domain/BibliothecaManager.Domain.csproj", "src/Common/BibliothecaManager.Api.Domain/"]
RUN dotnet restore "src/Apps/BibliothecaManager.Api/BibliothecaManager.Api.csproj"
COPY . .
WORKDIR "/src/src/Apps/BibliothecaManager.Api"
RUN dotnet build "BibliothecaManager.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BibliothecaManager.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BibliothecaManager.Api.dll"]