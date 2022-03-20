FROM postgres:alpine
ENV POSTGRES_HOST_AUTH_METHOD=trust
COPY import-data.sql /docker-entrypoint-initdb.d
RUN chmod a+r /docker-entrypoint-initdb.d/*
