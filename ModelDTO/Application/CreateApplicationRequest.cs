using Models.Applications;
using Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDTO.Application;

public class CreateApplicationRequest
{
    [Required][ForeignKey("User")] public Guid ApplicantUserId { get; set; }

    public int ApplicationStatus { get; set; }

    public DateTime ApplicationDate { get; set; }

    public DateTime LastStatusDate { get; set; }

    public decimal PaidFees { get; set; }

    [Required] public int ApplicationTypeId { get; set; }
    [Required] public int ApplicationForId { get; set; }
}
