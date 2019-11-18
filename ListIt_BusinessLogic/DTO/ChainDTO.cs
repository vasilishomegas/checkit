using ListIt_BusinessLogic.DTO.Interfaces;
using ListIt_DomainModel;

namespace ListIt_BusinessLogic.DTO
{
    public class ChainDto : IDto
    {
        public int Id { get; set; }
        public ShopApiDto ShopApi { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
    }
}