using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using Models.Tests;

namespace DataConfigurations.EntitiesConfiguration;

public class TestAppointmentConfiguration : IEntityTypeConfiguration<TestAppointment>
{
    public void Configure(EntityTypeBuilder<TestAppointment> builder)
    {
        builder.Property(testAppointment => testAppointment.AppointmentDate)
            .HasColumnType("Date");
        
        builder
            .HasOne<TimeInterval>(t => t.TimeInterval)
            .WithMany(t => t.TestAppointments); 
    }
}