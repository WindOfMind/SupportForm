# Support Form

## Solution

The solution consists of 2 parts - back and front. 

Back-end part is ASP.NET Core 3.1 API with 1 endpoint for posting support messages (https://github.com/WindOfMind/SupportForm/tree/main/SupportForm.API). 
It has built-in validation for posted data.
All posted message are persisted in the memory; all requests all logged.
Also, a swagger page ([host]/api/documentation) is available with endpoints' and models' descriptions.

Front-end part is a Angular (v. 10) single-page application (https://github.com/WindOfMind/SupportForm/tree/main/SupportForm.Frontend/ClientApp). 
Basically, it has only 1 component with the support form where you can create your support inquiry.
Most of the fields are mandatory for filling. 
Most elements (e.g. input fields, buttons, etc.) are used from Angular Material for making UI looks nice and responsive.
Also, all input fields have built-in validation with error hints.
While the request to back-end is being performed, the spinner is displayed instead of submit button.

## How to run

As this is the assignment task, not real production application, local deployment is set up separately for front-end and back-end parts.
As an advantage, you can develop and deploy these parts separately.

Actions to run:
1. Build and run API solution https://github.com/WindOfMind/SupportForm/blob/main/SupportForm.sln (Preferably to use a console SupportForm.API as a running configuration in VS).
2. Build and run Angular app. Before running app the first time, please run `npm install`.

### Back-end
ASP.NET Core API service can be run in different ways:
- IIS Express
- Console app (most recommended for local development)
- Docker (please, check if you use Windows and have installed https://docs.docker.com/docker-for-windows/)

Default port for running an API is 5000. 
If you need another, please also update front-end app config (https://github.com/WindOfMind/SupportForm/blob/main/SupportForm.Frontend/ClientApp/src/environments/environment.local.ts).
Docker file is set up for Linux containers.

### Front-end
Frond-end app for development is served by Angular Cli.
For frond-end local deployment please look at https://github.com/WindOfMind/SupportForm/blob/main/SupportForm.Frontend/ClientApp/README.md.
You can run it, for example, with VS Code (https://code.visualstudio.com/).
Keep in mind, that for local deployment of front-end app you should have:
- Node.js (https://nodejs.org/en/)
- Angular CLI (https://cli.angular.io/)
- Check if you are using a default port (4200) for serving app with Angular. If not - please update the port in the startup (https://github.com/WindOfMind/SupportForm/blob/main/SupportForm.API/Startup.cs).

## What's next

Currently, you have to deploy API and Angular app separately in manual mode. 
In the case of a real application it makes sense to automate deployment process (e.g. Docker containers, a separate server for the front app).

### Back-end part:

- Currently, all validation logic is implemented with ASP.NET Core validation attributes, not in the SupportMessage model https://github.com/WindOfMind/SupportForm/blob/main/SupportForm.Domain/SupportMessage.cs. 
In the case of the service growing and model is used in many places it makes sense to keep all validation logic (which is business logic) in the model. It will allow to keep all connected logic in one place and allow to avoid invalid state of the object.
- Memory persistance is implemented with singleton service for demonstration purpose. But in real service it's better to avoid singleton services and stick with scoped type of DI.
- Logging in modern apps is very important. That's why it's crucial to precisely specify what, how and when we need to log. For demonstration, this service has a logging middleware for each request, also logging for possible exception. 
Though, logging of the exceptions can be done differently, e.g. using exception filters (https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-3.1#exception-filters).
- Unit test and integration tests. So far there is little business logic, hence it makes more sense to proceed with integration tests and cover all ASP.NET pipelines.

### Front-end part:

- Though the front-end part is implemented as a SPA. Sometimes, for example for simple support forms, it makes more sense to use simpler tools with server-side rendering (e.g. Razor/Blazor pages), and not get overhead of setting all Angular infrastructure for trivial cases. 
But, as I am more experienced with Angular apps, I decided to proceed with it.
- Messages for success and fail posting don't look nice. Probably, it makes sense to beautify it.
- The checkbox for terms has a such default behavior, so while you don't click on it, the error message will be shown. In the real case it makes sense to do it differently.
- Also, it is possible to consider a cancel action for posting a message.
- For proper control of all UI elements it's better to avoid using Angular Material.
- If the list of inquiry types can be often updated, it will make sense to fetch this data from the API.
- Unit tests, e2e tests.

