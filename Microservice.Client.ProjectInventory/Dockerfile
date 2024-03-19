
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
ENV ASPNETCORE_ENVIRONMENT="Development"
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Microservice.Client.ProjectInventory/Microservice.Client.ProjectInventory.csproj", "Microservice.Client.ProjectInventory/"]
RUN dotnet restore "Microservice.Client.ProjectInventory/Microservice.Client.ProjectInventory.csproj"
COPY . .
WORKDIR "/src/Microservice.Client.ProjectInventory"
RUN dotnet build "Microservice.Client.ProjectInventory.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.Client.ProjectInventory.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.Client.ProjectInventory.dll"]