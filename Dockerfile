FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

RUN apt-get update && apt-get install -y git

WORKDIR /app

RUN git clone https://github.com/ndrxy/ImplementacaoSD .

WORKDIR /app/src/projetoRedes.API

RUN dotnet publish -c Release -o /app/compiledApi

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build-env /app/compiledApi .

ENTRYPOINT ["dotnet", "projetoRedes.API.dll"]
