FROM microsoft/dotnet:sdk
# Expecting the release publish folder to be in Training24Api/release the same directory as the Dockerfile
COPY ./HumanitarianAssistance.WebApi/release /app
WORKDIR /app
ENTRYPOINT ["dotnet", "HumanitarianAssistance.WebApi.dll"]