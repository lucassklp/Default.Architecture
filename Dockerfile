FROM node:16.13.1 as frontend-build
WORKDIR /src
COPY ./Frontend .
RUN npm ci
RUN npm run build-prod

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS backend-build
WORKDIR /src
COPY ./Backend .
COPY --from=frontend-build /src/dist/default-architecture /src/Default.Architecture/wwwroot
RUN dotnet restore
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=backend-build /app/ .
EXPOSE 80
ENTRYPOINT ["dotnet", "Default.Architecture.dll"]