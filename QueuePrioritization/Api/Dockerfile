FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["QueuePrioritizationApi/QueuePrioritizationApi.csproj", "QueuePrioritizationApi/"]
RUN dotnet restore "QueuePrioritizationApi/QueuePrioritizationApi.csproj"
COPY . .
WORKDIR "/src/QueuePrioritizationApi"
RUN dotnet build "QueuePrioritizationApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QueuePrioritizationApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QueuePrioritizationApi.dll"]
