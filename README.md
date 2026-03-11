# Fridge API

ASP.NET Core Web API for managing fridges and products.

## Technologies

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger

## Architecture

The project uses layered architecture:

- Controllers
- Services
- Repositories
- Data (DbContext)
- Core Models

## Features

- Get all fridges
- Get fridge by id
- Create products
- Update products
- Delete products
- Add products to fridge
- Remove products from fridge
- Update product quantity
- Call stored procedure when quantity is 0

## Project structure

```text
Fridge.API
├── Controllers
├── Services
├── Repositories
├── Data
└── Program.cs

Fridge.Core
└── Models
