#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Catalog/Gemma.Catalog.API/Gemma.Catalog.API.csproj", "Services/Catalog/Gemma.Catalog.API/"]
COPY ["Services/Shared/Gemma.Shared/Gemma.Shared.csproj", "Services/Shared/Gemma.Shared/"]
RUN dotnet restore "Services/Catalog/Gemma.Catalog.API/Gemma.Catalog.API.csproj"
COPY . .
WORKDIR "/src/Services/Catalog/Gemma.Catalog.API"
RUN dotnet build "Gemma.Catalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gemma.Catalog.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gemma.Catalog.API.dll"]
