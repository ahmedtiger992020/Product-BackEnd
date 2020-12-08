using System.Net;

namespace Sample.SharedKernal
{
    public sealed class OutputPort<T> : IOutputPort<T> where T : ResultBaseDto
    {
        public T Result { get; set; }
        public void HandlePresenter(T response)
        {
            Result = response;
        }
    }
}
