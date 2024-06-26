#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
RUN apt-get update
RUN apt-get install -y apt-utils
RUN apt-get install -y libgdiplus
RUN apt-get install -y libc6-dev 
RUN ln -s /usr/lib/libgdiplus.so/usr/lib/gdiplus.dll
USER app
WORKDIR /app
COPY Docs/*.html /app/Docs/
COPY libwkhtmltox.dll libwkhtmltox.dll
COPY libwkhtmltox.dylib libwkhtmltox.dylib
COPY libwkhtmltox.so libwkhtmltox.so
#EXPOSE 8080
#EXPOSE 8081
EXPOSE 5000 
ENV ASPNETCORE_URLS=http://*:5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ConvertDoc.csproj", "."]
RUN dotnet restore "./././ConvertDoc.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./ConvertDoc.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ConvertDoc.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConvertDoc.dll"]