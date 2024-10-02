using MediatR;

namespace LoggingBestPract.Application.Features.Brand.Command;

public class CreateBrandCommandRequest : IRequest<CreateBrandCommandResponse>
{
    public string BrandName { get; set; }
    
    public string ServiceAdress { get; set; }
}