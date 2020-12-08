namespace Sample.SharedKernal
{
    public interface IOutputPort<in TUseCaseResponse>
    {
        void HandlePresenter(TUseCaseResponse response);
    }
}
