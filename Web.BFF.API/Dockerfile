#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# Install clang/zlib1g-dev dependencies for publishing to native
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    clang zlib1g-dev
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Web.BFF.API/Web.BFF.API.csproj", "Web.BFF.API/"]
COPY ["Web.BFF.Domain/Web.BFF.Domain.csproj", "Web.BFF.Domain/"]
RUN dotnet restore "./Web.BFF.API/./Web.BFF.API.csproj"
COPY . .
WORKDIR "/src/Web.BFF.API"
RUN dotnet build "./Web.BFF.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Web.BFF.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=true

FROM mcr.microsoft.com/dotnet/runtime-deps:8.0 AS final
ENV DOTNET_ROOT=/usr/share/dotnet
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["./Web.BFF.API"]