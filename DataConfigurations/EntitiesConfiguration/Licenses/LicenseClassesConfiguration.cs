using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.LicenseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConfigurations.EntitiesConfiguration
{
    public class LicenseClassesConfiguration : IEntityTypeConfiguration<LicenseClass>
    {
        public void Configure(EntityTypeBuilder<LicenseClass> builder)
        {
            builder.HasData(
   new LicenseClass
   {
       LicenseClassId = (short)EnLicenseClasses.Private_Driver_License,
       ClassName = "Private Driver License",
       ClassDescription = "Permits non-professional drivers to operate private vehicles, tourist taxis, agricultural tractors for personal use, and light transport vehicles up to 2000 kg",
       MinimumAllowedAge = 18,
       DefaultValidityLengthInMonths = 60,
       LicenseClassFees = 120.00M
   },
   new LicenseClass
   {
       LicenseClassId = (short)EnLicenseClasses.Third_Class_License,
       ClassName = "Third Class License",
       ClassDescription = "For professional drivers to operate taxis and buses up to 17 passengers, in addition to all vehicles permitted under private license",
       MinimumAllowedAge = 21,
       DefaultValidityLengthInMonths = 60,
       LicenseClassFees = 150.00M
   },
   new LicenseClass
   {
       LicenseClassId = (short)EnLicenseClasses.Second_Class_License,
       ClassName = "Second Class License",
       ClassDescription = "Permits operation of taxis, buses (17-26 passengers), transport vehicles, and heavy equipment. Requires 3 years experience with Third Class License",
       MinimumAllowedAge = 21,
       DefaultValidityLengthInMonths = 60,
       LicenseClassFees = 180.00M
   },
   new LicenseClass
   {
       LicenseClassId = (short)EnLicenseClasses.First_Class_License,
       ClassName = "First Class License",
       ClassDescription = "Permits operation of all vehicle types. Requires 3 years experience with Second Class License",
       MinimumAllowedAge = 21,
       DefaultValidityLengthInMonths = 60,
       LicenseClassFees = 200.00M
   },
   new LicenseClass
   {
       LicenseClassId = (short)EnLicenseClasses.Agricultural_Tractor_License,
       ClassName = "Agricultural Tractor License",
       ClassDescription = "Permits operation of single tractors or those with agricultural trailers",
       MinimumAllowedAge = 21,
       DefaultValidityLengthInMonths = 60,
       LicenseClassFees = 100.00M
   },
   new LicenseClass
   {
       LicenseClassId = (short)EnLicenseClasses.Metro_Tram_License,
       ClassName = "Metro/Tram License",
       ClassDescription = "Permits operation of metro trains and tram vehicles",
       MinimumAllowedAge = 21,
       DefaultValidityLengthInMonths = 60,
       LicenseClassFees = 150.00M
   },
   new LicenseClass
   {
       LicenseClassId = (short)EnLicenseClasses.Private_Motorcycle_License,
       ClassName = "Private Motorcycle License",
       ClassDescription = "Permits non-professional operation of motorcycles",
       MinimumAllowedAge = 18,
       DefaultValidityLengthInMonths = 60,
       LicenseClassFees = 80.00M
   },
   new LicenseClass
   {
       LicenseClassId = (short)EnLicenseClasses.Military_License,
       ClassName = "Military License",
       ClassDescription = "Permits operation of military vehicles, issued exclusively to armed forces personnel",
       MinimumAllowedAge = 21,
       DefaultValidityLengthInMonths = 60,
       LicenseClassFees = 0.00M
   },
   new LicenseClass
   {
       LicenseClassId = (short)EnLicenseClasses.Police_License,
       ClassName = "Police License",
       ClassDescription = "Permits operation of police vehicles, issued exclusively to police personnel",
       MinimumAllowedAge = 21,
       DefaultValidityLengthInMonths = 60,
       LicenseClassFees = 0.00M
   },
   new LicenseClass
   {
       LicenseClassId = (short)EnLicenseClasses.Test_Driving_License,
       ClassName = "Test Driving License",
       ClassDescription = "Issued to individuals responsible for testing rapid transport vehicles",
       MinimumAllowedAge = 21,
       DefaultValidityLengthInMonths = 12,
       LicenseClassFees = 100.00M
   },
   new LicenseClass
   {
       LicenseClassId = (short)EnLicenseClasses.Learner_Permit,
       ClassName = "Learner Permit",
       ClassDescription = "Temporary permit for individuals learning to drive vehicles",
       MinimumAllowedAge = 18,
       DefaultValidityLengthInMonths = 3,
       LicenseClassFees = 50.00M
   }
);
        }
    }
}
