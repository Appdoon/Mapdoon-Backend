FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish ./Appdoon.WebApi/Mapdoon.WebApi.csproj

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
USER root 
WORKDIR /app
RUN mkdir Photos
COPY --from=build /app/publish .
EXPOSE 5000
CMD ["dotnet", "Mapdoon.WebApi.dll"] 
