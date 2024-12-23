using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTypes",
                columns: table => new
                {
                    EmployeeTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTypes", x => x.EmployeeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "LicenseClasses",
                columns: table => new
                {
                    LicenseClassId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinimumAllowedAge = table.Column<byte>(type: "tinyint", nullable: false),
                    DefaultValidityLengthInMonths = table.Column<int>(type: "int", nullable: false),
                    LicenseClassFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseClasses", x => x.LicenseClassId);
                });

            migrationBuilder.CreateTable(
                name: "LicenseTypes",
                columns: table => new
                {
                    LicenseTypeId = table.Column<byte>(type: "tinyint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fees = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseTypes", x => x.LicenseTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCategories",
                columns: table => new
                {
                    ServiceCategoryId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCategories", x => x.ServiceCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "ServicesPurposes",
                columns: table => new
                {
                    ServicePurposeId = table.Column<byte>(type: "tinyint", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesPurposes", x => x.ServicePurposeId);
                });

            migrationBuilder.CreateTable(
                name: "TestTypes",
                columns: table => new
                {
                    TestTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestTypeTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestTypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestTypeFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTypes", x => x.TestTypeId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThirdName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FourthName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ServicesFees",
                columns: table => new
                {
                    ServicePurposeId = table.Column<byte>(type: "tinyint", nullable: false),
                    ServiceCategoryId = table.Column<short>(type: "smallint", nullable: false),
                    Fees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesFees", x => new { x.ServicePurposeId, x.ServiceCategoryId });
                    table.ForeignKey(
                        name: "FK_ServicesFees_ServiceCategories_ServiceCategoryId",
                        column: x => x.ServiceCategoryId,
                        principalTable: "ServiceCategories",
                        principalColumn: "ServiceCategoryId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ServicesFees_ServicesPurposes_ServicePurposeId",
                        column: x => x.ServicePurposeId,
                        principalTable: "ServicesPurposes",
                        principalColumn: "ServicePurposeId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEmployee = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HiredByAdmin = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HiredDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Admins_HiredByAdmin",
                        column: x => x.HiredByAdmin,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Applicataions",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastStatusDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaidFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServicePurposeId = table.Column<byte>(type: "tinyint", nullable: false),
                    ServiceCategoryId = table.Column<short>(type: "smallint", nullable: false),
                    UpdatedByEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicataions", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_Applicataions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Applicataions_Employees_UpdatedByEmployeeId",
                        column: x => x.UpdatedByEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Applicataions_ServicesFees_ServicePurposeId_ServiceCategoryId",
                        columns: x => new { x.ServicePurposeId, x.ServiceCategoryId },
                        principalTable: "ServicesFees",
                        principalColumns: new[] { "ServicePurposeId", "ServiceCategoryId" },
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedByEmployee = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.DriverId);
                    table.ForeignKey(
                        name: "FK_Drivers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Drivers_Employees_CreatedByEmployee",
                        column: x => x.CreatedByEmployee,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "LocalDrivingLicenseApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    LicenseClassId = table.Column<short>(type: "smallint", nullable: false),
                    ReasonForTheApplication = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalDrivingLicenseApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalDrivingLicenseApplications_Applicataions_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applicataions",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LocalDrivingLicenseApplications_LicenseClasses_LicenseClassId",
                        column: x => x.LicenseClassId,
                        principalTable: "LicenseClasses",
                        principalColumn: "LicenseClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocalDrivingLicenses",
                columns: table => new
                {
                    LocalDrivingLicenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IssueReason = table.Column<byte>(type: "tinyint", nullable: false),
                    IssuingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByEmployee = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LicenseClassId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalDrivingLicenses", x => x.LocalDrivingLicenseId);
                    table.ForeignKey(
                        name: "FK_LocalDrivingLicenses_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LocalDrivingLicenses_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "DriverId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LocalDrivingLicenses_Employees_CreatedByEmployee",
                        column: x => x.CreatedByEmployee,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LocalDrivingLicenses_LicenseClasses_LicenseClassId",
                        column: x => x.LicenseClassId,
                        principalTable: "LicenseClasses",
                        principalColumn: "LicenseClassId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LocalDrivingLicenses_LocalDrivingLicenses_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "LocalDrivingLicenses",
                        principalColumn: "LocalDrivingLicenseId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "TestAppointments",
                columns: table => new
                {
                    TestAppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestTypeId = table.Column<int>(type: "int", nullable: false),
                    LocalDrivingLicenseApplicationId = table.Column<int>(type: "int", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaidFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false),
                    RetakeTestApplicationId = table.Column<int>(type: "int", nullable: false),
                    CreatedByEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestAppointments", x => x.TestAppointmentId);
                    table.ForeignKey(
                        name: "FK_TestAppointments_Applicataions_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applicataions",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TestAppointments_Employees_CreatedByEmployeeId",
                        column: x => x.CreatedByEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TestAppointments_LocalDrivingLicenseApplications_LocalDrivingLicenseApplicationId",
                        column: x => x.LocalDrivingLicenseApplicationId,
                        principalTable: "LocalDrivingLicenseApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TestAppointments_TestTypes_TestTypeId",
                        column: x => x.TestTypeId,
                        principalTable: "TestTypes",
                        principalColumn: "TestTypeId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "DetainedLicenses",
                columns: table => new
                {
                    DetainedLicenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenseId = table.Column<int>(type: "int", nullable: false),
                    DetainDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FineFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedByEmployee = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsReleased = table.Column<bool>(type: "bit", nullable: false),
                    ReleasedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReleasedByEmployee = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReleaseApplicationId = table.Column<int>(type: "int", nullable: false),
                    LocalDrivingLicenseId = table.Column<int>(type: "int", nullable: false),
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetainedLicenses", x => x.DetainedLicenseId);
                    table.ForeignKey(
                        name: "FK_DetainedLicenses_Applicataions_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applicataions",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DetainedLicenses_Employees_CreatedByEmployee",
                        column: x => x.CreatedByEmployee,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DetainedLicenses_Employees_ReleasedByEmployee",
                        column: x => x.ReleasedByEmployee,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DetainedLicenses_LocalDrivingLicenses_LocalDrivingLicenseId",
                        column: x => x.LocalDrivingLicenseId,
                        principalTable: "LocalDrivingLicenses",
                        principalColumn: "LocalDrivingLicenseId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "InternationalDrivingLicenseApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    LicenseId = table.Column<int>(type: "int", nullable: false),
                    LocalDrivingLicenseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternationalDrivingLicenseApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternationalDrivingLicenseApplications_Applicataions_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applicataions",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InternationalDrivingLicenseApplications_LocalDrivingLicenses_LocalDrivingLicenseId",
                        column: x => x.LocalDrivingLicenseId,
                        principalTable: "LocalDrivingLicenses",
                        principalColumn: "LocalDrivingLicenseId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    TestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestAppointmentId = table.Column<int>(type: "int", nullable: false),
                    TestResult = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedByEmployee = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.TestId);
                    table.ForeignKey(
                        name: "FK_Tests_Employees_CreatedByEmployee",
                        column: x => x.CreatedByEmployee,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Tests_TestAppointments_TestAppointmentId",
                        column: x => x.TestAppointmentId,
                        principalTable: "TestAppointments",
                        principalColumn: "TestAppointmentId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Tests_TestTypes_TestTypeId",
                        column: x => x.TestTypeId,
                        principalTable: "TestTypes",
                        principalColumn: "TestTypeId");
                });

            migrationBuilder.CreateTable(
                name: "InternationalDrivingLicenses",
                columns: table => new
                {
                    InternationalDrivingLicenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternationalDrivingLicenseApplicationID = table.Column<int>(type: "int", nullable: false),
                    LicenseClassId = table.Column<int>(type: "int", nullable: false),
                    LocalDrivingLicenseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternationalDrivingLicenses", x => x.InternationalDrivingLicenseId);
                    table.ForeignKey(
                        name: "FK_InternationalDrivingLicenses_InternationalDrivingLicenseApplications_InternationalDrivingLicenseApplicationID",
                        column: x => x.InternationalDrivingLicenseApplicationID,
                        principalTable: "InternationalDrivingLicenseApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InternationalDrivingLicenses_LocalDrivingLicenses_LocalDrivingLicenseId",
                        column: x => x.LocalDrivingLicenseId,
                        principalTable: "LocalDrivingLicenses",
                        principalColumn: "LocalDrivingLicenseId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "CountryCode", "CountryName" },
                values: new object[,]
                {
                    { 1, "AFG", "Afghanistan" },
                    { 2, "ALB", "Albania" },
                    { 3, "DZA", "Algeria" },
                    { 4, "AND", "Andorra" },
                    { 5, "AGO", "Angola" },
                    { 6, "ATG", "Antigua and Barbuda" },
                    { 7, "ARG", "Argentina" },
                    { 8, "ARM", "Armenia" },
                    { 9, "AUS", "Australia" },
                    { 10, "AUT", "Austria" },
                    { 11, "AZE", "Azerbaijan" },
                    { 12, "BHS", "Bahamas" },
                    { 13, "BHR", "Bahrain" },
                    { 14, "BGD", "Bangladesh" },
                    { 15, "BRB", "Barbados" },
                    { 16, "BLR", "Belarus" },
                    { 17, "BEL", "Belgium" },
                    { 18, "BLZ", "Belize" },
                    { 19, "BEN", "Benin" },
                    { 20, "BTN", "Bhutan" },
                    { 21, "BOL", "Bolivia" },
                    { 22, "BIH", "Bosnia and Herzegovina" },
                    { 23, "BWA", "Botswana" },
                    { 24, "BRA", "Brazil" },
                    { 25, "BRN", "Brunei" },
                    { 26, "BGR", "Bulgaria" },
                    { 27, "BFA", "Burkina Faso" },
                    { 28, "BDI", "Burundi" },
                    { 29, "CPV", "Cabo Verde" },
                    { 30, "KHM", "Cambodia" },
                    { 31, "CMR", "Cameroon" },
                    { 32, "CAN", "Canada" },
                    { 33, "CAF", "Central African Republic" },
                    { 34, "TCD", "Chad" },
                    { 35, "CHL", "Chile" },
                    { 36, "CHN", "China" },
                    { 37, "COL", "Colombia" },
                    { 38, "COM", "Comoros" },
                    { 39, "COD", "Congo Democratic Republic of the" },
                    { 40, "COG", "Congo Republic of the" },
                    { 41, "CRI", "Costa Rica" },
                    { 42, "CIV", "Cote d Ivoire" },
                    { 43, "HRV", "Croatia" },
                    { 44, "CUB", "Cuba" },
                    { 45, "CYP", "Cyprus" },
                    { 46, "CZE", "Czechia" },
                    { 47, "DNK", "Denmark" },
                    { 48, "DJI", "Djibouti" },
                    { 49, "DMA", "Dominica" },
                    { 50, "DOM", "Dominican Republic" },
                    { 51, "ECU", "Ecuador" },
                    { 52, "EGY", "Egypt" },
                    { 53, "SLV", "El Salvador" },
                    { 54, "GNQ", "Equatorial Guinea" },
                    { 55, "ERI", "Eritrea" },
                    { 56, "EST", "Estonia" },
                    { 57, "SWZ", "Eswatini" },
                    { 58, "ETH", "Ethiopia" },
                    { 59, "FJI", "Fiji" },
                    { 60, "FIN", "Finland" },
                    { 61, "FRA", "France" },
                    { 62, "GAB", "Gabon" },
                    { 63, "GMB", "Gambia" },
                    { 64, "GEO", "Georgia" },
                    { 65, "DEU", "Germany" },
                    { 66, "GHA", "Ghana" },
                    { 67, "GRC", "Greece" },
                    { 68, "GRD", "Grenada" },
                    { 69, "GTM", "Guatemala" },
                    { 70, "GIN", "Guinea" },
                    { 71, "GNB", "Guinea Bissau" },
                    { 72, "GUY", "Guyana" },
                    { 73, "HTI", "Haiti" },
                    { 74, "HND", "Honduras" },
                    { 75, "HUN", "Hungary" },
                    { 76, "ISL", "Iceland" },
                    { 77, "IND", "India" },
                    { 78, "IDN", "Indonesia" },
                    { 79, "IRN", "Iran" },
                    { 80, "IRQ", "Iraq" },
                    { 81, "IRL", "Ireland" },
                    { 83, "ITA", "Italy" },
                    { 84, "JAM", "Jamaica" },
                    { 85, "JPN", "Japan" },
                    { 86, "JOR", "Jordan" },
                    { 87, "KAZ", "Kazakhstan" },
                    { 88, "KEN", "Kenya" },
                    { 89, "KIR", "Kiribati" },
                    { 90, "PRK", "Korea North" },
                    { 91, "KOR", "Korea South" },
                    { 92, "XKX", "Kosovo" },
                    { 93, "KWT", "Kuwait" },
                    { 94, "KGZ", "Kyrgyzstan" },
                    { 95, "LAO", "Laos" },
                    { 96, "LVA", "Latvia" },
                    { 97, "LBN", "Lebanon" },
                    { 98, "LSO", "Lesotho" },
                    { 99, "LBR", "Liberia" },
                    { 100, "LBY", "Libya" },
                    { 101, "LIE", "Liechtenstein" },
                    { 102, "LTU", "Lithuania" },
                    { 103, "LUX", "Luxembourg" },
                    { 104, "MDG", "Madagascar" },
                    { 105, "MWI", "Malawi" },
                    { 106, "MYS", "Malaysia" },
                    { 107, "MDV", "Maldives" },
                    { 108, "MLI", "Mali" },
                    { 109, "MLT", "Malta" },
                    { 110, "MHL", "Marshall Islands" },
                    { 111, "MRT", "Mauritania" },
                    { 112, "MUS", "Mauritius" },
                    { 113, "MEX", "Mexico" },
                    { 114, "FSM", "Micronesia" },
                    { 115, "MDA", "Moldova" },
                    { 116, "MCO", "Monaco" },
                    { 117, "MNG", "Mongolia" },
                    { 118, "MNE", "Montenegro" },
                    { 119, "MAR", "Morocco" },
                    { 120, "MOZ", "Mozambique" },
                    { 121, "MMR", "Myanmar" },
                    { 122, "NAM", "Namibia" },
                    { 123, "NRU", "Nauru" },
                    { 124, "NPL", "Nepal" },
                    { 125, "NLD", "Netherlands" },
                    { 126, "NZL", "New Zealand" },
                    { 127, "NIC", "Nicaragua" },
                    { 128, "NER", "Niger" },
                    { 129, "NGA", "Nigeria" },
                    { 130, "MKD", "North Macedonia" },
                    { 131, "NOR", "Norway" },
                    { 132, "OMN", "Oman" },
                    { 133, "PAK", "Pakistan" },
                    { 134, "PLW", "Palau" },
                    { 135, "PSE", "Palestine" },
                    { 136, "PAN", "Panama" },
                    { 137, "PNG", "Papua New Guinea" },
                    { 138, "PRY", "Paraguay" },
                    { 139, "PER", "Peru" },
                    { 140, "PHL", "Philippines" },
                    { 141, "POL", "Poland" },
                    { 142, "PRT", "Portugal" },
                    { 143, "QAT", "Qatar" },
                    { 144, "ROU", "Romania" },
                    { 145, "RUS", "Russia" },
                    { 146, "RWA", "Rwanda" },
                    { 147, "KNA", "Saint Kitts and Nevis" },
                    { 148, "LCA", "Saint Lucia" },
                    { 149, "VCT", "Saint Vincent and the Grenadines" },
                    { 150, "WSM", "Samoa" },
                    { 151, "SMR", "San Marino" },
                    { 152, "STP", "Sao Tome and Principe" },
                    { 153, "SAU", "Saudi Arabia" },
                    { 154, "SEN", "Senegal" },
                    { 155, "SRB", "Serbia" },
                    { 156, "SYC", "Seychelles" },
                    { 157, "SLE", "Sierra Leone" },
                    { 158, "SGP", "Singapore" },
                    { 159, "SVK", "Slovakia" },
                    { 160, "SVN", "Slovenia" },
                    { 161, "SLB", "Solomon Islands" },
                    { 162, "SOM", "Somalia" },
                    { 163, "ZAF", "South Africa" },
                    { 164, "ESP", "Spain" },
                    { 165, "LKA", "Sri Lanka" },
                    { 166, "SDN", "Sudan" },
                    { 167, "SUR", "Suriname" },
                    { 168, "SWE", "Sweden" },
                    { 169, "CHE", "Switzerland" },
                    { 170, "SYR", "Syria" },
                    { 171, "TWN", "Taiwan" },
                    { 172, "TJK", "Tajikistan" },
                    { 173, "TZA", "Tanzania" },
                    { 174, "THA", "Thailand" },
                    { 175, "TLS", "Timor Leste" },
                    { 176, "TGO", "Togo" },
                    { 177, "TON", "Tonga" },
                    { 178, "TTO", "Trinidad and Tobago" },
                    { 179, "TUN", "Tunisia" },
                    { 180, "TUR", "Turkey" },
                    { 181, "TKM", "Turkmenistan" },
                    { 182, "TUV", "Tuvalu" },
                    { 183, "UGA", "Uganda" },
                    { 184, "UKR", "Ukraine" },
                    { 185, "ARE", "United Arab Emirates" },
                    { 186, "GBR", "United Kingdom" },
                    { 187, "USA", "United States of America" },
                    { 188, "URY", "Uruguay" },
                    { 189, "UZB", "Uzbekistan" },
                    { 190, "VUT", "Vanuatu" },
                    { 191, "VAT", "Vatican City" },
                    { 192, "VEN", "Venezuela" },
                    { 193, "VNM", "Vietnam" },
                    { 194, "YEM", "Yemen" },
                    { 195, "ZMB", "Zambia" },
                    { 196, "ZWE", "Zimbabwe" }
                });

            migrationBuilder.InsertData(
                table: "LicenseTypes",
                columns: new[] { "LicenseTypeId", "Fees", "Title" },
                values: new object[,]
                {
                    { (byte)1, 20m, "Local" },
                    { (byte)2, 100m, "International" }
                });

            migrationBuilder.InsertData(
                table: "ServiceCategories",
                columns: new[] { "ServiceCategoryId", "Category" },
                values: new object[,]
                {
                    { (short)1, "Local Driving License" },
                    { (short)2, "International Driving License" },
                    { (short)3, "Passport" },
                    { (short)4, "National Identity Card" }
                });

            migrationBuilder.InsertData(
                table: "ServicesPurposes",
                columns: new[] { "ServicePurposeId", "Purpose" },
                values: new object[,]
                {
                    { (byte)1, "New" },
                    { (byte)2, "Renew" },
                    { (byte)3, "Replacement For Damage" },
                    { (byte)4, "Replacement For Lost" },
                    { (byte)5, "Release" },
                    { (byte)6, "Retake Test" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "BirthDate", "ConcurrencyStamp", "CountryId", "Email", "EmailConfirmed", "FirstName", "FourthName", "Gender", "ImagePath", "LockoutEnabled", "LockoutEnd", "NationalNo", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecondName", "SecurityStamp", "ThirdName", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), 0, "Test Address 1", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "b3e27cb1-7bfb-4722-bc67-0c9c96c51d4e", 1, "test1@test.com", true, "Test", "First", 0, "null", true, null, "1111111111", "TEST1@TEST.COM", "TESTUSER1", "AQAAAAIAAYagAAAAEPahXcemRXGRxKE3LxhXh/jtLc5gQV31AVLNkX2GAh0xArP6aYtL2TFmDaxZHUXKQw==", "0777777771", true, "User", "K2MDKSUEXFG6QCHLCWJLVREVWT7545X2", "One", false, "testuser1" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 0, "Test Address 2", new DateTime(2000, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "44f69173-726f-44d8-b4b0-7695e4bec210", 1, "test2@test.com", true, "Test", "Second", 1, "null", true, null, "2222222222", "TEST2@TEST.COM", "TESTUSER2", "AQAAAAIAAYagAAAAEPahXcemRXGRxKE3LxhXh/jtLc5gQV31AVLNkX2GAh0xArP6aYtL2TFmDaxZHUXKQw==", "0777777772", true, "User", "K2MDKSUEXFG6QCHLCWJLVREVWT7545X3", "Two", false, "testuser2" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), 0, "Test Address 3", new DateTime(2000, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "2f511f41-c3a1-4ebe-a55b-377487fac8b8", 1, "test3@test.com", true, "Test", "Third", 0, "null", true, null, "3333333333", "TEST3@TEST.COM", "TESTUSER3", "AQAAAAIAAYagAAAAEPahXcemRXGRxKE3LxhXh/jtLc5gQV31AVLNkX2GAh0xArP6aYtL2TFmDaxZHUXKQw==", "0777777773", true, "User", "K2MDKSUEXFG6QCHLCWJLVREVWT7545X4", "Three", false, "testuser3" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), 0, "Test Address 4", new DateTime(2000, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "a98af5d4-731c-489b-851c-86f23c66b3d1", 1, "test4@test.com", true, "Test", "Fourth", 1, "null", true, null, "4444444444", "TEST4@TEST.COM", "TESTUSER4", "AQAAAAIAAYagAAAAEPahXcemRXGRxKE3LxhXh/jtLc5gQV31AVLNkX2GAh0xArP6aYtL2TFmDaxZHUXKQw==", "0777777774", true, "User", "K2MDKSUEXFG6QCHLCWJLVREVWT7545X5", "Four", false, "testuser4" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), 0, "Test Address 5", new DateTime(2000, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "7c8f2e9b-4c1a-483d-b832-d35cb3f3f287", 1, "test5@test.com", true, "Test", "Fifth", 0, "null", true, null, "5555555555", "TEST5@TEST.COM", "TESTUSER5", "AQAAAAIAAYagAAAAEPahXcemRXGRxKE3LxhXh/jtLc5gQV31AVLNkX2GAh0xArP6aYtL2TFmDaxZHUXKQw==", "0777777775", true, "User", "K2MDKSUEXFG6QCHLCWJLVREVWT7545X6", "Five", false, "testuser5" }
                });

            migrationBuilder.InsertData(
                table: "ServicesFees",
                columns: new[] { "ServiceCategoryId", "ServicePurposeId", "Fees", "LastUpdate" },
                values: new object[,]
                {
                    { (short)1, (byte)1, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)2, (byte)1, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)3, (byte)1, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)4, (byte)1, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)1, (byte)2, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)2, (byte)2, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)3, (byte)2, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)4, (byte)2, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)1, (byte)3, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)2, (byte)3, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)3, (byte)3, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)4, (byte)3, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)1, (byte)4, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)2, (byte)4, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)3, (byte)4, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)4, (byte)4, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)1, (byte)5, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { (short)1, (byte)6, 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserId",
                table: "Admins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Applicataions_ServicePurposeId_ServiceCategoryId",
                table: "Applicataions",
                columns: new[] { "ServicePurposeId", "ServiceCategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_Applicataions_UpdatedByEmployeeId",
                table: "Applicataions",
                column: "UpdatedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Applicataions_UserId",
                table: "Applicataions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CountryId",
                table: "AspNetUsers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DetainedLicenses_ApplicationId",
                table: "DetainedLicenses",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_DetainedLicenses_CreatedByEmployee",
                table: "DetainedLicenses",
                column: "CreatedByEmployee");

            migrationBuilder.CreateIndex(
                name: "IX_DetainedLicenses_LocalDrivingLicenseId",
                table: "DetainedLicenses",
                column: "LocalDrivingLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_DetainedLicenses_ReleasedByEmployee",
                table: "DetainedLicenses",
                column: "ReleasedByEmployee");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_CreatedByEmployee",
                table: "Drivers",
                column: "CreatedByEmployee");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_UserId",
                table: "Drivers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_HiredByAdmin",
                table: "Employees",
                column: "HiredByAdmin");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InternationalDrivingLicenseApplications_ApplicationId",
                table: "InternationalDrivingLicenseApplications",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_InternationalDrivingLicenseApplications_LocalDrivingLicenseId",
                table: "InternationalDrivingLicenseApplications",
                column: "LocalDrivingLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_InternationalDrivingLicenses_InternationalDrivingLicenseApplicationID",
                table: "InternationalDrivingLicenses",
                column: "InternationalDrivingLicenseApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_InternationalDrivingLicenses_LocalDrivingLicenseId",
                table: "InternationalDrivingLicenses",
                column: "LocalDrivingLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalDrivingLicenseApplications_ApplicationId",
                table: "LocalDrivingLicenseApplications",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalDrivingLicenseApplications_LicenseClassId",
                table: "LocalDrivingLicenseApplications",
                column: "LicenseClassId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalDrivingLicenses_ApplicationId",
                table: "LocalDrivingLicenses",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalDrivingLicenses_CountryId",
                table: "LocalDrivingLicenses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalDrivingLicenses_CreatedByEmployee",
                table: "LocalDrivingLicenses",
                column: "CreatedByEmployee");

            migrationBuilder.CreateIndex(
                name: "IX_LocalDrivingLicenses_DriverId",
                table: "LocalDrivingLicenses",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalDrivingLicenses_LicenseClassId",
                table: "LocalDrivingLicenses",
                column: "LicenseClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesFees_ServiceCategoryId",
                table: "ServicesFees",
                column: "ServiceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAppointments_ApplicationId",
                table: "TestAppointments",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAppointments_CreatedByEmployeeId",
                table: "TestAppointments",
                column: "CreatedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAppointments_LocalDrivingLicenseApplicationId",
                table: "TestAppointments",
                column: "LocalDrivingLicenseApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAppointments_TestTypeId",
                table: "TestAppointments",
                column: "TestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_CreatedByEmployee",
                table: "Tests",
                column: "CreatedByEmployee");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TestAppointmentId",
                table: "Tests",
                column: "TestAppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TestTypeId",
                table: "Tests",
                column: "TestTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DetainedLicenses");

            migrationBuilder.DropTable(
                name: "EmployeeTypes");

            migrationBuilder.DropTable(
                name: "InternationalDrivingLicenses");

            migrationBuilder.DropTable(
                name: "LicenseTypes");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "InternationalDrivingLicenseApplications");

            migrationBuilder.DropTable(
                name: "TestAppointments");

            migrationBuilder.DropTable(
                name: "LocalDrivingLicenses");

            migrationBuilder.DropTable(
                name: "LocalDrivingLicenseApplications");

            migrationBuilder.DropTable(
                name: "TestTypes");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Applicataions");

            migrationBuilder.DropTable(
                name: "LicenseClasses");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "ServicesFees");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "ServiceCategories");

            migrationBuilder.DropTable(
                name: "ServicesPurposes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
