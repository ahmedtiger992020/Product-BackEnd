
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Core.UseCases;
using Sample.SharedKernal;


namespace Sample.Web.Contollers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : BaseController
    {
        #region Props
        public IProductAddUseCase AddProductUseCase { get; set; }
        public IProductUpdateUseCase UpdateProductUseCase { get; set; }
        public IProductGetByIdUseCase GetByIdUseCase { get; set; }
        public IProductDeleteUseCase DeleteProductUseCase { get; set; }
        public IProductGetAllUseCase GetAllUseCase { get; set; }
        public IProductGetDDLUseCase GetProductDDLUseCase { get; set; }
        public OutputPort<ResultDto<bool>> Presenter { get; set; }
        public OutputPort<ResultDto<ProductGetByIdOutputDto>> GetByIdPresenter { get; set; }
        public OutputPort<ListResultDto<ProductGetAllOutputDto>> GetAllPresenter { get; set; }
        public OutputPort<ListResultDto<IdNameDto>> GetProductDDLPresenter { get; set; }

        #endregion

        #region APIs
        /// <summary>
        /// Add New Product
        /// </summary>
        /// <param name="_request">Product Information</param>
        /// <returns>Succes if added otherwise faild</returns>
        [HttpPost]
        public async Task<ActionResult<ResultDto<bool>>> Add([FromBody] ProductAddInputDto _request)
        {
            await AddProductUseCase.HandleUseCase(_request, Presenter);
            return Presenter.Result;
        }
        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="_request">Updated Information</param>
        /// <returns>Succes if updated otherwise faild</returns>
        [HttpPut]
        public async Task<ActionResult<ResultDto<bool>>> Update([FromBody] ProductUpdateInputDto _request)
        {
            await UpdateProductUseCase.HandleUseCase(_request, Presenter);
            return Presenter.Result;
        }
        /// <summary>
        /// Delete Product
        /// </summary>
        /// <param name="_request">Product Id</param>
        /// <returns>Succes if deleted otherwise faild</returns>
        [HttpDelete]
        public async Task<ActionResult<ResultDto<bool>>> Delete(int request)
        {
            await DeleteProductUseCase.HandleUseCase(request, Presenter);
            return Presenter.Result;
        }
        /// <summary>
        /// Get Product By Id
        /// </summary>
        /// <param name="request">ProductId</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ResultDto<ProductGetByIdOutputDto>>> GetById(int request)
        {
            await GetByIdUseCase.HandleUseCase(request, GetByIdPresenter);
            return GetByIdPresenter.Result;
        }

        [HttpPost]
     

        public async Task<ActionResult<ListResultDto<ProductGetAllOutputDto>>> GetAll([FromBody] ProductGetAllInputDto request)
        {
            await GetAllUseCase.HandleUseCase(request, GetAllPresenter);
            return GetAllPresenter.Result;
        }
        /// <summary>
        /// Get Products for Drop Down List 
        /// </summary>
        /// <returns>List of Products</returns>
        [HttpGet]
        public async Task<ActionResult<ListResultDto<IdNameDto>>> GetProductDDL()
        {
            await GetProductDDLUseCase.HandleUseCase(GetProductDDLPresenter);
            return GetProductDDLPresenter.Result;
        }


        #endregion
    }

}
