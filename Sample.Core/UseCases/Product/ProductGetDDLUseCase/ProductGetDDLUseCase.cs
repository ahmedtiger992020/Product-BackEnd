using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sample.Core.Interfaces.Repository;
using Sample.SharedKernal;

namespace Sample.Core.UseCases
{
    public sealed class ProductGetDDLUseCase : BaseUseCase, IProductGetDDLUseCase
    {
        public IProductRepository ProductRepository { get; set; }

        public async Task<bool> HandleUseCase(IOutputPort<ListResultDto<IdNameDto>> _response)
        {
            //Get All Product to using As DropDownList 
            IEnumerable<Entities.Product> DataList = await ProductRepository.GetWhereAsync();

            //Return All Results if Found
            _response.HandlePresenter(new ListResultDto<IdNameDto>(Mapper.Map<List<IdNameDto>>(DataList), DataList.Count()));
            return true;
        }
    }
}