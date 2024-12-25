﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Users;

namespace Models.Tests;

public class Test
{
    [Key] public int TestId { get; set; }

    [Required]
    [ForeignKey("TestAppointment")]
    public int TestAppointmentId { get; set; }

    [Required] public bool TestResult { get; set; }
    public string Notes { get; set; }

    [Required] [ForeignKey("Employee")] public Guid CreatedByEmployee { get; set; }

    public Employee Employee { get; set; }
    public TestAppointment TestAppointment { get; set; }
}