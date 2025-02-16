# GovConnect Documentation

## Overview
GovConnect is a comprehensive system designed to facilitate the process of issuing, renewing, and managing various government-related services, including driving licenses. The system adheres to SOLID principles, leverages dependency injection, and follows a clean architecture pattern to ensure maintainability and scalability.

## Skills & Technologies Used
- **Repository Pattern**
- **Dependency Injection** (Interface-based services)
- **Service Layer** for business logic
- **Validators** for service-specific checks
- **SOLID Principles**
- **Unit Testing**
- **T-SQL** (Table-Valued Functions, Stored Procedures)
- **Entity Framework Core (EF Core)**
- **LINQ**
- **Publish-Subscribe Pattern** (For automating workflows upon specific events)
- **DTOs** (Data Transfer Objects)
- **Generic Services and Interfaces** (Reusability and modularity)

## Key Features

### Employees
- Can create appointments by specifying available days and time intervals.
- Can manage appointment schedules.

### Service Categories
- **Admin**: Full CRUD operations on service categories.
- **User, Admin, Employee**: Can retrieve service categories.
- **Implementation**: CRUD interfaces with corresponding classes (e.g., `CreateServiceCategory` implementing `ICreateServiceCategory`).

### Booking System
- **Users** can book a test appointment but must follow a sequential test order.
- **Test Booking Flow:**
  - Users cannot book a second test before passing the first.
  - Upon passing the third test, the **Publish-Subscribe Pattern** triggers:
    - If the user is not in the **Driver Table**, a record is created.
    - A driving license is issued.
- **Booking Endpoints:**
  - `POST api/booking/appointment/firsttime`
  - `POST api/booking/appointment/retakeatest`
- **Validation Checks:**
  - Appointment availability.
  - License existence (avoiding redundant bookings).
  - Test completion order.
  - Differentiation between first-time and retake applications using validation interfaces.

### Appointments
- **Management:**
  - **Employees & Admins**: Can create, update, delete appointments.
  - **Users**: Can only view available appointments.
- **Database Structure:**
  - `TimeIntervals` table stores available time slots.
  - `Appointments` table includes **day, timeIntervalId, isAvailable**.
- **Backend Services:**
  - Ensures logical consistency of appointment schedules.

### CRUD Operations
- **Employees & Admins:** Create, Update, Delete operations.
- **Users:** Read-only access.
- **Entities Managed:**
  - Countries
  - Service Categories
  - Service Purposes
  - Test Types
  - Tests (Results management)
  - Local Driving License
  - International Driving License
  - Applications:
    - LocalDrivingLicenseApplication
    - RetakeTestApplication
    - InternationalApplication

### Application Processing Services
- **Service Types:**
  - First-time application (new driver registration).
  - New application (for existing drivers).
  - License renewal.
  - Replacement (for lost or damaged licenses).
- **Applicable to:**
  - Local driving licenses
  - International driving licenses
  - National ID services
- **Validation Services:**
  - Each application type has separate validation logic ensuring adherence to business rules.

## Architectural Highlights
- **SOLID Principles Applied**
- **Reusable & Generic Repository Pattern**
- **Modular & Scalable Service-Oriented Design**
- **Separation of Concerns for Maintainability**

### Current Development Stage
- The application is still under development, with ongoing enhancements and feature refinements.

