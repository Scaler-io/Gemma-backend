#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Gemma.Basket.API/Gemma.Basket.API.csproj", "Gemma.Basket.API/"]
COPY ["Services/Shared/Gemma.Shared/Gemma.Shared.csproj", "Services/Shared/Gemma.Shared/"]
RUN dotnet restore "Gemma.Basket.API/Gemma.Basket.API.csproj"
COPY . .
WORKDIR "/src/Gemma.Basket.API"
RUN dotnet build "Gemma.Basket.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gemma.Basket.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gemma.Basket.API.dll"] 
