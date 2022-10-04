#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Order/Gemma.Order.Api/Gemma.Order.Api.csproj", "Services/Order/Gemma.Order.Api/"]
COPY ["Services/Order/Gemma.Order.Application/Gemma.Order.Application.csproj", "Services/Order/Gemma.Order.Application/"]
COPY ["Services/Shared/Gemma.Shared/Gemma.Shared.csproj", "Services/Shared/Gemma.Shared/"]
COPY ["Services/Infrastructure/Gemma.Infrastructure/Gemma.Infrastructure.csproj", "Services/Infrastructure/Gemma.Infrastructure/"]
COPY ["Services/Order/Gemma.Order.Domain/Gemma.Order.Domain.csproj", "Services/Order/Gemma.Order.Domain/"]
COPY ["Services/Order/Gemma.Order.Infrastructure/Gemma.Order.Infrastructure.csproj", "Services/Order/Gemma.Order.Infrastructure/"]
RUN dotnet restore "Services/Order/Gemma.Order.Api/Gemma.Order.Api.csproj"
COPY . .
WORKDIR "/src/Services/Order/Gemma.Order.Api"
RUN dotnet build "Gemma.Order.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gemma.Order.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gemma.Order.Api.dll"]
