<p align="center">
  <img src="https://github.com/RobertoFreireFerrazPassos/challenge_MicroService_Async/blob/master/appshop/modelagem.png?raw=true">
</p>


RabbitMQ
http://localhost:15672/
username and password: guest / guest


Swagger to test some endpoints
http://localhost:9002/swagger/index.html


Request to test docker, api gateway, api appshop, rabbitmq, masstransit and creditcard console app

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

