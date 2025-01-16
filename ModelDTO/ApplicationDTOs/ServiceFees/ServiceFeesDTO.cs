
using System.ComponentModel.DataAnnotations;


namespace ModelDTO.ApplicationDTOs.Fees;


public class ServiceFeesDTO
{
    [Required] public byte ServicePurposeId { get; set; }

    [Required] public short ServiceCategoryId { get; set; }

    public decimal Fees { get; set; }

    [Required] public DateTime LastUpdate { get; set; }
}
