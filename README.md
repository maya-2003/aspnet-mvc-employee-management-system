# Employee & Department Management System

A layered ASP.NET MVC web application for managing employees and departments.  
The project is built using a clean 3-layer architecture that separates the user interface, business logic, and data access responsibilities.

## Overview

This application provides a structured employee and department management system with CRUD operations, authentication-related structure, and a maintainable architecture.

The project demonstrates how ASP.NET MVC can be organized into separate layers using services, repositories, DTOs, view models, and mapping profiles instead of placing all logic directly inside controllers.

## Features

- Employee management
  - Add employees
  - View employee records
  - Update employee information
  - Delete employees

- Department management
  - Add departments
  - View department records
  - Update department information
  - Delete departments

- Account and authentication structure
- MVC-based presentation layer with controllers, views, and view models
- Business logic layer using services, DTOs, factories, and mapping profiles
- Data access layer using models, repositories, and database context structure
- Static file support through `wwwroot`
- Clean separation of concerns across multiple projects

## Tech Stack

- ASP.NET MVC
- C#
- Entity Framework
- SQL Server
- LINQ
- HTML
- CSS
- Bootstrap
- JavaScript

## Project Structure

```text
aspnet-mvc-employee-management-system
│
├── MVCS3PL
│   ├── Controllers
│   ├── Models
│   ├── Utilities
│   ├── ViewModels
│   ├── Views
│   ├── wwwroot
│   ├── Program.cs
│   └── appsettings.json
│
├── MVCS3.BLL
│   ├── DTOs
│   ├── Factories
│   ├── MappingProfiles
│   └── Services
│
├── MVCS3.DAL
│   ├── Data
│   ├── Models
│   └── Repositories
│
└── MVCS4Sol.sln
