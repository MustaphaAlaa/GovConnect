using Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Models.ApplicationModels;

namespace ModelDTO.ApplicationDTOs.User;

public abstract class CreateApplicationRequest
{
    [Required] public Guid UserId { get; set; } // i think it must be initned inside the controller (from cookie)
    [Required] public virtual byte ServicePurposeId { get; set; }
    [Required] public virtual short ServiceCategoryId { get; set; }

    private bool _isFirstTime = true;

    public bool IsFirstTimeOnly
    {
        get { return _isFirstTime; }
        private set
        {
            switch (ServiceCategoryId)
            {
                case (short)EnServiceCategory.International_Driving_License:
                    _isFirstTime = true;
                    break;
                case (short)EnServiceCategory.Local_Driving_License:
                    _isFirstTime = true;
                    break;
                case (short)EnServiceCategory.Passport:
                    _isFirstTime = true;
                    break;
                case (short)EnServiceCategory.National_Identity_Card:
                    _isFirstTime = true;
                    break;
                default:
                    _isFirstTime = false;
                    break;
            }
        }
    }
}
