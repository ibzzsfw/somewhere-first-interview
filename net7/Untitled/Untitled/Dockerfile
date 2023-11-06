FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Untitled/Untitled.csproj", "Untitled/"]
RUN dotnet restore "Untitled/Untitled.csproj"
COPY . .
WORKDIR "/src/Untitled"
RUN dotnet build "Untitled.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Untitled.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Untitled.dll"]
