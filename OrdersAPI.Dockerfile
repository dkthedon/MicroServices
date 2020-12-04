ARG REPO=5.0-alpine3.12
FROM mcr.microsoft.com/dotnet/sdk:$REPO AS build

WORKDIR /DataContracts
COPY DataContracts .

WORKDIR /app
ADD OrdersAPI .

RUN dotnet publish \
    --runtime alpine-x64 \
    --self-contained true \
    /p:PublishTrimmed=true \
    /p:PublishSingleFile=true \
    -c Release \
    -o ./output

FROM alpine
RUN apk add --no-cache libstdc++ libintl icu krb5-libs
    
RUN adduser \
    --disabled-password \
    --home /app \
    --gecos '' app \
    && chown -R app /app
USER app

WORKDIR /app
COPY --from=build /app/output .

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1 \
  DOTNET_RUNNING_IN_CONTAINER=true \
  ASPNETCORE_URLS=http://+:5001

EXPOSE 5001

RUN ls

ENTRYPOINT ["./OrdersAPI"]