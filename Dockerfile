FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

RUN dotnet restore "TestTask/TestTask.csproj"
WORKDIR "/src/."
COPY . .
RUN dotnet build "TestTask/TestTask.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestTask/TestTask.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "TestTask.dll"]