FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY TimeManager.slnx ./

COPY TimeManager.API/TimeManager.API.csproj TimeManager.API/
COPY TimeManager.Application/TimeManager.Application.csproj TimeManager.Application/
COPY TimeManager.Domain/TimeManager.Domain.csproj TimeManager.Domain/
COPY TimeManager.Infrastructure/TimeManager.Infrastructure.csproj TimeManager.Infrastructure/

RUN dotnet restore "TimeManager.slnx"

COPY . .

WORKDIR /src/TimeManager.API
RUN dotnet publish "TimeManager.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .

ENTRYPOINT [ "dotnet", "TimeManager.API.dll" ]

