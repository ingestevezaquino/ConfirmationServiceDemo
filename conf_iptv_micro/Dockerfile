FROM mcr.microsoft.com/dotnet/sdk:6.0.201-alpine3.15-amd64 AS build-env
WORKDIR /app
COPY . ./
RUN dotnet publish -c Release "./CSConfIPTV.API/CSConfIPTV.API.csproj" -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0.3-alpine3.15-amd64
EXPOSE 80
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet","CSConfIPTV.API.dll"]