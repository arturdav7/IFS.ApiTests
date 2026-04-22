# IFS QA Test Automation Framework

Automated test suite for JSONPlaceholder REST API (https://jsonplaceholder.typicode.com).
Built with C# / .NET 8 / NUnit / HttpClient.

## Project Structure

IFS.ApiTests/
├── Clients/
│   └── ApiClient.cs            # HttpClient wrapper with logging
├── Helpers/
│   ├── ApiSettings.cs          # Settings model
│   └── ConfigurationHelper.cs  # Loads appsettings.json
├── Models/
│   └── Post.cs
├── Tests/
│   ├── BaseTest.cs             # Shared setup and teardown
│   └── PostsTests.cs           # All /posts test cases
└── appsettings.json            # Configuration

## Prerequisites

.NET 8 SDK - https://dotnet.microsoft.com/download/dotnet/8.0

## Setup

git clone <your-repo-url>
cd IFS.ApiTests
dotnet restore

## Run Tests

dotnet test

## Configuration

Base URL is configured in appsettings.json and can be changed without touching the code.

## Test Coverage

GET /posts - 200 OK, count = 100, field validation
GET /posts/{id} - correct post returned, 404 for non-existent id
POST /posts - 201 Created, response matches submitted data
PUT /posts/{id} - 200 OK, response contains updated values
DELETE /posts/{id} - 200 OK