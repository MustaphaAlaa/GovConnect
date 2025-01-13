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

    /// <summary>
    /// Represent the RetakeTestApplications table
    /// </summary>
    public class RetakeTestApplication
    {

        /// <summary>
        /// The unique identifier for the table.
        /// </summary>

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RetakeTestApplicationId { get; set; }

        /// <summary>
        /// Foreign key referncing the associated TestTypes table.
        /// </summary>

        [ForeignKey("TestType")]
        [Required]
        public int TestTypeId { get; set; }

        /// <summary>
        /// Foreign key referncing the associated LocalDrivingLicenseApplications table.
        /// </summary>
        [ForeignKey("LocalDrivingLicenseApplication")]
        [Required]
        public int LocalDrivingLicenseApplicationId { get; set; }


        /// <summary>
        /// Foreign key referncing the associated Applications table.
        /// </summary>
        [ForeignKey("Application")]
        [Required]
        public int ApplicationId { get; set; }

        /// <summary>
        ///  Navigatino property for the associated TestType.
        /// </summary>
        public TestType TestType { get; set; }

        /// <summary>
        ///  Navigatino property for the associated LocalDrivingLicenseApplication.
        /// </summary>
        public LocalDrivingLicenseApplication LocalDrivingLicenseApplication { get; set; }

        /// <summary>
        ///  Navigatino property for the associated Application.
        /// </summary>
        public Application Application { get; set; }
    }
}
