using Models.ApplicationModels;
using Models.Tests;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ModelDTO.TestsDTO;

public class LDLApplicationsAllowedToRetakeATestDTO
{

    /// <summary>
    /// The unique identifier for the record.
    /// </summary> 
    public int Id { get; set; }

    /// <summary>
    /// The foreign key referencing the associated local driving license application.
    /// </summary>
    [Required]
    public int LocalDrivingLicenseApplicationId { get; set; }

    /// <summary>
    /// The foreign key referencing the associated local driving license application.
    /// </summary>
    [Required]
    public int TestTypeId { get; set; }

    /// <summary>
    /// Indicates whether the application is allowed to retake a test.
    /// </summary>
    [Required]
    public bool IsAllowedToRetakeATest { get; set; }
}
