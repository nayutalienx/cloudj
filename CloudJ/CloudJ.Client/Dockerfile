FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["CloudJ.Client/CloudJ.Client.csproj", "CloudJ.Client/"]
RUN dotnet restore "CloudJ.Client/CloudJ.Client.csproj"
COPY . .
WORKDIR "/src/CloudJ.Client"
RUN dotnet build "CloudJ.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CloudJ.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CloudJ.Client.dll"]