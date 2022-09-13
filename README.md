# Department-Management-System

This is an ASP.NET Core Web API "monolith" project (.NET 6) that operates with a tree-like structure of departments. The application is implemented according to the onion architecture. The ASP.NET Core project uses REST API.
The application uses the following third-party libraries:
1. Entity Framework Core.
2. NuGet packages for PostgreSQL DB.
3. Swagger UI
4. AutoMapper
5. DevExpress Office File API.

Initial structure of deparments and users is stored in an Excel file (.XLSX). The loaded data is stored in a PostgreSQL DB.

# Implementation Details

The **Departments** Controller has CRUD actions for the Department entities. Also, the controller has the GetDepartmentsByIdAsync(parentId) method that returns sub-departments for the specified main department and the GetUsersAndPositionsByDepartmentAsync() method that returns information about all the departments.
The **ImportFile** Controller has an endpoint for importing data from an Excel file located in the "/App_Data/docs" folder.
