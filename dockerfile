
# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
# WORKDIR /prototype-csharp-alchemy-helper

# # Copy everything
# COPY . ./
# # Restore as distinct layers
# RUN dotnet restore
# # Build and publish a release
# RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /prototype-csharp-alchemy-helper
COPY ./prototype_csharp_alchemy_helper_api/bin/Release/net8.0/publish .
ENTRYPOINT ["dotnet", "prototype_csharp_alchemy_helper_api.dll"]
