using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDTO.Application;

public class UpdateApplicationByEmployee
{
    [Key] public int Id { get; set; }

    [Required] public Guid ApplicantUserId { get; set; }
    public Guid UpdatedByEmployeeId { get; set; }

    public byte ApplicationStatus { get; set; }

}
