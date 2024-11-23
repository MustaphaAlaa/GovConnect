
using System.ComponentModel.DataAnnotations;


namespace ModelDTO.Application.Fees;


public class ApplicationFeesDTO
{
    [Required] public int ApplicationTypeId { get; set; }

    [Required] public int ApplicationForId { get; set; }

    public decimal Fees { get; set; }

    [Required] public DateTime LastUdpate { get; set; }
}
