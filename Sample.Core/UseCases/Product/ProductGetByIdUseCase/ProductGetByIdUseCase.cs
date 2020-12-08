using Sample.SharedKernal;
using System.Threading.Tasks;
using Sample.Core.Interfaces.Repository;


namespace Sample.Core.UseCases
{
    public class ProductGetByIdUseCase : BaseUseCase, IProductGetByIdUseCase
    {
        #region Props  
        public IProductRepository ProductRepository { get; set; }

        #endregion

        public async Task<bool> HandleUseCase(int _request, IOutputPort<ResultDto<ProductGetByIdOutputDto>> _presenter)
        {
            //Getting Product By Id from shared Method to validate it found or not 
            Entities.Product product = await new ProductSharedMethods(MessageResource, ProductRepository).GetItemIfValid(_request, _culture);
            // mapping if found product in DB
            _presenter.HandlePresenter(new ResultDto<ProductGetByIdOutputDto>(Mapper.Map<ProductGetByIdOutputDto>(product)));
            return true;
        }
    }
}
