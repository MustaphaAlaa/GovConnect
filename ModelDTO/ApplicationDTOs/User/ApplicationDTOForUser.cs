﻿using Models.ApplicationModels;
using Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDTO.ApplicationDTOs.User;
public class ApplicationDTOForUser
{

    [Key] public int ApplicationId { get; set; }

    [Required] public Guid UserId { get; set; }

    public byte ApplicationStatus { get; set; }

    public DateTime ApplicationDate { get; set; }

    public DateTime LastStatusDate { get; set; }

    public decimal PaidFees { get; set; }

    [Required] public byte ServicePurposeId { get; set; }
    [Required] public short ServiceCategoryId { get; set; }

}
