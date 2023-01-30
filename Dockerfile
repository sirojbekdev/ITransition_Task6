#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 444

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Task6/Task6/Task6.csproj", "Task6/"]
RUN dotnet restore "Task6/Task6.csproj"
COPY . .
WORKDIR "/src/Task6/Task6"
RUN dotnet build "Task6.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Task6.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Task6.dll"]