FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

RUN apt-get update && apt-get install -y git

WORKDIR /app

RUN git clone https://github.com/ndrxy/RedesAvaliacao1 .

COPY src/ .

WORKDIR projetoRedes.API

RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "projetoRedes.API.dll"]
