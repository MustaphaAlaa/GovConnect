using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConfigurations
{
    public static class Sp_InsertAppointment
    {

        public static void Create(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetRequiredService<GovConnectDbContext>();
            context.Database.ExecuteSqlRaw(@"
                IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE type = 'P' AND name = 'SP_InsertAppointment')
                BEGIN
                    EXEC('              
                    CREATE PROCEDURE SP_InsertAppointment
                        @Day DATE,
                        @TestTypeId INT,
                        @TimeIntervalId INT
                    AS
                    BEGIN
                        IF NOT EXISTS (SELECT 1 FROM Appointments WHERE 
                                        TestTypeId = @TestTypeId AND
                                        AppointmentDay = @Day AND
                                        TimeIntervalId = @TimeIntervalId)
                        BEGIN
                            INSERT INTO Appointments (TestTypeId, TimeIntervalId, IsAvailable, AppointmentDay)
                            VALUES (@TestTypeId, @TimeIntervalId, 1, @Day);
                        END
                    END');
                END");
        }
    }
}
