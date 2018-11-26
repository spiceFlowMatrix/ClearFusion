FROM microsoft/dotnet:sdk
RUN ls

# Expecting the release publish folder to be in Training24Api/release the same directory as the Dockerfile
COPY ./HumanitarianAssistance/release /app
WORKDIR /app
ENTRYPOINT ["dotnet", "HumanitarianAssistance.WebAPI.dll"]