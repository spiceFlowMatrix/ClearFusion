FROM microsoft/dotnet:sdk
RUN apt-get update
# libgdiplus used for excel export styling and autofit columns
RUN apt-get install -y libgdiplus
#RUN ln -s /usr/lib/libgdiplus.so/usr/lib/gdiplus.dll
# Expecting the release publish folder to be in Training24Api/release the same directory as the Dockerfile
COPY ./HumanitarianAssistance.WebApi/release /app
WORKDIR /app
ENTRYPOINT ["dotnet", "HumanitarianAssistance.WebApi.dll"]