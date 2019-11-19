using ListIt_DomainModel.DTO.Interfaces;

namespace ListIt_DomainModel.DTO
{
    public class ShopApiDto : IDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }
}