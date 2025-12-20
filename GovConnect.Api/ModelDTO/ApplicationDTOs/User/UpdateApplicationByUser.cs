using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDTO.ApplicationDTOs.User;

public class UpdateApplicationByUser
{
    [Key] public int Id { get; set; }

    [Required] public Guid ApplicantUserId { get; set; }

    [Required] public byte ApplicationPurposeId { get; set; }
    [Required] public short ServiceCategoryId { get; set; }
}
