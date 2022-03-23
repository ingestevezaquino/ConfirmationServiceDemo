# ConfirmationServiceDemo

Confirmation Service Demo simulates a system that receives a bunch of faulty tickets, 
connect to their actual equipments / facilities directly, validate configuration, sync and params.

After all of this, CS takes actions based on defined bussiness rules.

## Requisites

You need to have installed:

* Docker
* Docker Compose

## How do I run this?

Run command: 

> docker-compose up --build 

Then wait until all services are available.

Once all microservices are running, go to this URI:

* http://localhost:8000/hanfire/

This will open a new tab with a job's dashboard. 
Here, you can track scheduled processes.

## NOTE

Frontend communication with backend has not been resolved yet.
Even though, it's running inside a container and share same network with microservices, 
it does not resolve docker dns automatically.
