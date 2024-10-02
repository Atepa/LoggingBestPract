using MediatR;
using LoggingBestPract.Application.Interfaces.Repositories;
using LoggingBestPract.Domain.Entities;

namespace LoggingBestPract.Application.Features.Products.Command.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler( 
            IProductRepository productRepository
            )
        {
            _productRepository = productRepository;
        }
        
        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = _productRepository.GetAsync(x => x.Title == request.Title);

            if (product is null)
            {
                return new CreateProductCommandResponse()
                {
                    IsSuccess = false,
                    Message = "Bu ürün zaten kayıtlı"
                };
            }
            
            try
            {
                var newProduct = new Domain.Entities.Products(request.Title, 
                    request.Description, 
                    request.BrandId, 
                    request.Price, 
                    request.Discount,
                    request.BrandName);
                
                // log
                var recordedProduct=await _productRepository.AddAsync(newProduct);
                await _productRepository.SaveChangesAsync();

                return new CreateProductCommandResponse()
                {
                    IsSuccess = true,
                    Message = "Kayıt başarılı",
                    Product = recordedProduct
                };
            }
            catch (Exception e)
            {
                // log atılacak
                Console.WriteLine(e);
                
                return new CreateProductCommandResponse()
                {
                    IsSuccess = false,
                    Message = "Kayıt başarılı",
                };
            }
        }
    }
}