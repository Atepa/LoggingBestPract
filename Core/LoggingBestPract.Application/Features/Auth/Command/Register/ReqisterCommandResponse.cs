using LoggingBestPract.Application.CQRS;
using LoggingBestPract.Domain.Entities;

namespace LoggingBestPract.Application.Features.Auth.Command.Register
{
    public class ReqisterCommandResponse : BaseResponse
    {
        public Users users { get; set; }
    }
}
