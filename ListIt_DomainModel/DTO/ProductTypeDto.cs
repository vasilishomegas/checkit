using ListIt_DomainModel.DTO.Interfaces;

namespace ListIt_DomainModel.DTO
{
    public class ProductTypeDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}