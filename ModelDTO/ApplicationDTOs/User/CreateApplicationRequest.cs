using Models.ApplicationModels;
using Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDTO.ApplicationDTOs.User;

public class CreateApplicationRequest
{
    [Required][ForeignKey("User")] public Guid ApplicantUserId { get; set; }
    [Required] public byte ApplicationTypeId { get; set; }
    [Required] public short ApplicationForId { get; set; }
}
