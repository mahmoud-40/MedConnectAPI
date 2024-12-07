using System;
using System.ComponentModel.DataAnnotations;

namespace Medical.DTOs.Records;

public class UpdateRecordByProviderDTO : AddRecordByProviderDTO
{
    [Required]
    public int RecordId { get; set; }
}
