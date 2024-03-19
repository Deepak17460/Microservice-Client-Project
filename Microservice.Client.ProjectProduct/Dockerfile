
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
ENV ASPNETCORE_ENVIRONMENT="Development"
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Microservice.Client.ProjectProduct/Microservice.Client.ProjectProduct.csproj", "Microservice.Client.ProjectProduct/"]
RUN dotnet restore "Microservice.Client.ProjectProduct/Microservice.Client.ProjectProduct.csproj"
COPY . .
WORKDIR "/src/Microservice.Client.ProjectProduct"
RUN dotnet build "Microservice.Client.ProjectProduct.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.Client.ProjectProduct.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.Client.ProjectProduct.dll"]