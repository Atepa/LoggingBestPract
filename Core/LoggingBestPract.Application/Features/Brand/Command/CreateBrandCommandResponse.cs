using System.Text.Json.Serialization;
using LoggingBestPract.Application.CQRS;

namespace LoggingBestPract.Application.Features.Brand.Command;

public class CreateBrandCommandResponse : BaseResponse
{
    [JsonPropertyName("newBrand")]
    public Domain.Entities.Brands Brand { get; set; }
}