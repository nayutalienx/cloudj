FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["CloudJ.API/CloudJ.API.csproj", "CloudJ.API/"]
RUN dotnet restore "CloudJ.API/CloudJ.API.csproj"
COPY . .
WORKDIR "/src/CloudJ.API"
RUN dotnet build "CloudJ.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CloudJ.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CloudJ.API.dll"]