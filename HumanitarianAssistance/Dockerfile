FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY bin/Debug/netcoreapp2.0/publish .
 
ENV ASPNETCORE_URLS http://+:4000
EXPOSE 4000
 
ENTRYPOINT ["dotnet", "HumanitarianAssistance.WebAPI.dll"]
