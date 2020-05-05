FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY publish/. ./
EXPOSE 5000
ENV ASPNETCORE_URLS http://+:5000
ENTRYPOINT ["dotnet", "Skinshare.Web.dll"]
