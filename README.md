  # TOOLIRENT API #

  ---
    School assignment, HT-25
    Avancerad systemutveckling ASP.NET
    SUT24 (Systemutveckling .NET)
    Campus Varberg
  ---

<!-- ABOUT THE PROJECT -->
## About The Project


### Built With
* C#
* ASP.NET Core
* Entity Framework Core
* MySQL
#### Also using
* Automapper
* FluentValidation





<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple example steps.

### Prerequisites


* NuGet packages:
  ```sh
  Microsoft.EntityFrameworkCore (8.0.20)
  Microsoft.EntityFrameworkCore.SqlServer (9.0.8)
  Microsoft.EntityFrameworkCore.Tools (9.0.8)
  Microsoft.AspNetCore.Identity.EntityFrameworkCore (8.0.20)
  AutoMapper (15.0.1)
  FluentValidation (12.0.0)
  FluentValidation.DependencyInjectionExtensions (12.0.0)
  Microsoft.AspNetCore.Authentication.JwtBearer (8.0.20)
  Swashbuckle.AspNetCore (6.6.2)
  ```

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/github_username/repo_name.git
   ```
2. Change git remote url to avoid accidental pushes to base project
   ```sh
   git remote set-url origin github_username/repo_name
   git remote -v # confirm the changes
   ```

forts...



<!-- USAGE EXAMPLES -->
## Usage

This API can be used to manage a catalogue of tools available for lending by registered members. 

forts...




## Endpoint Documentation

    
# TooliRent API Endpoints

> **Base URL:** `/api/{Controller}`  
> All endpoints require JWT authentication. Role-based access is enforced (`Admin`, `User`).  
> Responses are in JSON format.

---

## ToolController

### Get All Tools
```
GET /api/Tool
```
**Roles:** Admin  
Returns a list of all tools.

---

### Get Tool Details
```
GET /api/Tool/{id}
```
**Roles:** Admin, User  
Returns details for a specific tool.

---

### Get Tool Overview
```
GET /api/Tool/Overview
```
**Roles:** Admin, User  
Returns a cached, shorthand overview of all tools.

---

### Filter/Search Tools
```
POST /api/Tool/FilterSearch
```
**Roles:** Admin, User  
Returns tools matching filter criteria.

---

### Create Tool
```
POST /api/Tool
```
**Roles:** Admin  
Creates a new tool.

---

### Update Tool
```
PUT /api/Tool/{id}
```
**Roles:** Admin  
Updates an existing tool.

---

### Delete Tool
```
DELETE /api/Tool/{id}
```
**Roles:** Admin  
Deletes a tool.

---

### Change Tool Availability
```
PUT /api/Tool/ChangeAvailability/{id}
```
**Roles:** Admin, User  
Toggles the availability status of a tool.

---

## ToolTypeController

### Get All Tool Types
```
GET /api/ToolType
```
**Roles:** Admin, User  
Returns all tool types.

---

### Get Tool Type Details
```
GET /api/ToolType/{id}
```
**Roles:** Admin  
Returns details for a specific tool type.

---

### Create Tool Type
```
POST /api/ToolType
```
**Roles:** Admin  
Creates a new tool type.

---

### Update Tool Type
```
PUT /api/ToolType/{id}
```
**Roles:** Admin  
Updates an existing tool type.

---

### Delete Tool Type
```
DELETE /api/ToolType/{id}
```
**Roles:** Admin  
Deletes a tool type.

---

## CategoryController

### Get All Categories
```
GET /api/Category
```
**Roles:** Admin, User  
Returns all categories.

---

### Create Category
```
POST /api/Category
```
**Roles:** Admin  
Creates a new category.

---

### Update Category
```
PUT /api/Category/{id}
```
**Roles:** Admin  
Updates an existing category.

---

### Delete Category
```
DELETE /api/Category/{id}
```
**Roles:** Admin  
Deletes a category.

---

## BookingController

### Get All Bookings
```
GET /api/Booking
```
**Roles:** Admin  
Returns all bookings.

---

### Get Booking Details
```
GET /api/Booking/{id}
```
**Roles:** Admin  
Returns details for a specific booking.

---

### Get Late Bookings
```
GET /api/Booking/late/{late}
```
**Roles:** Admin  
Returns bookings that are late.

---

### Get Bookings by User
```
GET /api/Booking/user/{userId}
```
**Roles:** Admin, User  
Returns bookings for a specific user.

---

### Create Booking
```
POST /api/Booking
```
**Roles:** Admin, User  
Creates a new booking.

---

### Update Booking
```
PUT /api/Booking/{id}
```
**Roles:** Admin, User  
Updates an existing booking.

---

### Update Late Bookings
```
PUT /api/Booking/late/update
```
**Roles:** Admin  
Checks and updates status for late bookings.

---

### Delete Booking
```
DELETE /api/Booking/{id}
```
**Roles:** Admin  
Deletes a booking.

---

### Complete Booking (Return)
```
PUT /api/Booking/return/{id}
```
**Roles:** Admin  
Marks a booking as returned.

---

### Extend Booking
```
PUT /api/Booking/extend/{id}?newEndDate={date}
```
**Roles:** Admin  
Extends the end date of a booking.

---

### Cancel Booking
```
PUT /api/Booking/cancel/{id}
```
**Roles:** Admin, User  
Cancels a booking.

---

### Pickup Booking
```
PUT /api/Booking/pickup/{id}
```
**Roles:** Admin  
Marks a booking as picked up.

---

## LateFeeController

### Get All Late Fees
```
GET /api/LateFee
```
**Roles:** Admin  
Returns all late fees.

---

### Get Late Fees by User
```
GET /api/LateFee/{id}
```
 
