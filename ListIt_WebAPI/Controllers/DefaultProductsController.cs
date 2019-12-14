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
    public class DefaultProductsController : GenericController<DefaultProduct, DefaultProductDto>
    {
        private readonly IDefaultProductService _productService;
        public DefaultProductsController() : this(new ProductService())
        {

        }

        public DefaultProductsController(IDefaultProductService productService) : base(productService)
        {
            _productService = productService;
        }
    }
}