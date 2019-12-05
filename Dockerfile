FROM microsoft/dotnet:sdk
RUN apt-get install libgdiplus

RUN    cd /usr/lib
RUN   ln -s libgdiplus.so gdiplus.dll
RUN    apt-get install libc6-dev libx11-dev
RUN   rm -rf /var/lib/apt/lists/*
# Expecting the release publish folder to be in Training24Api/release the same directory as the Dockerfile
COPY ./HumanitarianAssistance.WebApi/release /app
WORKDIR /app
ENTRYPOINT ["dotnet", "HumanitarianAssistance.WebApi.dll"]