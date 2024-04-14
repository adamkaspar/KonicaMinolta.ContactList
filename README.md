# KonicaMinolta.ContactList

ContactList is a .NET 8 project, that offers basic CRUD operations for management of the person's contact list. Project implements service repository pattern.

Whole solution is divided into following structure:
```
├── KonicaMinolta.ContactList.sln
│   ├── 01 - Application
│   │     ├── KonicaMinolta.ContactList.Api.csproj
│   │     ├── KonicaMinolta.ContactList.Business.csproj
│   │     ├── KonicaMinolta.ContactList.Data.csproj
│   ├── 02 - Shared
│   │     ├── KonicaMinolta.Shared.Domain.csproj
│   │     ├── KonicaMinolta.Shared.Entities.csproj
│   ├── 03 - Tests
│         ├── KonicaMinolta.ContactList.Tests.csproj
└── .gitignore
```
### KonicaMinolta.ContactList.Api

This project contains basic ContactListController as a starting point of the whole application and ExceptionHandlingMiddleware as a global place, where all errors are catched and handled. In a development mode, there is support for Swagger UI.

### KonicaMinolta.ContactList.Business

Business project contains whole business logic. Entry point is ContactListService, that derives all basic CRUD methods from generic BaseService.

### KonicaMinolta.ContactList.Data

Data project contains database layer and provides data services to upper application methods. Currently, there is one main repository - ContactRepository, that derives all operations from BaseRepository. Currently, Entity Framework 8.0.4 is used as ORM framework and project applies database code-first migration approach. Application use in-memory database, what is sufficient for our development purposes, but it could be changed in Program.cs.

### KonicaMinolta.Shared.Domain

This project contains shared POCO objects, that could be used across different projects.

### KonicaMinolta.Shared.Entities

This project contains shared business entity objects, that could be used across different projects.

### KonicaMinolta.ContactList.Tests

Tests project contains unit and integration tests, that covers basic test scenarios. xUnit is used as default test framework, Moq is used as mocking library and FluentAssertions is used as an assertion framework. To be able run integration tests, KonicaMinolta.ContactList.Api project must be up and running.

## Installation

KonicaMinolta.ContactList runs on .NET 8, so .NET 8 SDK is necessary to [install](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).

To run the aplication, do the following steps:

1. open .NET CLI and go to the KonicaMinolta.ContactList.Api folder
2. type and run following command **dotnet run**
3. project should be built and restored and solution should be hosted on http://localhost:5121
4. Swagger UI is supported in development mode (ASPNETCORE_ENVIRONMENT env. variable is set to Development) as well on http://localhost:5121/swagger

To run unit and integration tests, do the following steps:

1. run the application, see previous steps
2. open .NET CLI and go to the KonicaMinolta.ContactList.Tests folder
3. type and run following command **dotnet test**

Launch settings and default ports is possible to change in KonicaMinolta.ContactList.Api\Properties\launchSettings.json file
## Usage

There is multiple endpoints reacheable on path: /api/v1/ContactList:

### POST Add

This endpoint add new contact into the database. Endpoint is possible to call with POST verb and accepts payload in following json format structure:

```
{
  "name": "Adam",
  "surname": "Kaspar",
  "age": 35,
  "phone": 123456789,
  "isActive": true
}
```
Where:

| Property    | Comment |
| --------  | ------- |
| name | First name.     |
| surname    | Surname.    |
| age    | Age.    |
| phone    | Contact's phone number.   |
| isActive  | True or false value, that signals if contact is still active in the database.    |

Endpoint returns json response with following structure:

```
{
  "id": 1
  "name": "Adam",
  "surname": "Kaspar",
  "age": 35,
  "phone": 123456789,
  "isActive": true
}
```
Where:

| Property    | Comment |
| --------  | ------- |
| id | Contact's Id.     |
| name | First name.     |
| surname    | Surname.    |
| age    | Age.    |
| phone    | Contact's phone number.   |
| isActive  | True or false value, that signals if contact is still active in the database.    |

### GET FindById/{id}

