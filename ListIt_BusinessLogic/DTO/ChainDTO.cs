using ListIt_DomainModel;

namespace ListIt_BusinessLogic.DTO
{
    public class ChainDto
    {
        public int Id { get; set; }
        public ShopApiDto ShopApi { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
    }


}