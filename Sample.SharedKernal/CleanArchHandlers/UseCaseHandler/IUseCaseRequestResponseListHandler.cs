using System.Threading.Tasks;

namespace Sample.SharedKernal
{
    public interface IUseCaseRequestResponseListHandler<in TUseCaseRequest, /*out*/ TUseCaseResponse>
    {
        Task<bool> HandleUseCase(TUseCaseRequest request, IOutputPort<ListResultDto<TUseCaseResponse>> outputPort);
    }

   
}
