using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class Create_IsTestTypePassed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string Sql = @"CREATE FUNCTION IsTestTypePassed(@LDLAppId int, @TestTypeId int)
                                RETURNS bit 
                                AS 
                                BEGIN
                                	DECLARE @Found bit = 0;

                                	SELECT @Found = 1 FROM TESTS 
                                	  Join Bookings ON Bookings.BookingId = Tests.BookingId
                                	    Where Bookings.LocalDrivingLicenseApplicationId = @LDLAppId
                                		 AND Bookings.TestTypeId = @TestTypeId;
                                		 RETURN @FOUND
                                END";

            migrationBuilder.Sql(Sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IsTestTypePassed");
        }
    }
}
