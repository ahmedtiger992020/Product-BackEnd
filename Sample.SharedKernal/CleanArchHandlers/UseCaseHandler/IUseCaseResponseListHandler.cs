using System.Threading.Tasks;

namespace Sample.SharedKernal
{
    public interface IUseCaseResponseListHandler</*out*/ TUseCaseResponse> 
    {
        Task<bool> HandleUseCase(IOutputPort<ListResultDto<TUseCaseResponse>> _response);
    }
}
