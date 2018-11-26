FROM microsoft/dotnet:sdk
RUN ls

# Expecting the release publish folder to be in Training24Api/release the same directory as the Dockerfile
COPY ./Training24Api/release /app
WORKDIR /app
RUN ls
ENTRYPOINT ["dotnet", "Training24Api.dll"]