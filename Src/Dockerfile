FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY ./Appdoon.Application/Mapdoon.Application.csproj ./Appdoon.Application/Mapdoon.Application.csproj
COPY ./Appdoon.Common/Mapdoon.Common.csproj ./Appdoon.Common/Mapdoon.Common.csproj
COPY ./Appdoon.Domain/Mapdoon.Domain.csproj ./Appdoon.Domain/Mapdoon.Domain.csproj
COPY ./Appdoon.Presistence/Mapdoon.Presistence.csproj ./Appdoon.Presistence/Mapdoon.Presistence.csproj
COPY ./Appdoon.WebApi/Mapdoon.WebApi.csproj ./Appdoon.WebApi/Mapdoon.WebApi.csproj
COPY ./Src.sln ./Src.sln
RUN dotnet restore
COPY . . 
RUN dotnet publish -c Release -o /app/publish ./Appdoon.WebApi/Mapdoon.WebApi.csproj

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
USER root 
WORKDIR /app
RUN mkdir Photos
COPY --from=build /app/publish .
EXPOSE 5000
CMD ["dotnet", "Mapdoon.WebApi.dll"]
