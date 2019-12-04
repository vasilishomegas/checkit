using ListIt_DomainModel.DTO.Interfaces;

namespace ListIt_DomainModel.DTO
{
    public class ChainDto : IDto
    {
        public int Id { get; set; }
        public ShopApiDto ShopApi { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
    }
}