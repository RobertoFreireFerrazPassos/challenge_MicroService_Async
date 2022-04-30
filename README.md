

# Microservices using asynchronous communication and api gateway


## Architecture solution

<p align="center">
  <img src="https://github.com/RobertoFreireFerrazPassos/challenge_MicroService_Async/blob/master/appshop/modelagem.png?raw=true">
</p>


## Pending/next steps:

1 - Fix Api Gateway to point all endpoints correctly

2 - Create more tests

3 - Create Credit Card Validation 

4 - Create Authorization with token

5 - Insert Observer Design Pattern to listen to consumed events 

6 - Add log messages structure 

7 - Change Services and Repositories Methods to Async


## Notes

RabbitMQ
http://localhost:15672/
username and password: guest / guest

In docker containers such as ApiAppShop.Presentation, it is possible to see the console messages


## Steps to complete test using swagger 

(http://localhost:9002/swagger/index.html)

1 - Create user

Endpoint ​/User​/SignIn

```json
{
    "name": "Alberto Junior",
    "cpf": "",
    "birthDate": "1993-12-25T17:44:35.665Z",
    "gender": 0,
    "address": null,
    "creditCard": null
}
```

Debug to find id just before persist data in mongodb

ex: userId = d426d088-af6d-4207-80ec-20275a3aa0d1

Or get userId accessing mongodb database directly

See topic Access Mongodb Container data.

2 - Test user created

Endpoint /User/LogIn/{userId}

3 - Create App

Endpoint /App/setapp

```json
{
  "name": "Sales Manager",
  "price": 49
}
```

4 - Test app created and get id of app created

Endpoint /App/getapps

ex: appid = "id": "f19ddaba-e0a5-45fc-9b5b-a2dd99fa600b",


5 - Buy App

Endpoint /App/purchase

```json
{
    "appId": "f19ddaba-e0a5-45fc-9b5b-a2dd99fa600b",
    "userId": "d426d088-af6d-4207-80ec-20275a3aa0d1",
    "saveCreditCard": true,
    "creditCard": {
        "name": "Alberto Junior",
    "number": "5496374407457455",
    "cvv": "123",
    "expirationDateMMYYYY": "122025"
  }
}
```

6 - Test App bought

Endpoint /App/getappsbyuser/{userid}

7 - Redo test 2 (Test user created)

it must have the creditcard new information provided during app purchase

## Access Mongodb Container data

1 - Using docker interface, access Mongodb Container

2 - In command prompt, use commands:

mongo

show dbs

use DB

db.getCollectionNames()

db.Users.find()


3 - Get _id 

ex:

{ "_id" : "d2f1a07c-e503-49a4-bae3-dd2055dd502a", "Name" : "Alberto Junior", "Cpf" : "", "BirthDate" : ISODate("1993-12-25T17:44:35.665Z"), "Gender" : 0, "CreditCard" : null, "Address" : null }


## Simple request to test docker, api gateway using ocelot, api appshop, cache using redis, rabbitmq, masstransit and creditcard console app

1 - Run docker compose on visual studio.

note: set docker-compose as start up project

2 - Import and run request on Postman:

example:

curl --location --request POST 'http://localhost:9001/purchase' \
--header 'accept: */*' \
--header 'Content-Type: application/json' \
--data-raw '{
    "appId": "d24a3c0d-117a-4637-a078-2d386d7a6952",
    "userId": "8f681848-75c5-4c09-8b01-62ab2713b2b2",
    "saveCreditCard": true,
    "creditCard": {
        "name": "Adalto Jarbas Lopes",
        "number": "5496374407457455",
        "cvv": "123",
        "expirationDateMMYYYY": "122025"
    }
}'
