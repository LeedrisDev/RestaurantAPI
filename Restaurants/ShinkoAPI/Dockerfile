FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine3.17 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ShinkoAPI/ShinkoAPI.csproj", "ShinkoAPI/"]
RUN dotnet restore "ShinkoAPI/ShinkoAPI.csproj"
COPY . .
WORKDIR "/src/ShinkoAPI"
RUN dotnet build "ShinkoAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShinkoAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShinkoAPI.dll"]
