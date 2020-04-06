#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["test/Ivory.SoapApi/Ivory.SoapApi.csproj", "test/Ivory.SoapApi/"]
RUN dotnet restore "test/Ivory.SoapApi/Ivory.SoapApi.csproj"
COPY . .
WORKDIR "/src/test/Ivory.SoapApi"
RUN dotnet build "Ivory.SoapApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ivory.SoapApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ivory.SoapApi.dll"]