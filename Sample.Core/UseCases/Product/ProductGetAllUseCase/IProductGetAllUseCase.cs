using Sample.SharedKernal;

namespace Sample.Core.UseCases
{
    public interface IProductGetAllUseCase : IUseCaseRequestResponseListHandler<ProductGetAllInputDto, ProductGetAllOutputDto>
    {
    }
}
