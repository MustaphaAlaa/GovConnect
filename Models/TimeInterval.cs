using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Tests;

namespace Models;

public class TimeInterval 
{
    [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      
    public int TimeIntervalId { get; set; }
    public int Hour { get; set; }
    public int Minute { get; set; }
    
    public IEnumerable<TestAppointment> TestAppointments { get; set; }
    
}