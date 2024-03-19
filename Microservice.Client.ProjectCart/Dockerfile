
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
ENV ASPNETCORE_ENVIRONMENT="Development"
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Microservice.Client.ProjectCart/Microservice.Client.ProjectCart.csproj", "Microservice.Client.ProjectCart/"]
RUN dotnet restore "Microservice.Client.ProjectCart/Microservice.Client.ProjectCart.csproj"
COPY . .
WORKDIR "/src/Microservice.Client.ProjectCart"
RUN dotnet build "Microservice.Client.ProjectCart.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.Client.ProjectCart.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.Client.ProjectCart.dll"]