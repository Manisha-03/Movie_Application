namespace MovieAppRepository.Model
{
    public class UserModel
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public short CountryId { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public byte? StoreId { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public bool? Active { get; set; }
        public int? CityId { get; set; }
    }
}
