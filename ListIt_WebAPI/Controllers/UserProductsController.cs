using ListIt_DataAccessModel;
using ListIt_DomainModel.DTO;
using ListIt_WebAPI.Controllers.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListIt_BusinessLogic.Services;
using ListIt_DomainInterface.Interfaces.Service;

namespace ListIt_WebAPI.Controllers
{
    public class UserProductsController : GenericController<UserProduct, UserProductDto>
    {
        private readonly IUserProductService _productService;
        public UserProductsController() : this(new ProductService())
        {

        }

        public UserProductsController(IUserProductService productService) : base(productService)
        {
            _productService = productService;
        }
    }
}