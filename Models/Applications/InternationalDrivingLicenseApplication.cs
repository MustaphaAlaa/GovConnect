﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.LicenseModels;
using Models.Types;

namespace Models.ApplicationModels;


// i'll deal with passport in Passport branch
public class InternationalDrivingLicenseApplication
{
    [Key] public int Id { get; set; }

    [Required][ForeignKey("Application")] public int ApplicationId { get; set; }
    [Required][ForeignKey("LocalDrivingLicense")] public int LicenseId { get; set; }
    //[Required][ForeignKey("Passport")] public int PassportId { get; set; }


    public Application Application { get; set; }
    public LocalDrivingLicense LocalDrivingLicense { get; set; }
    //public Passport Passport { get; set; }
}