
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
ENV ASPNETCORE_ENVIRONMENT="Development"
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Microservice.Client.ProjectOrder/Microservice.Client.ProjectOrder.csproj", "Microservice.Client.ProjectOrder/"]
RUN dotnet restore "Microservice.Client.ProjectOrder/Microservice.Client.ProjectOrder.csproj"
COPY . .
WORKDIR "/src/Microservice.Client.ProjectOrder"
RUN dotnet build "Microservice.Client.ProjectOrder.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.Client.ProjectOrder.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.Client.ProjectOrder.dll"]