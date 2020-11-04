#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["webapplication-estudos-asp-net-core-rabbitmq.v1/webapplication-estudos-asp-net-core-rabbitmq.v1.csproj", "webapplication-estudos-asp-net-core-rabbitmq.v1/"]
RUN dotnet restore "webapplication-estudos-asp-net-core-rabbitmq.v1/webapplication-estudos-asp-net-core-rabbitmq.v1.csproj"
COPY . .
WORKDIR "/src/webapplication-estudos-asp-net-core-rabbitmq.v1"
RUN dotnet build "webapplication-estudos-asp-net-core-rabbitmq.v1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "webapplication-estudos-asp-net-core-rabbitmq.v1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "webapplication-estudos-asp-net-core-rabbitmq.v1.dll"]