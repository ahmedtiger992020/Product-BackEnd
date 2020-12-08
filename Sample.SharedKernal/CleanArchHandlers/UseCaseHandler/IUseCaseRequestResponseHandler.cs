using System.Threading.Tasks;

namespace Sample.SharedKernal
{
    public interface IUseCaseRequestResponseHandler<in TUseCaseRequest, /*out*/ TUseCaseResponse>
    {
        Task<bool> HandleUseCase(TUseCaseRequest _request, IOutputPort<ResultDto<TUseCaseResponse>> _response);
    }
}
