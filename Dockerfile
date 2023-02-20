FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5287
ENV ASPNETCORE_URLS="http://0.0.0.0:5287/"

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BlazorTrades.csproj", "BlazorTrades.csproj"]
RUN dotnet restore "BlazorTrades.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "BlazorTrades.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorTrades.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorTrades.dll"]
