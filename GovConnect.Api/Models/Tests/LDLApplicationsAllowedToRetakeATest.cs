using Models.ApplicationModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Tests
{
    /// <summary>
    /// Represents the local driving license applications that are allowed to retake a test.
    /// </summary>
    public class LDLApplicationsAllowedToRetakeATest
    {
        /// <summary>
        /// The unique identifier for the record.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// The foreign key referencing the associated local driving license application.
        /// </summary>
        [Required]
        [ForeignKey("LocalDrivingLicenseApplication")]
        public int LocalDrivingLicenseApplicationId { get; set; }

        /// <summary>
        /// The foreign key referencing the associated local driving license application.
        /// </summary>
        [Required]
        [ForeignKey("TestType")]
        public int TestTypeId { get; set; }

        /// <summary>
        /// Indicates whether the application is allowed to retake a test.
        /// </summary>
        [Required]
        public bool IsAllowedToRetakeATest { get; set; }

        /// <summary>
        /// Navigation property for the associated local driving license application.
        /// </summary>
        [NotMapped]
        public LocalDrivingLicenseApplication LocalDrivingLicenseApplication { get; set; }
        [NotMapped]
        public TestType TestType { get; set; }
    }
}
