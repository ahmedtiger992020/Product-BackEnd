using Sample.SharedKernal;
using System.Threading.Tasks;
using Sample.Core.Interfaces.Repository;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Core.UseCases
{
    public class ProductGetAllUseCase : BaseUseCase, IProductGetAllUseCase
    {
        #region Props  
        public IProductRepository ProductRepository { get; set; }
        #endregion
        public async Task<bool> HandleUseCase(ProductGetAllInputDto _request, IOutputPort<ListResultDto<ProductGetAllOutputDto>> _presenter)
        {

            //Filtering input to check validation inputs from UI
            #region Filter
            Expression<Func<Entities.Product, bool>> filter = x =>
                 (string.IsNullOrEmpty(_request.Name) || x.Name.Contains(_request.Name.Trim()))
                && (_request.Price == default(double) || x.Price == _request.Price);
            #endregion

            //Sorting Expressions to Handle Sorting from UI
            #region Sorting
            Expression<Func<Entities.Product, string>> sorting = null;
            switch (_request.SortingModel.SortingExpression)
            {
                case "Name": sorting = s => s.Name; break;
                case "Photo": sorting = s => s.Photo; break;
                case "Price": sorting = s => s.Price.ToString(); break;
                default: sorting = s => s.Id.ToString(); break;
            }
            #endregion
            //Get Pagging Result ServerSide Pagging 
            List<Entities.Product> products = await ProductRepository.GetPageAsync(_request.Paging.PageNumber, _request.Paging.PageSize, filter, sorting, _request.SortingModel.SortingDirection);

            products.ForEach(a =>
            {
                a.Photo = $"{Configuration.GetSection("FilesPaths").GetSection("ServerUrl").Value}{a.Photo}.png";
            });

            _presenter.HandlePresenter(new ListResultDto<ProductGetAllOutputDto>(Mapper.Map<List<ProductGetAllOutputDto>>(products), await ProductRepository.GetCountAsync(filter)));
            return true;
        }
    }
}
