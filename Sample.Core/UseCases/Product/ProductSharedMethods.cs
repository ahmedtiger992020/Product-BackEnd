using System;
using System.Linq;
using System.Threading.Tasks;
using Sample.Core.Interfaces.Repository;
using Sample.SharedKernal.Exceptions;
using Sample.SharedKernal.Localization;

namespace Sample.Core.UseCases
{
    public class ProductSharedMethods
    {
        #region Vars
        private readonly ILocalizationReader _messageResource;
        private readonly IProductRepository _productRepository;

        #endregion

        #region Ctor
        public ProductSharedMethods(ILocalizationReader messageResource, IProductRepository productRepository)
        {
            _messageResource = messageResource;
            _productRepository = productRepository;

        }
        #endregion

        internal async Task<bool> ValidateToSave(ProductUpdateInputDto _request, string _culture, bool _isUpdate)
        {
            #region Product Input Validation
            if (_request == null)
                throw new ValidationsException(_messageResource.InvalidId(_culture));
            if (_isUpdate && _request.Id <= default(int))
                throw new ValidationsException(_messageResource.InvalidRequest(_culture));

            if (_isUpdate && _request.Id >= default(int))
            {
                if (await GetItemIfValid(_request.Id, _culture) == null)
                {
                    throw new ValidationsException(_messageResource.ProductNotFound(_culture));
                }
            }
            if (string.IsNullOrEmpty(_request.Name))
                throw new ValidationsException(_messageResource.InvalidName(_culture));
            if (string.IsNullOrEmpty(_request.Photo))
                throw new ValidationsException(_messageResource.InvalidPhoto(_culture));
            if (_request.Price == 0)
                throw new ValidationsException(_messageResource.InvalidPrice(_culture));
      

            #endregion

            #region DB Validation
          
            if (await _productRepository.GetAnyAsync(us => us.Name == _request.Name 
                                                  && us.Id != _request.Id))
                throw new ValidationsException(_messageResource.NameAlreadyExisit(_culture));
                        
            #endregion
            
            return true;
        }

        internal async Task<Entities.Product> GetItemIfValid(int _requestId, string _culture)
        {
            #region Product Input Validation
            if (_requestId <= default(int))
                throw new ValidationsException(_messageResource.InvalidId(_culture));
            #endregion

            #region DB Validation
            Entities.Product exisitProduct = await _productRepository.GetFirstOrDefaultAsync(x => x.Id == _requestId);
            if (exisitProduct == null)
                throw new ValidationsException(_messageResource.ProductNotFound(_culture));
       
            #endregion
            return exisitProduct;
        }

    }
}
