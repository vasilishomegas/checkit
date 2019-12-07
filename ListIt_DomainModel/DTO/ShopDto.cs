using ListIt_DomainModel.DTO.Interfaces;

namespace ListIt_DomainModel.DTO
{
    public class ShopDto : IDto
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public int Chain_id { get; set; }
    }
}