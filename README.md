# Microservice-Client-Project

This project demonstrates a full-duplex Microservice-Client-Project architecture implemented using various technologies including Docker, SonarQube, Eureka, Ocelot, RabbitMQ, Docker-Compose-File, and SQL Server.

## Description

The project showcases the implementation of a microservices architecture with bidirectional communication, facilitated by Docker containers. It leverages various technologies to build a scalable and resilient system.

## Technologies Used

- Docker: For containerization and deployment of microservices.
- SonarQube: For continuous code quality analysis.
- Eureka: For service discovery and registration.
- Ocelot: For API Gateway functionality.
- RabbitMQ: For asynchronous messaging between microservices.
- Docker Compose: For defining and running multi-container Docker applications.
- SQL Server: For data storage and retrieval.

## Docker Hub Repository

You can find the Docker images for this project on Docker Hub:

[dpcode72/microservice_client_project](https://hub.docker.com/repository/docker/dpcode72/microservice_client_project/general)

### You can pull the Docker images using the following command
- docker pull dpcode72/microservice_client_project:1.0
- docker pull dpcode72/microservice_client_project:2.0
- docker pull dpcode72/microservice_client_project:3.0
- docker pull dpcode72/microservice_client_project:4.0
-docker pull dpcode72/microservice_client_project:5.0
-docker pull dpcode72/microservice_client_project:6.0





<img width="957" alt="image" src="https://github.com/Deepak17460/Microservice-Client-Project/assets/99780500/1505139c-9398-4251-9371-5c48fa08a64c">

### All Service are Registered on EUREKA SERVER As YOU CAN SEE BELOW IMAGE

<img width="953" alt="image" src="https://github.com/Deepak17460/Microservice-Client-Project/assets/99780500/8a9898ae-2a37-4bd1-a019-e9c2b90175b4">

### Once you will pull image using above commands then you can run images on CLI using below commands

- docker run -p <wish-port>:80 -d(if not wish to see logs) <name of image which is pulled>
## And so on.............

### After running above command on CLI(Commnad-Line Interface) as you can aspect to see the logs of images below

<img width="951" alt="image" src="https://github.com/Deepak17460/Microservice-Client-Project/assets/99780500/f335b492-3c3c-47d7-987c-17424788db09">


### After running above command you can see SWAGGER UI on Browser by writing http://localhost(Host Machine IP):<wish port>//<service route//upstream url


<img width="949" alt="image" src="https://github.com/Deepak17460/Microservice-Client-Project/assets/99780500/0003edb3-9473-4541-b871-f9db879f2c01">



