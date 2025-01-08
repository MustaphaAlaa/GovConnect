﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Tests.Enums;

namespace ModelDTO.Appointments;

/// <summary>
/// Represents the Time Intervals
/// </summary>
public class TimeIntervalDTO
{
    /// <summary>
    /// The Unique identitfier for TimeInterval
    /// </summary>
    public int TimeIntervalId { get; set; }

    public EnHour Hour { get; set; }
    public EnHourStage Minute { get; set; }
}