FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Factory.WebApi/Factory.WebApi.csproj", "Factory.WebApi/"]
COPY ["BLL/BLL.csproj", "BLL/"]
COPY ["BLL.Interfaces/BLL.Interfaces.csproj", "BLL.Interfaces/"]
COPY ["Entities/Entities.csproj", "Entities/"]
COPY ["DAL/DAL.csproj", "DAL/"]
COPY ["DAL.Interfaces/DAL.Interfaces.csproj", "DAL.Interfaces/"]
RUN dotnet restore "Factory.WebApi/Factory.WebApi.csproj"
COPY . .
WORKDIR "/src/Factory.WebApi"
RUN dotnet build "Factory.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Factory.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Factory.WebApi.dll"]
