# ConfirmationServiceDemo

Confirmation Service Demo simulates that it receives a bunch of faulty tickets, 
connect to them, validate configuration, sync and params.

After all of this, CS takes actions based on defined bussiness rules.

## Requisites

You need to have installed:

* Docker
* Docker Compose

## How do I run this?

Run docker-compose up --build command, then wait until all services are available.

Once all microservices are running, go to this URI:

* http://localhost:8000/hanfire/

Here you will be redirected to a dashboard where you can track processes that are running as Jobs.
