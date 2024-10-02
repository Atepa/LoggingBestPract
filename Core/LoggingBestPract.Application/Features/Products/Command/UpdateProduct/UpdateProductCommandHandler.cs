using MediatR;
using LoggingBestPract.Application.Interfaces.Repositories;

namespace LoggingBestPract.Application.Features.Products.Command.UpdateProduct
{
    public class UpdateProductCommandHandler :  IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler( 
            IProductRepository productRepository
        )
        {
            _productRepository = productRepository;
        }
        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var product =await _productRepository.GetAsync(x => x.Id == request.Id);

                if (product != null)
                {
                    // _logger.LogWarning("Ürün bulunamadı", request.BrandName);

                    return new UpdateProductCommandResponse()
                    {
                        IsSuccess = false,
                        Message = "Ürün bulunamadı."
                    };
                }
                
                product.Description = request.Description;
                product.Discount = request.Discount;
                product.BrandId = request.BrandId;
                product.Price = request.Price;
                product.BrandName = request.BrandName;

                var updatedProdcut = await _productRepository.UpdateAsync(product);
                await _productRepository.SaveChangesAsync();

                // _logger.LogInformation("Product '{updatedProdcut}' successfully created.", recordedBrand.Name);

                return new UpdateProductCommandResponse()
                {
                    IsSuccess = true,
                    Message = "Ürün başarıyla güncellendi",
                    Product = updatedProdcut
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
                return new UpdateProductCommandResponse()
                {
                    IsSuccess = false,
                    Message = e.Message
                };;
            }
        }
    }
}
