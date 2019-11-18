using ListIt_BusinessLogic.DTO.Interfaces;
using ListIt_DomainModel;

namespace ListIt_BusinessLogic.DTO
{
    public class ShopApiDto : IDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }
}