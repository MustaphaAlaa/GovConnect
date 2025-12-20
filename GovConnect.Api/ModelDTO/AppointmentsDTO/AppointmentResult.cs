using System.Runtime.InteropServices.JavaScript;

namespace ModelDTO.Appointments;

public class AppointmentResult
{
    public DateTime Date { get; set; }
    public   List<int> TimeIntervalIds  { get; set; }
    public string Status { get; set; } // Success or Failed
    public string Reason { get; set; } // Reason for failure, if applicable
    
}