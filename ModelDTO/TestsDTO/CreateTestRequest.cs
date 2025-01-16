using System.ComponentModel.DataAnnotations;

namespace ModelDTO.TestsDTO;

public class CreateTestRequest
{



    /// <summary>
    /// The result of the test, it's onlt can be true (for pass) or false (for failure)
    /// The field is required
    /// </summary>
    [Required] public bool TestResult { get; set; }

    /// <summary>
    /// The forign key for the booked appointment
    /// The field is required
    /// </summary>
    [Required] public int BookingId { get; set; }

    /// <summary>
    /// if there any notes about the result
    /// The field is optional
    /// </summary>
    public string Notes { get; set; }

    /// <summary>
    /// The forign key for the employee created this result.
    /// The field is required
    /// </summary>
    [Required] public int CreatedByEmployeeId { get; set; }

}
