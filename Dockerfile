
FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /app  

COPY ./Smokeball.Seo.Api.sln ./nuget.config  ./

COPY ./src/Smokeball.Seo.Api/Smokeball.Seo.Api.csproj  ./src/Smokeball.Seo.Api/Smokeball.Seo.Api.csproj
COPY ./src/Smokeball.Seo.Ui/Smokeball.Seo.Ui.csproj  ./src/Smokeball.Seo.Ui/Smokeball.Seo.Ui.csproj
COPY ./test/Smokeball.Seo.Api.Tests/Smokeball.Seo.Api.Tests.csproj  ./test/Smokeball.Seo.Api.Tests/Smokeball.Seo.Api.Tests.csproj 

RUN dotnet restore

# Copy all the source code and build
COPY ./test ./test  
COPY ./src ./src  
RUN dotnet build -c Release --no-restore

# Run dotnet test on the solution
RUN dotnet test "./test/Smokeball.Seo.Api.Tests/Smokeball.Seo.Api.Tests.csproj" -c Release --no-build --no-restore

FROM build AS publish
RUN dotnet publish "./src/Smokeball.Seo.Api/Smokeball.Seo.Api.csproj" -c Release -o /app/publish/ --no-restore

#App image
FROM base AS final
WORKDIR /app  
COPY --from=publish /app/publish .  
ENV ASPNETCORE_URLS=http://*:5000
ENTRYPOINT ["dotnet", "Smokeball.Seo.Api.dll"]
