using Models.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDTO.TestsDTOs;
/// <summary>
/// Represent the types of tests in the system.
/// </summary>
public class TestTypeDTO
{
    /// <summary>
    /// The unique identifier for test type.
    /// </summary>
    [Key] public int TestTypeId { get; set; }

    /// <summary>
    /// The name of the test type.
    /// The field is required.
    /// </summary>
    [Required] public string TestTypeTitle { get; set; }

    /// <summary>
    /// For description or additional information about the test type.
    /// </summary>
    public string? TestTypeDescription { get; set; }

    /// <summary>
    /// The fees for this test's type
    /// </summary>
    [Required] public decimal TestTypeFees { get; set; }
}