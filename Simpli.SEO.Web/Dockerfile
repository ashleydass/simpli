FROM microsoft/dotnet:2.1.301-sdk AS builder
WORKDIR /source

RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get install -y nodejs

COPY *.csproj .
RUN dotnet restore

COPY ./ ./

RUN dotnet publish "./Simpli.SEO.Web.csproj" --output "./dist" --configuration Release --no-restore

FROM microsoft/dotnet:2.1.1-aspnetcore-runtime
WORKDIR /app
COPY --from=builder /source/dist .
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_HTTPS_PORT 5443
ENV ASPNETCORE_URLS https://+:443;http://+:80
ENV ASPNETCORE_ENVIRONMENT "Production"
ENV TZ=Australia/Sydney
ENV Kestrel__Certificates__Default__Path "/root/.dotnet/https/cert-aspnetcore.pfx"
ENV Kestrel__Certificates__Default__Password "ufo"


RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

ENTRYPOINT ["dotnet", "Simpli.SEO.Web.dll"]