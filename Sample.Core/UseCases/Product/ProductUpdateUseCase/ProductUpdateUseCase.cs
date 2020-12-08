using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Sample.Core.Entities;
using Sample.Core.Interfaces.Repository;
using Sample.SharedKernal;
using Sample.SharedKernal.Files;

namespace Sample.Core.UseCases
{
    public sealed class ProductUpdateUseCase : BaseUseCase, IProductUpdateUseCase
    {
        public IProductRepository ProductRepository { get; set; }

        public async Task<bool> HandleUseCase(ProductUpdateInputDto _request, IOutputPort<ResultDto<bool>> _presenter)
        {
            ProductSharedMethods productSharedMethod = new ProductSharedMethods(MessageResource, ProductRepository);

            //validate Updated Input Request Product
            await productSharedMethod.ValidateToSave(Mapper.Map<ProductUpdateInputDto>(_request), _culture, true);

            //validate Founded Product or not  
            Entities.Product oldProduct = await productSharedMethod.GetItemIfValid(_request.Id, _culture);
            //mappind the founded Product to prepare update it in DB
            if (_request.Photo != null&& !_request.Photo.Contains("http"))
                oldProduct.Photo = new FileOperation(Configuration).SaveFile(new SharedKernal.Files.Dto.FileDto { File = _request.Photo, Name = _request.Name });
            else  
                _request.Photo = oldProduct.Photo;
            
            oldProduct = Mapper.Map(_request, oldProduct);

            #region Save The New Files Physically
            #endregion
            oldProduct.LastUpdated = DateTime.Now;
            //Update Product Entity 
            ProductRepository.Update(oldProduct);

            //Commit Product Entity
            await UnitOfWork.Commit(_culture);

            _presenter.HandlePresenter(new ResultDto<bool>(true));
            return true;
        }
    }
}