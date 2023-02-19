FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BlazorTrades/BlazorTrades.csproj", "BlazorTrades/"]
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
