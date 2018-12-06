FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5000

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Default.Architecture/Default.Architecture.csproj", "Default.Architecture/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Extensions/Extensions.csproj", "Extensions/"]
COPY ["Business/Business.csproj", "Business/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["Persistence/Persistence.csproj", "Persistence/"]
COPY ["Jobs/Jobs.csproj", "Jobs/"]
RUN dotnet restore "Default.Architecture/Default.Architecture.csproj"
COPY . .
WORKDIR "/src/Default.Architecture"
RUN dotnet build "Default.Architecture.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Default.Architecture.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Default.Architecture.dll"]