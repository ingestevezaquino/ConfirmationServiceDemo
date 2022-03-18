FROM mcr.microsoft.com/mssql/server

# Switch to root user for access to apt-get install
USER root

# Install dos2unix
RUN apt-get -y update  && \
        apt-get install -y dos2unix

# SA_PASSWORD should be a secret
ENV SA_PASSWORD=!T3ST@1539
ENV ACCEPT_EULA=Y
ENV MSSQL_PID=Express

RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app
COPY . .

# Grant permissions for the import-data script to be executable
RUN chmod +x /usr/src/app/import_data.sh

EXPOSE 8080

# Switch back to mssql user and run the entrypoint script
USER mssql
ENTRYPOINT ["/bin/bash", "./entrypoint.sh"]