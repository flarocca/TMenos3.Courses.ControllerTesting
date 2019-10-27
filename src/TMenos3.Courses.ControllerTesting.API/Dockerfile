FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["TMenos3.Courses.ControllerTesting.API/TMenos3.Courses.ControllerTesting.API.csproj", "TMenos3.Courses.ControllerTesting.API/"]
RUN dotnet restore "TMenos3.Courses.ControllerTesting.API/TMenos3.Courses.ControllerTesting.API.csproj"
COPY . .
WORKDIR "/src/TMenos3.Courses.ControllerTesting.API"
RUN dotnet build "TMenos3.Courses.ControllerTesting.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TMenos3.Courses.ControllerTesting.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TMenos3.Courses.ControllerTesting.API.dll"]