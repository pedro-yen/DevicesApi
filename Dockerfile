# Use SDK image to build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore DevicesApi.Api/DevicesApi.Api.csproj
RUN dotnet publish DevicesApi.Api/DevicesApi.Api.csproj -c Release -o /app/publish

# Final image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DevicesApi.Api.dll"]
