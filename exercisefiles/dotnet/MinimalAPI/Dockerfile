# create dotnet 7 image for the current project

# Use the official .NET 7 SDK image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS build

# Set the working directory to /app
WORKDIR /app

# Copy the current directory contents into the container at /app
COPY . .

# Build the project and restore the packages
RUN dotnet restore

# Publish the project to a folder called "out"
RUN dotnet publish -c Release -o out

# Use the official .NET 7 runtime image as the base image
FROM mcr.microsoft.com/dotnet/runtime:7.0 AS runtime

# Set the working directory to /app
WORKDIR /app

# Copy the published output from the build image to the runtime image
COPY --from=build /app/out ./

# Set the entry point to the application
ENTRYPOINT ["dotnet", "/app/MinimalAPI.dll"]

# # Build Stage

# # create dotnet 8 image for the current project
# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# WORKDIR /source

# # copy csproj and restore as distinct layers
# # COPY *.sln .
# # COPY MinimalAPI/*.csproj ./MinimalAPI/
# COPY . ./MinimalAPI/

# # Print the contents of the source directory to ensure files are copied
# # RUN ls -la /source
# # RUN ls -la /source/MinimalAPI

# # Install any missing dependencies
# RUN apt-get update && apt-get install -y \
#     libicu-dev \
#     && rm -rf /var/lib/apt/lists/*

# # Print the contents of the MinimalAPI directory to ensure the csproj file is copied
# RUN ls -la ./MinimalAPI/

# # Run dotnet restore with no cache to avoid using cached layers
# RUN dotnet restore "MinimalAPI/MinimalAPI.csproj" -r linux-x64 --disable-parallel --no-cache

# # copy everything else and build app
# COPY MinimalAPI/. ./MinimalAPI/
# WORKDIR /source/MinimalAPI
# RUN dotnet publish -c release -o /app --no-restore

# # Server Stage

# # final stage/image
# FROM mcr.microsoft.com/dotnet/aspnet:8.0
# WORKDIR /app
# COPY --from=build /app ./

# # run the app on port 8080
# EXPOSE 8080

# ENTRYPOINT ["dotnet", "MinimalAPI.dll"]
