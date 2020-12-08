using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sample.Core.Interfaces.Repository;
using Sample.SharedKernal;

namespace Sample.Core.UseCases
{
    public sealed class ProductDeleteUseCase : BaseUseCase, IProductDeleteUseCase
    {
        public IProductRepository ProductRepository { get; set; }

        public async Task<bool> HandleUseCase(int _request, IOutputPort<ResultDto<bool>> _presenter)
        {
            //check if item found before deleted it
            Entities.Product product = await new ProductSharedMethods(MessageResource, ProductRepository).GetItemIfValid(_request, _culture);

            //delete the founded product 
            ProductRepository.HardDelete(product);
            //Commiting Changes in DB
            await UnitOfWork.Commit(_culture);

            _presenter.HandlePresenter(new ResultDto<bool>(true));
            return true;
        }
    }
}