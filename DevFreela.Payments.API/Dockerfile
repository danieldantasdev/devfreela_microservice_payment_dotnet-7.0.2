FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DevFreela.Payments.API/DevFreela.Payments.API.csproj", "DevFreela.Payments.API/"]
RUN dotnet restore "DevFreela.Payments.API/DevFreela.Payments.API.csproj"
COPY . .
WORKDIR "/src/DevFreela.Payments.API"
RUN dotnet build "DevFreela.Payments.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DevFreela.Payments.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevFreela.Payments.API.dll"]
