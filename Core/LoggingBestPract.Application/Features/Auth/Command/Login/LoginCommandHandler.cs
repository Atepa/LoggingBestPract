using MediatR;
using NLog;
using Serilog;
using LoggingBestPract.Application.Interfaces.Logging;
using LoggingBestPract.Application.Interfaces.Repositories;

namespace LoggingBestPract.Application.Features.Auth.Command.Login
{
    public class LoginCommandHandler :  IRequestHandler<LoginCommandRequest, LoginCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoggingHolder _loggingHolder;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public LoginCommandHandler(IUserRepository userRepository, ILoggingHolder loggingHolder)
        {
            _userRepository = userRepository;
            _loggingHolder = loggingHolder;
        }
        public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.Email))
                {
                    logger.Warn("{@TrackId} {@context} {@requestMail}",
                        _loggingHolder.TrackId,
                        "Password ve Email boş bırakalamaz!",
                        request.Email
                        );
                    
                    Log.Warning("{@TrackId} {@context} {@requestMail}",
                        _loggingHolder.TrackId,
                        "Password ve Email boş bırakalamaz!",
                        request);
                    
                    return new LoginCommandResponse()
                    {
                        IsSuccess = false,
                        Message = "Password ve Email boş bırakalamaz!"
                    };
                }

                var existUser = await _userRepository.GetAsync(x => x.Email == request.Email );
                if (existUser == null)
                {
                    logger.Warn("{@TrackId} {@context} {@requestMail}",
                        _loggingHolder.TrackId,
                        "Kullanıcı Bulunamadı",
                        request.Email
                    );
                    
                    Log.Warning("{@TrackId} {@context} {@requestMail}",
                        _loggingHolder.TrackId,
                        "Kullanıcı Bulunamadı",
                        request.Email);
                    
                    return new LoginCommandResponse()
                    {
                        IsSuccess = false,
                        Message = "Kullanıcı Bulunamadı"
                    }; 
                }
                if (string.Equals(existUser.Password, request.Password))
                {
                    Log.Information("{@TrackId} {@context} {@requestMail}",
                        _loggingHolder.TrackId,
                        "Giriş başarılı.",
                        request.Email);

                    logger.Info("{@TrackId} {@context} {@requestMail}",
                        _loggingHolder.TrackId,
                        "Giriş başarılı.",
                        request.Email
                    );
                    
                    return new LoginCommandResponse()
                    {
                        IsSuccess = true,
                        Message = "Giriş başarılı"
                    }; 
                }
                
                logger.Warn("{@TrackId} {@context} {@requestMail}",
                    _loggingHolder.TrackId,
                    "Giriş başarısız.",
                    request.Email
                );
                
                Log.Warning("{@TrackId} {@context} {@requestMail}",
                    _loggingHolder.TrackId,
                    "Giriş başarısız.",
                request.Email);
                
                return new LoginCommandResponse()
                {
                    IsSuccess = false,
                    Message = "Giriş başarısız. Lütfen email veya şifresini doğru giriniz."
                }; 
                
                
            }
            catch (Exception e)
            {
                logger.Error("{@TrackId} {@context} {@Exception} {@requestMail}",
                    _loggingHolder.TrackId,
                    "Bir hata meydana geldi",
                    e,
                    request.Email
                );
                
                Log.Error("{@TrackId} {@description} {@Exception} {@requestMail}",
                    _loggingHolder.TrackId,
                    "Bir hata meydana geldi",
                    e,
                    request.Email);

                return new LoginCommandResponse()
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }

        }
    }
}

