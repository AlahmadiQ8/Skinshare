# ----- SDK ----- # 
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Skinshare.Core/*.csproj ./Skinshare.Core/
COPY Skinshare.Data/*.csproj ./Skinshare.Data/
COPY Skinshare.Seed/*.csproj ./Skinshare.Seed/
COPY Skinshare.Tests/*.csproj ./Skinshare.Tests/
COPY Skinshare.Web/*.csproj ./Skinshare.Web/
RUN dotnet restore

# copy everything else and build app
COPY Skinshare.Core/. ./Skinshare.Core/
COPY Skinshare.Data/. ./Skinshare.Data/
COPY Skinshare.Seed/. ./Skinshare.Seed/
COPY Skinshare.Tests/. ./Skinshare.Tests/
COPY Skinshare.Web/. ./Skinshare.Web/
WORKDIR /app/Skinshare.Web
RUN dotnet publish -c Release -o out
# run nswag 
RUN dotnet msbuild -target:NSwag  \
    -property:NSwagDllDir=./publish/Skinshare.Web.dll \
    -property:NSwagOutputDir=. 

# ----- ANGULAR ----- # 
FROM node:10.19-alpine AS angular
WORKDIR /app
COPY Skinshare.Web/ClientApp/package.json .
COPY Skinshare.Web/ClientApp/package-lock.json .
RUN npm install
COPY Skinshare.Web/ClientApp/. .
COPY --from=build /app/Skinshare.Web/app.generated.ts .
RUN npm run build -- --prod

# ----- Frontend ----- # 
FROM node:10.19-alpine AS frontend
WORKDIR /app
COPY Skinshare.Web/frontend/package.json .
COPY Skinshare.Web/frontend/package-lock.json .
RUN npm install
COPY Skinshare.Web/frontend/. .
RUN npm run build

# ----- Runtime ----- # 
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/Skinshare.Web/out ./
COPY --from=angular /app/dist/ ./wwwroot/ClientApp/dist/
COPY --from=frontend /app/build/. ./wwwroot/assets
EXPOSE 5000
ENV ASPNETCORE_URLS http://+:5000
CMD ["dotnet", "Skinshare.Web.dll"]
