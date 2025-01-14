using Models.Tests;
using Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDTO.TestsDTO;

public class TestDTO
{

    /// <summary>
    /// the unique identifier for the test
    /// </summary>
    [Key] public int TestId { get; set; }

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
    /// The Employee who create the result
    /// </summary>
    [Required] public Guid CreatedByEmployee { get; set; }
}
