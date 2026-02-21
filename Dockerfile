FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/OncoTimeline.Web/OncoTimeline.Web.csproj", "OncoTimeline.Web/"]
COPY ["src/OncoTimeline.Application/OncoTimeline.Application.csproj", "OncoTimeline.Application/"]
COPY ["src/OncoTimeline.Domain/OncoTimeline.Domain.csproj", "OncoTimeline.Domain/"]
COPY ["src/OncoTimeline.Infrastructure/OncoTimeline.Infrastructure.csproj", "OncoTimeline.Infrastructure/"]
RUN dotnet restore "OncoTimeline.Web/OncoTimeline.Web.csproj"
COPY src/ .
WORKDIR "/src/OncoTimeline.Web"
RUN dotnet build "OncoTimeline.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OncoTimeline.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OncoTimeline.Web.dll"]
