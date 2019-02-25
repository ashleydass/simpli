# Stage 1
FROM microsoft/dotnet:2.2-sdk AS build
ARG version

# Setup node
ENV NODE_VERSION 11.6.0
ENV NODE_DOWNLOAD_SHA EE5B070CAA8E812EE763B65E75C6F4F120A65E40FDEF807B075E39DC8916FA9C

RUN curl -SL "https://nodejs.org/dist/v${NODE_VERSION}/node-v${NODE_VERSION}-linux-x64.tar.gz"  --output nodejs.tar.gz \
    && echo "$NODE_DOWNLOAD_SHA nodejs.tar.gz" | sha256sum -c - \
    && tar -xzf "nodejs.tar.gz" -C /usr/local --strip-components=1 \
    && rm nodejs.tar.gz \
    && ln -s /usr/local/bin/node /usr/local/bin/nodejs

RUN npm i npm@latest -g

WORKDIR /src
COPY Simpli.SEO.Web/Simpli.SEO.Web.csproj Simpli.SEO.Web/
RUN dotnet restore Simpli.SEO.Web/Simpli.SEO.Web.csproj
COPY . .
WORKDIR /src/Simpli.SEO.Web
RUN dotnet build Simpli.SEO.Web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Simpli.SEO.Web.csproj -c Release -o /app

# Stage 3
FROM microsoft/dotnet:2.2-aspnetcore-runtime AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "Simpli.SEO.Web.dll"]