This endpoint gets contact, located by provided id. Endpoint is possible to call with GET verb and accepts one parameter - contact's Id.

Endpoint returns json response with following structure:

```
{
  "id": 1
  "name": "Adam",
  "surname": "Kaspar",
  "age": 35,
  "phone": 123456789,
  "isActive": true
}
```
Where:

| Property    | Comment |
| --------  | ------- |
| id | Contact's Id.     |
| name | First name.     |
| surname    | Surname.    |
| age    | Age.    |
| phone    | Contact's phone number.   |
| isActive  | True or false value, that signals if contact is still active in the database.    |

### GET GetAll

This endpoint gets all contacts - active and inactive.

Endpoint returns json payload with following structure:

```
[
    {
        "id": 1
        "name": "Adam",
        "surname": "Kaspar",
        "age": 35,
        "phone": 123456789,
        "isActive": true
    }
]
```
Where:

| Property    | Comment |
| --------  | ------- |
| id | Contact's Id.     |
| name | First name.     |
| surname    | Surname.    |
| age    | Age.    |
| phone    | Contact's phone number.   |
| isActive  | True or false value, that signals if contact is still active in the database.    |

### GET GetAll/{isActive}

This endpoint gets all contacts, that could be active or inactive, based on input parameter - isActive.

Endpoint returns json response with following structure:

```
[
    {
        "id": 1
        "name": "Adam",
        "surname": "Kaspar",
        "age": 35,
        "phone": 123456789,
        "isActive": true
    }
]
```
Where:

| Property    | Comment |
| --------  | ------- |
| id | Contact's Id.     |
| name | First name.     |
| surname    | Surname.    |
| age    | Age.    |
| phone    | Contact's phone number.   |
| isActive  | True or false value, that signals if contact is still active in the database.    |

### GET PageAll/{skip}/{take}

This endpoint provides pagination support. It returns all contacts between specified range. This endpoint accepts 2 parameters - skip (how many entries we should skip) and take (how many entries we should take).

Endpoint returns json payload with following structure:

```
[
    {
        "id": 1
        "name": "Adam",
        "surname": "Kaspar",
        "age": 35,
        "phone": 123456789,
        "isActive": true
    }
]
```
Where:

| Property    | Comment |
| --------  | ------- |
| id | Contact's Id.     |
| name | First name.     |
| surname    | Surname.    |
| age    | Age.    |
| phone    | Contact's phone number.   |
| isActive  | True or false value, that signals if contact is still active in the database.    |

### PUT Update/{id}

This endpoint updates contact entry in the database. Endpoint accepts PUT verb and expects 2 parameters - contact's Id and json payload with following structure:

```
{
  "name": "Adam",
  "surname": "Kaspar",
  "age": 35,
  "phone": 123456789,
  "isActive": true
}
```
Where:

| Property    | Comment |
| --------  | ------- |
| name | First name.     |
| surname    | Surname.    |
| age    | Age.    |
| phone    | Contact's phone number.   |
| isActive  | True or false value, that signals if contact is still active in the database.    |

Endpoint returns json response with following structure:

```
[
    {
        "id": 1
        "name": "Adam",
        "surname": "Kaspar",
        "age": 35,
        "phone": 123456789,
        "isActive": true
    }
]
```
Where:

| Property    | Comment |
| --------  | ------- |
| id | Contact's Id.     |
| name | First name.     |
| surname    | Surname.    |
| age    | Age.    |
| phone    | Contact's phone number.   |
| isActive  | True or false value, that signals if contact is still active in the database.    |

### DELETE Remove/{id}

This endpoint removes contact, specified by delivered id. Endpoint returns true or false, based on operation success.

### Exceptions

In case of any error, json message with following structure is returned back:

```
{
  "correlationId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "statusCode": 500,
  "message": "string"
}
```
Where:

| Property    | Comment |
| --------  | ------- |
| correlationId | Error id. Use this error id to find out more information about error in application log.     |
| statusCode    | Error status code    |
| message    | Basic information about an error.    |

Application support Swagger UI in development mode on http://localhost:5121/swagger, so it could be tested from this interface as well.

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)