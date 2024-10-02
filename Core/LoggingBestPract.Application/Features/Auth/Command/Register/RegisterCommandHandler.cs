using MediatR;
using LoggingBestPract.Application.Interfaces.Repositories;
using LoggingBestPract.Domain.Entities;

namespace LoggingBestPract.Application.Features.Auth.Command.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, ReqisterCommandResponse>
    {
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ReqisterCommandResponse> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Email) ||
                    string.IsNullOrWhiteSpace(request.Password) ||
                    string.IsNullOrWhiteSpace(request.Name) ||
                    string.IsNullOrWhiteSpace(request.ConfirmPassword) ||
                    string.IsNullOrWhiteSpace(request.Phone) ||
                    string.IsNullOrWhiteSpace(request.Surname))
                {
                    return new ReqisterCommandResponse()
                    {
                        IsSuccess = false,
                        Message = "Zorunlu alanlar boş bırakılamaz"
                    };
                }

                if (request.Password.Length < 4)
                {
                    return new ReqisterCommandResponse()
                    {
                        IsSuccess = false,
                        Message = "Password en az 4 karakter uzunluğunda olmalı."
                    };
                }
                
                if (!request.Password.Equals(request.ConfirmPassword))
                {
                    return new ReqisterCommandResponse()
                    {
                        IsSuccess = false,
                        Message = "Password ile ConfirmPassword Aynı olmalıdır."
                    };
                }

                Users newUser = new Users(request.Name, request.Password, request.Surname, request.Phone, request.Email);

                var RecordedUser = await _userRepository.AddAsync(newUser);
                await _userRepository.SaveChangesAsync();

                return new ReqisterCommandResponse()
                {
                    IsSuccess = true,
                    Message = "Kullanıcı başarıyla kaydedildi.",
                    users = RecordedUser
                };
            }
            catch (Exception e)
            {
                return new ReqisterCommandResponse()
                {
                    IsSuccess = true,
                    Message = e.Message
                };
            }
        }
    }
}
