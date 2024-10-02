using System.Text.Json.Serialization;
using LoggingBestPract.Application.CQRS;
using LoggingBestPract.Domain.Entities;

namespace LoggingBestPract.Application.Features.Products.Command.UpdateProduct;

public class UpdateProductCommandResponse : BaseResponse
{
    [JsonPropertyName("NewProduct")]
    public Domain.Entities.Products Product { get; set; }
}
