[33mcommit 1d51e25a08a76f600c992df85fc395a6c33a2acb[m[33m ([m[1;36mHEAD[m[33m -> [m[1;32mauth[m[33m)[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Fri Feb 7 17:47:35 2025 +0200

    Refactors User DTOs and adds account controller
    
    Moves User-related DTOs to a dedicated folder for better organization.
    Adds an Account controller with registration and login functionalities.
    The registration process now includes validation and utilizes AutoMapper.
    Adds Microsoft.AspNetCore.Mvc.Core package.
    Move Services configuration into extension method.

[33mcommit 518d80041d3b68bf8f255908ec543ec6a3707f5d[m[33m ([m[1;32mmaster[m[33m, [m[1;32mPublish-Subscriber-DP[m[33m)[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Sun Jan 26 09:49:06 2025 +0200

    Mini MVP IS DONE
    (issuing the Local driving license)
    Refactors and expands service interfaces
    
    Renames IGetLocalDrivingLicenseApplication to ILocalDrivingLicenseApplicationRetrieve for better consistency.
    Introduces new interfaces for LicenseClass, User retrieval services, and a final test subscriber.
    Adds a new event to the test creation service for final test pass.
    Adds new DTOs and enums for licenses.
    Improves test result retrieval to include booking information.
    Implements the logic for creating a new driver and license on final test pass event.

[33mcommit e3e71bc2a148e88bf952e6357cb0894c3e96bba8[m[33m ([m[1;32mIssue-License[m[33m)[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Sun Jan 26 04:19:13 2025 +0200

     Refactors license DTOs and improves logging
    Updates the license creation service to provide more descriptive logging

[33mcommit 0e2d3b63fd0c3c32e6e76ff1a4b90287a8e7b8f8[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Sun Jan 26 03:03:56 2025 +0200

    Refactors license service namespaces
    
    Updates namespaces for license services from `ILicencesServices` to `ILicenseServices` for consistency.
    Removes unused `ICreateLicenseService` interface.
    Adds DTO properties, improves Create/Update services implementations

[33mcommit eaba980d84f38339af2441543fcc253dec573b39[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Sun Jan 26 03:02:55 2025 +0200

    Adds local driving license services
    
    Adds creation and update service interfaces and implementation for local driving licenses.
    Registers these services with the dependency injection container.

[33mcommit 6a772654626fc0a3a3e23e56d3d924f8a7b1af33[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Sat Jan 25 14:52:39 2025 +0200

    Adding Driver's services,
    Re arrange IValidators Folders

[33mcommit dfdedec9df1d0d071d80f51a4b61e79558c6a003[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Fri Jan 24 06:37:41 2025 +0200

    Adds retake test application event
    
    Implements event for when a retake test application is created,
    allowing subscribers to react to the creation of a retake test,
    for example, updating LDL status.
    Also, fixing the validation of retake tests to consider TestTypeId.
    Moves RetakeTestApplicationRetriever to its own file.
    Removes some unused code in the appointment update service
    and makes it event-based as well.

[33mcommit f0f669f08051221c69e3d0cf72542dd77c75dddb[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Thu Jan 23 12:05:33 2025 +0200

    Improves test retake application flow
    
    Refactors the test retake application process by introducing dedicated services and repositories for handling LDL application retake logic and test results retrieval.

[33mcommit d30a79cbd081677425b92f4ff3c7a619b1fd8e1c[m[33m ([m[1;32mbooking[m[33m)[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Thu Jan 23 02:53:06 2025 +0200

    Moves LDL services interfaces
    
    Relocates LDL test retake application service interfaces to remove the redundant "ITests" folder from their namespace.

[33mcommit 232e151df983f1762fe9f7a3dd0e6aeeaf1fe43c[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Thu Jan 23 01:41:20 2025 +0200

    Improves retake test application creation
    
    Refactors the RetakeTestApplicationCreateor to handle validation and creation logic correctly.
    Removes redundant validation logic from the BookingCreationValidator.
    Adds a retake test endpoint to the LocalLicenseApplicationController and configures related dependencies.
    This change ensures a cleaner and more modular structure for creating retake test applications and improves the overall code quality.

[33mcommit 7eefe3bf348a11635e4300212d46591f5e8f2a73[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Wed Jan 22 08:52:26 2025 +0200

    Refactors booking validation logic
    Moves booking validation logic into dedicated validator classes,
    introduces a type validation abstraction to allow for more granular
    validation and removes the first time booking chec
    k from the main booking service.
    move the validation logic into the end point invoking;

[33mcommit ca54656f717376ba5f9168e544dc16bd6a674f0a[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Wed Jan 22 04:22:01 2025 +0200

    Updates stored procedure logic
    
    Modifies the stored procedure to only update appointments that are both expired and currently available.
    
    Removes empty folders from project files.

[33mcommit 8520d9eb195b5bdce4e2526afeb06ca2f393459e[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Wed Jan 22 03:54:50 2025 +0200

    Adds stored procedure for expired appointments
    
    Introduces a stored procedure to automatically mark appointments as unavailable if their date has passed, enhancing the system's ability to manage appointment availability dynamically.
    This change also adds new generic repositories, related to this feature.

[33mcommit 2f180684ab19b283cb3c996de85080d19a10a3a3[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Tue Jan 21 03:51:36 2025 +0200

    Removes migration for function
    
    Removes the migration files related to isTestTypePassed function.
    
    Changes the `onDelete` behavior to `NoAction` in `LocalDrivingLicenses` table to avoid issues when deleting related data

[33mcommit d7a7b0827ea6078dcbbec552fceb8dfb800aad42[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Tue Jan 21 03:45:26 2025 +0200

    Refactors booking and test logic
    
    Removes the `IsTestTypePassed` scalar function and related interface and repository implementation.
    Adds a new `TestTypePassedChecker` service and repository to handle test passing logic, improving test retrieval.
    Updates booking service to include a validation for retake tests and to prevent duplicate bookings, ensuring test integrity and correct booking order.
    Adds and refactors validators for booking and retake tests, ensuring that all booking and retake test applications follow the correct business logic.
    Renames and moves generic repositories, enhancing organization.
    Fixes exception names, creating consistency in the application.
    Adds DTOs for test results and views, enhancing data organization.
    Removes unneeded interfaces and services, keeping only the necessary logic.

[33mcommit 8b98a26bb9302aa564369f25faa1e9dcc0a78c84[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Sun Jan 19 22:01:36 2025 +0200

    Improves booking creation validation
    
    Refactors the booking creation validator
    by adding a check to ensure the same local driving license
    and test type isn't booked twice, and also checks
    the order of the test.

[33mcommit ca7458ddca26a2b7ce9b3c2854e6736b616e867a[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Sun Jan 19 21:59:44 2025 +0200

    Adds database functions and extensions
    
    Introduces database function configurations, TVFs and extensions
    for querying available test days, retrieving LDL applications,
    checking for passed tests, and retrieving test results,
    all managed through new extension methods and function definitions.
    The changes also include a function to check if the test type is passed for a specific application.
    
    Change ApplicationId column in Local Driving License table to LocalDrivingLicenseApplicationId

[33mcommit 0e97a6445cd6c9ad3e3570399886059199191c94[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Sat Jan 18 03:31:43 2025 +0200

    Refactors test retake application creation
    
    Updates the test retake application creator to use a service scope for dependency resolution, ensuring proper disposal of resources.
    It also ensures that test retake applications are only created if the test has failed and the application is valid to retake the test.
    It removes the validator from constructor injection and uses the scope to resolve it.
    It implements IDisposable and removes the event handler in the Dispose method.
    
    DbContext Disposing bug is fixed.

[33mcommit 2471b8ce4e36c12f5e3be663e0ebb5e250ffb68d[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Sat Jan 18 02:03:50 2025 +0200

    Adds TVF function mapping to DbContext
    
    Maps Table-Valued Functions (TVFs) to the DbContext using DbFunction attribute,
    allowing them to be called as LINQ queries.
    Also, creates DTO for the 'AvailableDay' TVF result and replaces SQL raw queries with function calls.
    
    before using IServiceScopeFactory and make independant scope for the event handling operation

[33mcommit feca4e2bad5db4c8bcfdf3b7a39e846030fe60c5[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Fri Jan 17 08:26:36 2025 +0200

    Refactors data access logic
    
    Moves TVF and SP interfaces and implementations to the IRepository project and Repositories folders, respectively.
    This change improves project structure, separates concerns, and prepares the project for better testability by decoupling the data access logic from the DbContext.
    Removes unnecessary command timeout configuration.
    
    Before discard sql query raw and start use EF Core's built-in function mapping

[33mcommit 6780afc370f1141bb26044edf600dd9eaecb62d5[m[33m ([m[1;31morigin/Booking[m[33m)[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Fri Jan 17 04:53:16 2025 +0200

    Adds entity configurations
    
    Adds configurations for LDLApplicationsAllowedToRetakeATest, Employee, and Admin entities.
    Adds dummy data for Employee and Admin for testing purposes.
    Also, fixes a column name in LDLApplicationsAllowedToRetakeATest table in migrations.
    Updates the GetLDLAppsAllowedToRetakATest to return the columns and fixes the type of the returned object.
    Updates the test creation service to return testDTO.
    Also modifies the testRetakeApplicationCreator service, to fetch the test record and use it for its operations.
    
    There an issue with DBcontext with Dependency Injection and the table-value functions

[33mcommit c6a6362ad0ed7b29c3b98bb43f7074a33ef203b5[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Thu Jan 16 23:24:33 2025 +0200

    Corrects table name typo and adds new features
    
    Fixes a typo in the table name "Applicataions" to "Applications" across migrations, foreign key constraints, and indexes.
    Adds .vs directory to .gitignore to ignore visual studio specific files.
    Adds a foreign key constraint for service purposes and service categories to the applications table, and also adds a new table for RetakeTestApplication.
    Adds an event to the test creator service so that the application can subscribe for test creation.
    Also, updates the service fees DTO and service to align the service purpose field name across the app

[33mcommit a29b57ada05ea0dafb096d038dbc7124939ac678[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Thu Jan 16 09:26:55 2025 +0200

    Removes project config files
    
    Deletes various project configuration files related to the IDE and development environment.
    These files are not necessary for the core project functionality and should not be tracked.

[33mcommit 33944bc14a46e357c0ac9f5343d59dbc9d63e9c5[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Thu Jan 16 09:22:22 2025 +0200

    Adds test retake functionality and improves setup
    
    Adds functionality for users to retake tests, along with the required database setup.
    Adds a trigger to allow retake test creation only after a fail to reduce unnecessary records.
    Updates .gitignore to include IDE specific folders.

[33mcommit 264bc42136ddc8e5a22e2e9f7b4fe0de18ad3b14[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Tue Jan 14 10:12:16 2025 +0200

    Refactors services and adds new interfaces
    
    This commit refactors existing services to use asynchronous retrieval and creation interfaces,
    enhancing code organization and maintainability.
    
    It introduces new interfaces for retrieving and creating specific records asynchronously.
    Additionally, the commit includes new DTOs for test data and a new service for retake tests.
    
    Removes unused service interfaces.

[33mcommit 92a1f9ccb8d0479dfe39b1645a177e24f056371d[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Mon Jan 13 09:40:23 2025 +0200

    Renames and fixes application entity
    
    Renames the 'Applicataions' table to 'Applications' to adhere to naming conventions.
    Updates the Application entity configuration to specify the table name and correct a typo in the DbSet property name in the DbContext.
    Adds foreign key relationships to the applications table.
    Updates the retake test application to be linked with the applicaiton table.
    
    Fixes issues related to table naming and entity relationships

[33mcommit d037453e1e7c35c7bc3d029fad1c5cd2239e1829[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Sun Jan 12 02:19:19 2025 +0200

    Adds test type FK to bookings
    
    Adds a foreign key column 'TestTypeId' to the 'Bookings' table, linking it to the 'TestTypes' table.
    This change allows tracking the type of test associated with each booking.
    Updates the database model and migrations to reflect this new relationship.
    Implements a first-time booking check to not allow the user to book multiple times the same test for the same application,
    also implements the retake test logic for booking the same test.
    Refactor the create local driving license application to use the orchestrator pattern to separate the services calls
    
    Add Documentation to some code.

[33mcommit f0a923d665f9f60519fd9b87b7cb68d40b37287c[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Sat Jan 11 01:23:05 2025 +0200

    Adds retake test application feature
    
    Adds a retake test application entity, including related configurations and migrations.
    Updates the get available days query to consider available appointments and refines time interval query to include appointment ID.
    
    removed UserId Property from some DTOs.

[33mcommit 07bab665d856bdea918a8f6f3ae324b5d8dfe466[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Fri Jan 10 04:43:50 2025 +0200

    Updates GetTestTypeDayTimeInterval function
    
    Renames the function in SQL migration and stored procedure from ITVF_GetTestTypeDayTimeInterval to GetTestTypeDayTimeInterval.
    Adds a condition to the stored procedure to consider only available appointments.
    Refactors the endpoint to return  APIResponse object, enhancing efficiency.
    Improves error handling for invalid test type IDs and unexpected exceptions.
    Removes unnecessary logic from the controller.

[33mcommit 5e1649e2a6abc27fd8f08213f75ad73c97811834[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Fri Jan 10 03:58:05 2025 +0200

    Migrates SPs to Table-Valued Functions
    
    (in actual code, not migration)
    Replaces stored procedures with inline table-valued functions (TVFs) for fetching available days and test type time intervals.
    This change improves data retrieval efficiency and aligns with best practices for querying data with parameters in the database context.
    Removes the old SP interface and adds the new TVF interfaces and implementations.

[33mcommit 09b41444492c7ebb97862d05329f14d5fe97b6e2[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Fri Jan 10 03:34:30 2025 +0200

    Adds application status column
    
    Adds a new column named ApplicationStatus of type tinyint to the Application entity.
    
    Removes and replaces stored procedures with functions (Migration Files)
    Fixes GetAllApplicationsByEmployeeTEST, GetApplicationByEmployeeSeriviceTEST, UpdateApplicationByEmployeeTEST, UpdateApplicationByUserTEST test cases
    Create Appointment Services Test Cases
    Create Get All appointment Service Test Cases
    Create Get Appointment Service Test Cases
    Add create booking request DTO
    Add Get Local Driving License Application Interface

[33mcommit faf936006088bfc55ba1d9fe33e1f26f07fba731[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Thu Jan 9 01:52:43 2025 +0200

    Adds stored procedure interfaces and implementation
    
    Introduces interfaces for stored procedures, promoting loose coupling.
    Implements the stored procedure `SP_GetTestTypeDayTimeInterval` to fetch time intervals
    and `SP_InsertAppointment` to insert appointment data
    Updates `AppointmentController` to use the new stored procedure interface, improving code modularity.

[33mcommit f73bdb6d05a2611a08b33fe331b8af0e74a0fe32[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Wed Jan 8 23:26:46 2025 +0200

    Modify SP_GetTestTypeDaysInterval

[33mcommit 34bf41acda39871d6a0e2324f0e5b2589f813ffb[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Wed Jan 8 22:40:45 2025 +0200

    Create Procedure SP_GetTestTypeDaysTimeInterval

[33mcommit f2c7b283ad3a74eda6f3ac16475b3ebd485e3923[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Wed Jan 8 12:55:09 2025 +0200

    Add Two Procedures  (SP_GetAvailableDays, SP_InsertAppointment)
    Add GetDays Action to get all avaialble days for a specfic test type

[33mcommit 07560d58e0c414a86fa29105bb9b1c37646cd5dc[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Wed Jan 8 02:25:35 2025 +0200

    Add Dicitionry to CreateAppointmetsRequest and remove to lists

[33mcommit a74525db15477c48226025590566803d90d4bce8[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Tue Jan 7 18:04:11 2025 +0200

    Improves appointments and validation
    
    Adds appointment creation, retrieval, and validation services.
    Removes redundant validator interfaces and consolidates to base validators.
    Introduces time interval services for appointment management.
    Enhances API responses and validation of input data.
    Updates model and DTO classes for appointments and time intervals.

[33mcommit b1fdb45d6f2404b88aadc05d1a4ae07d057b9a12[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Fri Jan 3 02:55:06 2025 +0200

    refactor: organize enums into Enums namespace and update references; add IGetTestTypeService and GetTestTypesService implementations

[33mcommit c443bbba4eb9c908a8fd98683d9342e49c731020[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Tue Dec 31 04:19:05 2024 +0200

    create InsertAppointment stored procedure

[33mcommit 40f4f1b25854553ad064f622c165acc3e9fe38da[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Sun Dec 29 08:03:30 2024 +0200

    add CreateAppointmentsDTO, BookingController, and update TimeInterval properties; make RetakeTestApplicationId nullable

[33mcommit cc89592e2737ecea3d73bd409b77c2ce14e13467[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Sat Dec 28 00:44:56 2024 +0200

    add Booking and Appointment models, introduce EnBookingStatus enum, and refactor TestAppointment to Appointment

[33mcommit 12a1e349933aae9be93fe264bd3b0b57cd4c6381[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Thu Dec 26 15:48:16 2024 +0200

    add TimeIntervalId to TestAppointments and create TimeInterval table with initial data, create function in CountryConfiguration To Create Array of CountryObject

[33mcommit ec51db53c637dae44d7a561d8dc108f77e4561f4[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Thu Dec 26 00:14:06 2024 +0200

    add  enums EnHour and EnHoursStage , implement TimeInterval and TestAppointment configurations, and rename configuration classes for consistency

[33mcommit a445ba8573aebc0395725644dd7a1a44af7daf7b[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Wed Dec 25 18:33:11 2024 +0200

    move Countries and Tests Separate folders
    and changes IServices.Country To IServices.ICountryServices

[33mcommit d9d91ef6ae5a91500338e150e9b6b118ec2e6743[m[33m ([m[1;32mCreateApplications[m[33m)[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Wed Dec 25 17:26:47 2024 +0200

    add ValidationException class, improve logging in CountryController,improve logging in local driving license application (some of it's services),and fix variable naming in license application services

[33mcommit f3fb6509516d1a847a108b1510ed390bc3376b47[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Mon Dec 23 11:13:22 2024 +0200

    create migrations, renamed columns, add enums for license class and liceanse statues

[33mcommit 7eecfab8b6f08c0a49b1167b7ac942c62d69351e[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Sun Dec 22 21:23:16 2024 +0200

    before removing the migrations

[33mcommit 73c62d89d863cd13bb59a8cca532075ad9f91ecd[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Sun Dec 22 17:20:38 2024 +0200

    rename enums (EnServicePurpose, EnServiceCategory)'s values name
    create ServiceFeesConfig and insert Service Fees Data into database
    Migrations all changes

[33mcommit 8ce86deaf7b18cef3a47403a333d6029ca3ecc57[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Sat Dec 21 18:38:34 2024 +0200

    fix TaskCancelExecption, create GetAllCountries action for CountryCountroller, Make Enums for Countries Namem Countries Code

[33mcommit 3151845182b1c72e3ced643af90f80166d29624e[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Sat Dec 21 10:22:33 2024 +0200

    create init migrate and update the database

[33mcommit 8532a31b2a00cbf0664fd208d306d563573ee771[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Sat Dec 21 10:02:44 2024 +0200

    creatinging controller for Local Driving License Application, and fix its Dependency injection

[33mcommit 42f339b77a55868c98ebf567445223eb9cff190f[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Fri Dec 20 16:27:18 2024 +0200

    implement get detained licenes service,and add detained license dto to the mapper config

[33mcommit b42eecbb4a5286370ad5563e3ddf5a3df935e673[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Fri Dec 20 15:57:35 2024 +0200

    Create:
             Get Local Detained Interface
             Detained License Application DTO
             Renew Local Driving License class and interface
             New Exception => InvalidRequestException
             Application/LocalLicenseApplication Controller and actions's signature

[33mcommit a6f91f9d744453a5fc39ddea98ee56f89a5f16d2[m[33m ([m[1;31morigin/CreateApplications[m[33m)[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Fri Dec 20 10:51:38 2024 +0200

    create class and interface for:
          Local Driving License Application Validation.
          Create Driving License Application entity.
    create interfaces for Local Driving License Application Services Purposes

[33mcommit 0aab150b602bb95bb9f024f8e04c7076f2b196df[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Fri Dec 20 00:56:37 2024 +0200

    rename AppliactionPupose to ServicePurpose

[33mcommit 0f57c314303b827e26776649fce3705b9a3bdea1[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Thu Dec 19 00:00:34 2024 +0200

    rename enum ApplicationStatus to enum EnApplicationStatus
    rename namespace from IServices.Application to iServices.ApplicationServices
    create interface Application Status service
    create Exception for Application Status's (pending, in progress)
    separate Get License by user Id into interfaces and classes
    separate other files and logic classes and interfaces
    fix DI

[33mcommit ca37ea261be9de2e9056f350d471a529c51cd3f4[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Wed Dec 18 08:04:49 2024 +0200

    breaking classes and interfaces into subclasses and subinterfaces

[33mcommit 48916fd9d3c52dbcb89b2441df129393bbfe997e[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Tue Dec 17 17:49:46 2024 +0200

    Apply Single Responsibilty on CreateApplicationService

[33mcommit b0e860376217b5b0f1fb3c14ca20de10cd70a430[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Tue Dec 17 06:56:38 2024 +0200

    fix unit testing, fix DI

[33mcommit a161935d2863687932e74c857693aac86834494b[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Mon Dec 16 21:59:53 2024 +0200

    fixed dependency injection

[33mcommit 316082691c927f69beb2205cede2858f8bc92ee3[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Mon Dec 16 21:22:29 2024 +0200

    Rename
         ApplicationFor to ServiceCategory,
         ApplicationType to ApplicationPurpose,
         ApplicationFees to ServiceFees

[33mcommit 17e80956b31e9b6643ec0955a59078884156f921[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Mon Dec 16 15:46:17 2024 +0200

    rename license Application to Application

[33mcommit 980f37dce773b668df19999024d3d5a9d00c1406[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Mon Dec 16 10:03:59 2024 +0200

    Change CreateApplicationRequest to abstract
           Add DTOs:
                CreateLocalDrivingLicenseApplicationRequest, CreateLocalDrivingLicenseApplicationRequest

[33mcommit 03e26291bd042e1b54b0a677b0bc3c6e3cd0ba45[m
Author: MustaphaAlaa <mostafaalaa11998@gmail.com>
Date:   Mon Dec 16 09:19:03 2024 +0200

    modified files

[33mcommit fcdc1fe94c0929117892b66af31bc4c97a445901[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Mon Dec 16 00:04:23 2024 +0200

    create interfaces for services and DTOs, renamed fields and classes.

[33mcommit 92292f26347f6cdbff205fd0b5fc165b252bf82f[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Sat Dec 14 10:41:13 2024 +0200

    modified and renamed filed and classes, create IGetLocalLicense

[33mcommit 0014b9cd1e11c869fbb3624e279e6e451058a35f[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Thu Dec 12 07:02:44 2024 +0200

    change interface's genric order in IServices Project

[33mcommit 679968a25d9d38fdf97e7793c591f9ddfa9d690e[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Wed Dec 11 12:15:10 2024 +0200

    rename License.cs to LocalLicense.cs, rename appiaction field inside InternationalLicense to InternationalDrivingLicenseApplication

[33mcommit da609b6021e923c31a3e295ac51e662d3dfb7eda[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Tue Dec 10 17:30:50 2024 +0200

    create ApplicationFeesConfig, LicenseTypeConfig,LicenseApplicationConfig
    and modified OnCreating file

[33mcommit 4162482cf84490736ee28935f45b02c82768942e[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Tue Dec 10 17:08:43 2024 +0200

    remove migration folder
    renamed DVLDDbContext file to GovConnectDbContext

[33mcommit d9324b64f4b3c2b2b76aaeb736506781611866b4[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Tue Dec 10 17:02:30 2024 +0200

     Create EntitiesConfiguration directory inside DataConfigurations to split tables configuration from on creating file
        create ApplicationForConfig, ApplicationTypeConfig files inside it.
        rename DbContext from DVLDDbContext to GovConnectDbContext

[33mcommit 7440c935fde551719d108d54a05d36e0b9e2059c[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Mon Dec 9 02:38:38 2024 +0200

    migrate and update database with previous changes

[33mcommit 6e1f2278b2174cdc8e10b8a31f99093f71d0f136[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Sun Dec 8 15:15:06 2024 +0200

    rename Appication class to LicenseApplication

[33mcommit e2f7457d2a4122419c0834fbb1b54930c7e81f11[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Sun Dec 8 12:13:02 2024 +0200

                     Create application class for intenational License, international license class.
                     modified classes, rename test project

[33mcommit 09ecce8e921e34471402d3ed0132169951a8a0ec[m[33m ([m[1;31morigin/Master[m[33m)[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Sat Dec 7 12:12:43 2024 +0200

    change the repo name from Drivers and Vehicles License Department to GovConnect
    add nth names fields to user, update user DTO, register user DTO classes

[33mcommit 7382cd10fcc8fcf7cbb1a47e4f7c5aca5655ffef[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Sat Dec 7 09:21:37 2024 +0200

            Before the change from LicenseHub to GovConnect
    Add nth names fields to user class,
    other changes

[33mcommit b34a9347840d555c2a0fd896767c464de7566496[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Sat Nov 30 00:06:00 2024 +0200

    Modify database design and it's string connection
    add columns in User Table
    renaming namespace from Models.License to Models.LicenseModels
    add LicenseType class and enLicenseType.
    change databaseName from DVLD To LicenseHubDB.

[33mcommit 1633b01649a10856a961a004416e06ea1d81c06f[m
Merge: f8d6289 31799a3
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Fri Nov 29 16:51:42 2024 +0200

    Merge branch 'Application'

[33mcommit 31799a3b6c72da5e0a1744531750e60a8dc43b9f[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Fri Nov 29 15:55:34 2024 +0200

    Get All Applications By Employee service and unit testing are done.

[33mcommit e8764d27799ddbe0372a58ad7ffc86b74b510a11[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Fri Nov 29 06:05:12 2024 +0200

    Update Application By Employee service and unit testing are done

[33mcommit 8263ce9a318147cd07bdb02f950c6f490c314a63[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Thu Nov 28 07:42:56 2024 +0200

    Get Application By User services and test are done

[33mcommit f8d628995c187b09ee1632bc8fc8905c9301ef4c[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Fri Nov 29 15:55:34 2024 +0200

    GetAllApplicationsByEmployee service and unit testing are done.

[33mcommit 7a9e16ba3ac75641d11869a06a90259edf0c80cf[m[33m ([m[1;31mgitlab/Application[m[33m)[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Fri Nov 29 06:05:12 2024 +0200

    UpdateApplicationByEmployee service and unit testing are done

[33mcommit ff5af2fc24c404c4822afead35a5f4fece4241ca[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Thu Nov 28 07:42:56 2024 +0200

    GetApplicationByUser services and test are done

[33mcommit d30d039256fcc4c4921745fecccd396bd6517c99[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Tue Nov 26 09:31:07 2024 +0200

    Adjust GetApplicationByUser and renaming some namespaces names

[33mcommit 0ce3bbdf823dc7c62183a7c0d1999903176f8ff3[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Tue Nov 26 05:37:42 2024 +0200

    GetApplicationByuser Service and test are done.

[33mcommit e79a57d3929a3ca51a29dc32c3041d9561149cc2[m
Author: MustaphaAlaa <MostafaAlaa11998@gmail.com>
Date:   Mon Nov 25 23:17:27 2024 +0200

    UpdateApplicationByUser Service and tests are done. + added custom exception.

[33mcommit 30c6c46a6d1ac4ecbb477243a677ab788fb5420c[m
Author: Mostafa Alaa <MostafaAlaa@gmail.com>
Date:   Mon Nov 25 04:13:36 2024 +0200

    CreateApplicationRequest service and test cases are done.

[33mcommit 34ab1e6693ca8fcbd7bf40fc5e3bd85523cff759[m
Author: Mostafa Alaa <MostafaAlaa@gmail.com>
Date:   Sun Nov 24 09:46:41 2024 +0200

    new migration and change key datatype for appFor and appType tables

[33mcommit e0b26e675ace857e46c148d6b11a34590400aac5[m
Author: Mostafa Alaa <MostafaAlaa@gmail.com>
Date:   Sun Nov 24 07:49:16 2024 +0200

    before remove all  migrations and migrate it again

[33mcommit ba2c3fd1a53fe1f3651c97998e95e8ee78dab85a[m
Author: Mostafa Alaa <MostafaAlaa@gmail.com>
Date:   Sat Nov 23 19:30:52 2024 +0200

    Change ApplicationStatus DataType and  renaming CreatedByEmployee To UpdatedByEmployee

[33mcommit 5543e9d58d177b76c2cbaedda382a7a42663993d[m
Author: Mostafa Alaa <MostafaAlaa@gmail.com>
Date:   Sat Nov 23 10:06:08 2024 +0200

    DeleteApplication service and tests are done

[33mcommit 49db5643899b3ea7f033fd7619721724541bee36[m
Author: Mostafa Alaa <MostafaAlaa@gmail.com>
Date:   Sat Nov 23 09:39:45 2024 +0200

    UpdateApplicationFees service and tests are done

[33mcommit e7bf84f3ea7cdfffc7f21fbe0065ab35b5f45e49[m
Author: Mostafa Alaa <MostafaAlaa@gmail.com>
Date:   Sat Nov 23 05:57:06 2024 +0200

    Add Migariotn for LastUpdaet in ApplicationFees

[33mcommit 58c08b79761912144f646477e0d5f73a984093fd[m
Author: Mostafa Alaa <MostafaAlaa@gmail.com>
Date:   Sat Nov 23 05:11:28 2024 +0200

    Add LastUpdate field to ApplicationFees

[33mcommit 5b9de7727c28c2aa5aaee5aecc2cb02f78f0a286[m
Author: Mostafa Alaa <MostafaAlaa@gmail.com>
Date:   Sat Nov 23 04:50:08 2024 +0200

    GetAllApplicationsFees service and tests are done

[33mcommit 0a89c0438957368e874650b56639b7dde4697686[m
Author: Mostafa Alaa <MostafaAlaa@gmail.com>
Date:   Sat Nov 23 03:21:26 2024 +0200

    GetApplicationFees Service and test are done

[33mcommit ad775d6d315d8677e2e96aa277232d76c3c09e91[m
Author: Mostafa Alaa <MostafaAlaa@gmail.com>
Date:   Sat Nov 23 01:28:18 2024 +0200

    Confilct between ApplicationFor and ApplicationFees Is Solved

[33mcommit ec9f35be6999b1db8a85cb7401e1dbe91cdb4248[m
Author: Mostafa Alaa <MostafaAlaa@gmail.com>
Date:   Fri Nov 22 23:34:18 2024 +0200

    ICreateAppFees service and test

[33mcommit 6c9055d3a7254bb8438e1368c61449a4ccda6e56[m
Author: Mostafa Alaa <MostafaAlaa@gmail.com>
Date:   Thu Nov 21 06:58:05 2024 +0200

    unit Testing for ApplictionType and ApplictionFor are done

[33mcommit d75fae7a2c73d1dbd8b6304e35e12e29cbd357bd[m
Author: Mostafa Alaa <MostafaAlaa@gmail.com>
Date:   Mon Nov 18 17:43:55 2024 +0200

    Services for (ApplicationType & ApplicationFor) are done

[33mcommit 2cd08e85990d10dc4efbe9e343837f2bfa65c64b[m
Author: Mostafa Alaa <MostafaAlaa@gmail.com>
Date:   Mon Nov 18 06:56:23 2024 +0200

    redesign Application Classes and migrate them into database

[33mcommit f3a9603e86e453ec1a328aa252caed0bbb215241[m
Author: Mostafa Alaa <MostafaAlaa@gmail.com>
Date:   Sat Nov 16 00:42:37 2024 +0200

    all unit testing for country Services is done.

[33mcommit 6bb4e4516401ee76d437f4547002f1fae4cced15[m
Author: Mostafa Alaa <MostafaAlaa@gmail.com>
Date:   Fri Nov 15 06:14:00 2024 +0200

    Add project files.

[33mcommit 64dfa40088167f3bdc1be04c816b706702153b79[m
Author: Mostafa Alaa <MostafaAlaa@gmail.com>
Date:   Fri Nov 15 06:13:51 2024 +0200

    Add .gitattributes and .gitignore.
