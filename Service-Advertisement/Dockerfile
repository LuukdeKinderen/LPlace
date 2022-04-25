#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Service-Advertisement/Service-Advertisement.csproj", "Service-Advertisement/"]
COPY ["RabbitMQ-Models/RabbitMQ-Models.csproj", "RabbitMQ-Models/"]
RUN dotnet restore "Service-Advertisement/Service-Advertisement.csproj"
COPY . .
WORKDIR "/src/Service-Advertisement"
RUN dotnet build "Service-Advertisement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Service-Advertisement.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Service-Advertisement.dll"]