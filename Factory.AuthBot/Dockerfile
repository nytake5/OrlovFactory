FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Factory.AuthBot/Factory.AuthBot.csproj", "Factory.AuthBot/"]
COPY ["BLL.Interfaces/BLL.Interfaces.csproj", "BLL.Interfaces/"]
COPY ["Entities/Entities.csproj", "Entities/"]
COPY ["BLL/BLL.csproj", "BLL/"]
COPY ["DAL/DAL.csproj", "DAL/"]
COPY ["DAL.Interfaces/DAL.Interfaces.csproj", "DAL.Interfaces/"]
COPY ["Dal.Dapper/Dal.Dapper.csproj", "Dal.Dapper/"]
RUN dotnet restore "Factory.AuthBot/Factory.AuthBot.csproj"
COPY . .
WORKDIR "/src/Factory.AuthBot"
RUN dotnet build "Factory.AuthBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Factory.AuthBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Factory.AuthBot.dll"]
