using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Sample.Core.Interfaces.Repository;
using Sample.SharedKernal;
using Sample.SharedKernal.Files;

namespace Sample.Core.UseCases
{
    public sealed class ProductAddUseCase : BaseUseCase, IProductAddUseCase
    {
        public IProductRepository ProductRepository { get; set; }
        public async Task<bool> HandleUseCase(ProductAddInputDto _request, IOutputPort<ResultDto<bool>> _presenter)
        {
            //calling SharedMethod To handle All Validations 
            await new ProductSharedMethods(MessageResource, ProductRepository).ValidateToSave(Mapper.Map<ProductUpdateInputDto>(_request), _culture, false);

            //mapping inputRequest to entity to preparing Saving entity to DB 
            Entities.Product newProduct = Mapper.Map<Entities.Product>(_request);

            #region Save The New Files Physically
            if (_request.Photo != null)
            {
                newProduct.Photo =new FileOperation(Configuration).SaveFile(new SharedKernal.Files.Dto.FileDto { File = _request.Photo, Name = _request.Name });
            }
            #endregion
            //saving New Product
            await ProductRepository.InsertAsync(newProduct);

            //CommitChanges after Add New Product
            await UnitOfWork.Commit(_culture);

            //return to Presenter success Added 
            _presenter.HandlePresenter(new ResultDto<bool>(true));
            return true;
        }


    }
}