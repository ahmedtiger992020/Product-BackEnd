using Sample.SharedKernal;

namespace Sample.Core.UseCases
{
    public interface IProductGetByIdUseCase : IUseCaseRequestResponseHandler<int,ProductGetByIdOutputDto>
    {
    }
}
