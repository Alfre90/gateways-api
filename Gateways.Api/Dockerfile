#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Gateways.Api/Gateways.Api.csproj", "Gateways.Api/"]
COPY ["Gateways.Core/Gateways.Core.csproj", "Gateways.Core/"]
COPY ["Gateways.Database/Gateways.Database.csproj", "Gateways.Database/"]
COPY ["Gateways.Services/Gateways.Services.csproj", "Gateways.Services/"]
RUN dotnet restore "Gateways.Api/Gateways.Api.csproj"
COPY . .
WORKDIR "/src/Gateways.Api"
RUN dotnet build "Gateways.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gateways.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gateways.Api.dll"]