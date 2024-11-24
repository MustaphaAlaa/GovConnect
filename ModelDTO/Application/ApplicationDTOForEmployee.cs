using Models.ApplicationModels;
using Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDTO.Application;

public class ApplicationDTOForEmployee
{

    [Key] public int Id { get; set; }

    [Required] public Guid ApplicantUserId { get; set; }

    public byte ApplicationStatus { get; set; }

    public DateTime ApplicationDate { get; set; }

    public DateTime LastStatusDate { get; set; }

    public decimal PaidFees { get; set; }

    [Required] public byte ApplicationTypeId { get; set; }
    [Required] public short ApplicationForId { get; set; }
    public Guid? UpdatedByEmployeeId { get; set; }

}
