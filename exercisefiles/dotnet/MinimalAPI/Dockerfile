# create dotnet 7 image for the current project
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY MinimalAPI/*.csproj ./MinimalAPI/
RUN dotnet restore

# copy everything else and build app
COPY MinimalAPI/. ./MinimalAPI/
WORKDIR /source/MinimalAPI
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "MinimalAPI.dll"]
