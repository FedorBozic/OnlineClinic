FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 1027

RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

USER student
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Integration API/Integration API.csproj", "Integration API/"]
COPY ["Integration/Integration.csproj", "Integration/"]
RUN dotnet restore "Integration API/Integration API.csproj"
COPY . .
WORKDIR "/src/Integration API"
RUN dotnet build "Integration API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Integration API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Integration API.dll"]