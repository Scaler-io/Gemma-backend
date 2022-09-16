#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Discount/Gemma.Discount.GRPC/Gemma.Discount.GRPC.csproj", "Services/Discount/Gemma.Discount.GRPC/"]
COPY ["Services/Shared/Gemma.Shared/Gemma.Shared.csproj", "Services/Shared/Gemma.Shared/"]
COPY ["Services/Infrastructure/Gemma.Infrastructure/Gemma.Infrastructure.csproj", "Services/Infrastructure/Gemma.Infrastructure/"]
RUN dotnet restore "Services/Discount/Gemma.Discount.GRPC/Gemma.Discount.GRPC.csproj"
COPY . .
WORKDIR "/src/Services/Discount/Gemma.Discount.GRPC"
RUN dotnet build "Gemma.Discount.GRPC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gemma.Discount.GRPC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gemma.Discount.GRPC.dll"]
