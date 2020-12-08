using System.Threading.Tasks;

namespace Sample.SharedKernal
{
    public interface IUseCaseResponseHandler</*out*/ TUseCaseResponse> 
    {
        Task<bool> HandleUseCase(IOutputPort<ResultDto<TUseCaseResponse>> _response);
    }
}
