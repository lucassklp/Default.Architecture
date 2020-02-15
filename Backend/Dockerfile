FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS publish
WORKDIR /src
COPY . .
RUN dotnet restore "Default.Architecture/Default.Architecture.csproj"
WORKDIR "/src/Default.Architecture"
RUN dotnet publish "Default.Architecture.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/runtime:3.1
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Default.Architecture.dll"]