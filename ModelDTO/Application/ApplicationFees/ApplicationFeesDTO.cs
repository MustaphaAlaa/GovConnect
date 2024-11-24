
using System.ComponentModel.DataAnnotations;


namespace ModelDTO.Application.Fees;


public class ApplicationFeesDTO
{
    [Required] public byte ApplicationTypeId { get; set; }

    [Required] public short ApplicationForId { get; set; }

    public decimal Fees { get; set; }

    [Required] public DateTime LastUdpate { get; set; }
}
