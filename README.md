# Camunda .NET Core PoC
Proof of Concept project that shows how we can use Camunda BPMN Platform in .NET Core applications.

## Hiring Heroes Business Case

Our project aims to support a fictional Hire Heroes company in their business of allowing customers to hire superheroes with a given set of superpowers. The diagram below presents the business process of hiring a superhero using BPMN notation.

<p align="center">
    <img alt="Hire Heroes Business Process" src="https://raw.githubusercontent.com/asc-lab/camunda-dotnetcore-poc/master/images/heroes-bpmn.png"/>
</p>

The process is started by a customer using **Customer Portal**. The customer sends an order with information about what superpower is needed and for which period. The first step in the process is an offer preparation by a salesman. Note that tasks for humans are depicted as boxes with a little user icon. Salesman uses **Sales Portal** to view the list of their tasks. When they see a task for offer preparation they must choose one of the available superheroes or reject an order due to a lack of free superheroes. If the offer is created then the next stop is a task for a customer to review and accept or reject and offer. When the user rejects the offer, then the process finishes. If the offer is accepted then we have a so-called service task to create an invoice. Service tasks are the ones with little cogs icon and they are tasks to be carried out not by humans but by computer systems. In our case, our project will subscribe to the process engine to get a notification when there is a need to generate an invoice. Once the invoice is generated and sent to the client, the process will wait for payment confirmation. In our sample solution, this step is performed manually by the salesman in the Sales Portal by a salesman. When payment is confirmed in the application, a message is sent to the process engine to complete the process. If payment is not confirmed within 5 days the process engine will execute the Cancel Order task, so the whole order and related hero assignment are canceled.  
           
## Architecture

Solution components:

<p align="center">
    <img alt="Hire Heroes Architecture" src="https://raw.githubusercontent.com/asc-lab/camunda-dotnetcore-poc/master/images/heroes_arch.png"/>
</p>

**Customer Portal** is a single-page Angular application. It allows customers to log in, view their orders and tasks, review offers, and accept or reject it, view notifications, and monitor orders status. It communicates via REST HTTP with SSO and HeroesForHire microservices.

**Sales Portal** is also a single-page Angular application. It allows salesmen to log in, view their tasks based on the information from Camunda processes and HeroesForHire service, create offers, notify customers that no heroes are available for their orders, view invoices, and confirm payments. It communicates via REST HTTP with SSO and HeroesForHire microservices.

**SSO** is a simple authentication service that is used for the login process. It has a hard-coded list of users and passwords. When the user is successfully authenticated it returns JWT token with given user roles and in case of a customer additional information that identifies the user’s company.

**HeroesForHire** is our main application service. It provides an API that both portals use to manage users’ tasks, manage orders, offers, invoices, notifications, and heroes assignments.
It is a typical .NET Core Web API application written in C#. It uses Entity Framework Core for data access. Its internal implementation follows CQS principles and Domain-Driven Design tactical patterns. All API functions are exposed from Web API controllers, while their implementations are nicely encapsulated in command/query handlers with a little help of the excellent MediatR library. For interactions with Camunda two additional external libraries are used: Camunda REST API Client and Camunda Worker - we will discuss them in more detail later. Both of these libraries wrap Camunda REST APIs with C# APIs and their help greatly simplifies the way we can interact with Camunda from .NET Core app.

**Camunda Engine** is an instance of Camunda BPM platform running in a Docker container, with its REST API on the top and Cockpit, Admin, and Tasklist applications. 

There are also two databases involved: PostgreSQL database used by our main service HeroesForHire and H2 in-memory database used by Camunda Engine.


## Run Instruction

### Dockerized Clients run (avoids npm hell)

- **Prerequisites**
    - You need to have docker-compose installed (tested with 1.22.0 version)
    - You need .net core 3.1.300 installed
    
- **Running instruction**
    - to run Camunda and Postgres execute from solution root directory
    `docker-compose -f infrastructure.yml up`
    - navigate in your browser to `http://localhost:8080/camunda/` and ensure you can login as **demo** with password **demo**
    - still in root directory of solution (the one with .sln file in) restore `dotnet restore` and build `dotnet build` .net solution
    - go to Sso directory (`cd Sso`) and run Sso microservice `dotnet run`
    - go to HeroesForHire directory (`cd HeroesForHire`) and run HeroesForHire microservice (`dotnet run`)
    - go to customer-portal directory (`cd  customer-portal`). Build and run docker image `docker build -t customer-portal .` `docker run -p 4200:80  customer-portal`. You should be able now to go to http://localhost:4200 and login as Tim.Client with password pass, select New Order and submit one
    - go to sales-portal directory (`cd sales-portal`). Build docker image (`docker build -t sales-portal .`) and run it (`docker run -p 4220:80 sales-portal`). You should be able now to go to http://localhost:4220 and login as Jim.Salesman with password pass, view list of tasks, claim one and create an offer
                 

### Manual run

- **Prerequisites**
    - You need to have docker-compose installed (tested with 1.22.0 version)
    - You need .net core 3.1.300 installed
    - You need nodeJs (tested with v10.16.3) and npm (tested with 6.9.0) installed
    - You need Angular CLI installed (tested with 9.1.1 version)
    
- **Running instruction**
    - to run Camunda and Postgres execute from solution root directory
    `docker-compose -f infrastructure.yml up`
    - navigate in your browser to http://localhost:8080/camunda/ and ensure you can login as demo with password demo
    - still in root directory of solution (the one with .sln file in) restore `dotnet restore` and build `dotnet build` .net solution
    - go to Sso directory (`cd Sso`) and run Sso microservice `dotnet run`
    - go to HeroesForHire directory (`cd HeroesForHire`) and run HeroesForHire microservice (`dotnet run`)
    - go to customer-portal directory (`cd  customer-portal`), install dependencies (`npm install`) and run (`ng serve`). You should be able now to go to http://localhost:4200 and login as Tim.Client with password pass, select New Order and submit one
    - go to sales-portal directory (`cd sales-portal`), install dependencies (`npm install`) and run (`ng serve`). You should be able now to go to http://localhost:4220 and login as Jim.Salesman with password pass, view list of tasks, claim one and create an offer
