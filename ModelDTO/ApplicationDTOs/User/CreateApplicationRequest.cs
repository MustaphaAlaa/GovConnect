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
    [Required] public Guid UserId { get; set; }
    [Required] public byte ApplicationPurposeId { get; set; }
    [Required]  public virtual short ServiceCategoryId { get; set; }

    private bool _isFirstTime = true;

    public bool IsFirstTimeOnly
    {
        get { return _isFirstTime; }
        set
        {
            switch (ServiceCategoryId)
            {
                case (short)EnServiceCategory.InternationalLicense:
                    _isFirstTime = true;
                    break;
                case (short)EnServiceCategory.LocalLicense:
                    _isFirstTime = true;
                    break;
                case (short)EnServiceCategory.Passport:
                    _isFirstTime = true;
                    break;
                case (short)EnServiceCategory.NationalNumberId:
                    _isFirstTime = true;
                    break;
                default:
                    _isFirstTime = false;
                    break;
            }
        }
    }
}