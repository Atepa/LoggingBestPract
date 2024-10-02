using MediatR;

namespace LoggingBestPract.Application.Features.Auth.Command.Register
{
    public class RegisterCommandRequest : IRequest<ReqisterCommandResponse>
    {
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
