using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using Models.Tests;

namespace DataConfigurations.EntitiesConfiguration;

/// <summary>
/// Configuration for Appointment Table
/// </summary>
public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.Property(testAppointment => testAppointment.AppointmentDay)
            .HasColumnType("Date");

        builder
            .HasOne<TimeInterval>(t => t.TimeInterval)
            .WithMany(t => t.Appointments);
    }
}