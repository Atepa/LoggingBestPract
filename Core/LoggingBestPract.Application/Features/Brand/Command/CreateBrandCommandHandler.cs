using MediatR;
using Microsoft.EntityFrameworkCore;
using LoggingBestPract.Application.Interfaces.Repositories;

namespace LoggingBestPract.Application.Features.Brand.Command;

public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommandRequest, CreateBrandCommandResponse>
{
    private readonly IBrandRepository _brandRepository;

    public CreateBrandCommandHandler(
        IBrandRepository brandRepository
    )
    {
        _brandRepository = brandRepository;
    }

    public async Task<CreateBrandCommandResponse> Handle(CreateBrandCommandRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var brand = await _brandRepository.GetAsync(x => x.Name == request.BrandName);

            if (brand != null)
            {
                // _logger.LogWarning("Attempt to create brand '{BrandName}' that already exists.", request.BrandName);

                return new CreateBrandCommandResponse()
                {
                    IsSuccess = false,
                    Message = "Brand zaten kayıtlı"
                };
            }

            var newBrand = new Domain.Entities.Brands(request.BrandName, request.ServiceAdress);

            var recordedBrand = await _brandRepository.AddAsync(newBrand);
            await _brandRepository.SaveChangesAsync();

            //stargatteki trackId li tanımlamaları yap
            // _logger.LogInformation($"Brand '{recordedBrand.Name}' successfully created.");

            return new CreateBrandCommandResponse()
            {
                IsSuccess = true,
                Message = "Brand başarıyla kaydedildi.",
                Brand = recordedBrand
            };
        }
        catch (DbUpdateConcurrencyException ex)
        {
            // _logger.LogError(ex, "Concurrency conflict during save for brand '{request.BrandName}'");
            return new CreateBrandCommandResponse()
            {
                IsSuccess = false,
                Message = $"A concurrency conflict occurred during the save operation. Message: {ex.Message}"
            };
        }
        catch (HttpRequestException ex)
        {
            // _logger.LogError(ex, "HTTP request failed while trying to communicate with external service.");
            return new CreateBrandCommandResponse()
            {
                IsSuccess = false,
                Message = $"External service call failed. Message: {ex.Message}"
            };
        }
        catch (TimeoutException ex)
        {
            // _logger.LogError(ex, "Operation timed out");
            return new CreateBrandCommandResponse()
            {
                IsSuccess = false,
                Message = $"Operation timed out. Message: {ex.Message}"
            };
        }
        catch (Exception ex)
        {
            // _logger.LogError(ex, "An unexpected error occurred");
            return new CreateBrandCommandResponse()
            {
                IsSuccess = false,
                Message = $"Unexpected error. Message: {ex.Message}"
            };
        }
    }
}