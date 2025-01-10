using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Users;

namespace Models.ApplicationModels;



/*
 
##Will Require Check Service and Uint testing and update it whenever needing
    Upcoming update

    @@Add @@
    public decimal LicenseTypeFees { get; set; }

    @@from PaidFees@@
    public decimal ServiceFees { get; set; } 

    public decimal TotalPaidFees { get; set; }

    @@renaming from ServiceFees to AppFees@@ 
    public ServiceFees ServiceFees { get; set; }

  
     public LicenseType LicenseType  { get; set; }
    
 */
public class Application
{
    [Key] public int ApplicationId { get; set; }

    [Required][ForeignKey("User")] public Guid UserId { get; set; }

    public EnApplicationStatus ApplicationStatus { get; set; }

    public DateTime ApplicationDate { get; set; }

    public DateTime LastStatusDate { get; set; }

    public decimal PaidFees { get; set; }

    [Required] public byte ServicePurposeId { get; set; }
    [Required] public short ServiceCategoryId { get; set; }

    [ForeignKey("Employee")] public Guid? UpdatedByEmployeeId { get; set; }

    public User User { get; set; }
    public ServiceFees ServiceFees { get; set; }
    public Employee Employee { get; set; }
}
