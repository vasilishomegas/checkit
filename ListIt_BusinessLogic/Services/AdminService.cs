using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListIt_BusinessLogic.Services.Generics;
using ListIt_DataAccess.Repository;
using ListIt_DataAccessModel;
using ListIt_DomainModel;
using ListIt_DomainModel.DTO;

namespace ListIt_BusinessLogic.Services
{
    public class AdminService : Service<Admin, AdminDto>
    {
        private readonly AdminRepository _adminRepository;
        public AdminService() : base(new AdminRepository())
        {
            _adminRepository = (AdminRepository) _repository;
        }

        public override IEnumerable<AdminDto> GetAll()
        {
            return _repository.GetAll().Select(ConvertDBToDto).ToList();
        }

        protected override AdminDto ConvertDBToDto(Admin entity)
        {
            return StaticDBToDto(entity);
        }

        protected override Admin ConvertDtoToDB(AdminDto dto)
        {
            return StaticDtoToDB(dto);
        }

        public static Admin StaticDtoToDB(AdminDto adminDto)
        {
            if (adminDto == null) return null;
            return new Admin
            {
                username = adminDto.username,
                password = adminDto.password
            };
        }

        public static AdminDto StaticDBToDto(Admin admin)
        {
            return new AdminDto
            {
                username = admin.username,
                password = admin.password
            };
        }
        public AdminDto Login(string username, string password)
        {
            AdminRepository repo = new AdminRepository();
            var admin = repo.Get(username, password);
            //if (admin != null)
                return ConvertDBToDto(admin);
            
        }
    }
}
