using System.Text.Json.Serialization;
using LoggingBestPract.Application.CQRS;

namespace LoggingBestPract.Application.Features.Products.Command.CreateProduct;

public class CreateProductCommandResponse : BaseResponse
{
    [JsonPropertyName("NewProduct")]
    public Domain.Entities.Products Product { get; set; }
}
