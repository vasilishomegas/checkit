using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_BusinessLogic.Services;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccessModel;
using ListIt_DomainInterface.Interfaces.Converter;
using ListIt_DomainInterface.Interfaces.Service;
using ListIt_DomainModel.DTO;
using ListIt_WebAPI.Controllers.Generics;

namespace ListIt_WebAPI.Controllers
{
    public class ProductsController : GenericController<Product, ProductDto>
    {
        private readonly IProductService<Product, ProductDto> _productService;
        public ProductsController() : this(new ProductService())
        {

        }

        public ProductsController(IProductService<Product, ProductDto> productService) : base (productService)
        {
            _productService = productService;
        }
    }
}