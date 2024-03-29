#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Basket/Gemma.Basket.API/Gemma.Basket.API.csproj", "Services/Basket/Gemma.Basket.API/"]
COPY ["Services/Shared/Gemma.Shared/Gemma.Shared.csproj", "Services/Shared/Gemma.Shared/"]
COPY ["Services/Infrastructure/EventBus.Message/EventBus.Message.csproj", "Services/Infrastructure/EventBus.Message/"]
RUN dotnet restore "Services/Basket/Gemma.Basket.API/Gemma.Basket.API.csproj"
COPY . .
WORKDIR "/src/Services/Basket/Gemma.Basket.API"
RUN dotnet build "Gemma.Basket.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gemma.Basket.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gemma.Basket.API.dll"]
