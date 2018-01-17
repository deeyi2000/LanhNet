FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY *.sln ./
COPY Com.LanhNet.Iot.WebApi/Com.LanhNet.Iot.WebApi.csproj Com.LanhNet.Iot.WebApi/
COPY Com.LanhNet.Iot/Com.LanhNet.Iot.csproj Com.LanhNet.Iot/
RUN dotnet restore
COPY . .
WORKDIR /src/Com.LanhNet.Iot.WebApi
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Com.LanhNet.Iot.WebApi.dll"]
