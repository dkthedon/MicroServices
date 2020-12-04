FROM mysql

COPY ./InitDb.sql /docker-entrypoint-initdb.d/
EXPOSE 3306