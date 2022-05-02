

# Microservices using asynchronous communication and api gateway


## Architecture solution

<p align="center">
  <img src="https://github.com/RobertoFreireFerrazPassos/challenge_MicroService_Async/blob/master/appshop/modelagem.png?raw=true">
</p>


## Pending/next steps:

1 - Fix Api Gateway to point all endpoints correctly

2 - Create more tests with mocks

3 - Add log messages structure 

4 - For update or insert in Cache and Database, add roll back logic if either one fails

5 - Add Fluent validation

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
    "Password" : "123456",
    "birthDate": "1993-12-25T17:44:35.665Z",
    "gender": 2,
    "address": null,
    "creditCard": null
}
```

To see user created in database, see topic Access Mongodb Container data.

2 - Test user created and add token to SwaggerUI

Endpoint /Auth/LogIn

```json
{
  "name": "Alberto Junior",
  "password": "123456"
}
```

Copy Token from response and in Authorize button for SwaggerUI paste bearer + token
From now on, all request that need authorization will have the bearer header authorization

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

{ "_id" : "58f3d12e-6886-495e-81e6-232fa4648d30", "Name" : "Alberto Junior", "Cpf" : "", "Role" : 1, "PasswordHash" : BinData(0,"llRJDdobMjUt56hUa5JImH0j3AtTDotZO9aT0nZyRATiFqlidQT4suu9bQvHxvQ76C4tbgkmENBXVFAewEgUAg=="), "PasswordSalt" : BinData(0,"c/tWoB2ndiMqx5Nx7fJRtV+9k6GXVhqmdhzisvJhg0K3RpzVpTL6U7vHS1llhcj7u4iNVX+yrM7HF/teiksVSoMYnlpDW2CFi3kGn1J1tGkGZ92nvKUnKk8gZ532h+ypkz9y0/X1ryxAXJ+QRTy9iLJ3TdYIHFxkldKegdy9mws="), "BirthDate" : ISODate("1993-12-25T17:44:35.665Z"), "Gender" : 2, "CreditCard" : null, "Address" : null }


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

## References:

[1] Strategies to handle partial failure:

https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/partial-failure-strategies