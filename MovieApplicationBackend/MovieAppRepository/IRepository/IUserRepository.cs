namespace MovieAppRepository.IRepository
{
    public interface IUserRepository
    {
        int InsertUpdateCustomer(
            string cityName, int countryId, string address, string address2, string district,
            int postalCode, int phone, int storeId, string firstName, string lastName,
            string email, bool active, int customerId);
    }
}
