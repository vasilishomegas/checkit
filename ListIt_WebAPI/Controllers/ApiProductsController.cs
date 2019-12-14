using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_BusinessLogic.Services;
using ListIt_BusinessLogic.Services.Interface;
using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;
using ListIt_WebAPI.Controllers.Generics;

namespace ListIt_WebAPI.Controllers
{
    public class ApiProductsController : GenericController<ApiProduct, ApiProductDto>
    {
        private readonly IApiProductService _productService;
        public ApiProductsController() : this(new ProductService())
        {

        }

        public ApiProductsController(IApiProductService productService) : base(productService)
        {
            _productService = productService;
        }
    }
}