using Models.ApplicationModels;
using Models.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Applications
{
    public class RetakeTestApplication
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RetakeTestApplicationId { get; set; }


        [ForeignKey("TestType")]
        [Required]
        public int TestTypeId { get; set; }

        [ForeignKey("LocalDrivingLicenseApplication")]
        [Required]
        public int LocalDrivingLicenseApplicationId { get; set; }

        [ForeignKey("Application")]
        [Required]
        public int ApplicationId { get; set; }


        public TestType TestType { get; set; }
        public LocalDrivingLicenseApplication LocalDrivingLicenseApplication { get; set; }
        public Application Application { get; set; }
    }
}
