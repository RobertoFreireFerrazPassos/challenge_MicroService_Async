

# Microservices using asynchronous communication and api gateway


# Architecture solution

<p align="center">
  <img src="https://github.com/RobertoFreireFerrazPassos/challenge_MicroService_Async/blob/master/appshop/modelagem.png?raw=true">
</p>


# Notes

RabbitMQ
http://localhost:15672/
username and password: guest / guest

In docker containers such as ApiAppShop.Presentation, it is possible to see the console messages


# Simple request to test docker, api gateway, api appshop, rabbitmq, masstransit and creditcard console app

1 - Run docker compose on visual studio.

2 - Import and run this request on Postman:

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


# Steps to complete test using swagger (http://localhost:9002/swagger/index.html)

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


