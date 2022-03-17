FROM mcr.microsoft.com/dotnet/sdk:6.0 AS builder
WORKDIR /app
COPY . ./
RUN dotnet build ./CSCore.API/CSCore.API.csproj

FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app
COPY --from=builder /app/CSCore.API/bin/Release/net6.0/ ./
ENTRYPOINT ['dotnet','CSCore.API.dll']
