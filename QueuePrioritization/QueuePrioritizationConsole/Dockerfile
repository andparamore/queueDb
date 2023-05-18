FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["QueuePrioritizationConsole/QueuePrioritizationConsole.csproj", "QueuePrioritizationConsole/"]
RUN dotnet restore "QueuePrioritizationConsole/QueuePrioritizationConsole.csproj"
COPY . .
WORKDIR "/src/QueuePrioritizationConsole"
RUN dotnet build "QueuePrioritizationConsole.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QueuePrioritizationConsole.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QueuePrioritizationConsole.dll"]
