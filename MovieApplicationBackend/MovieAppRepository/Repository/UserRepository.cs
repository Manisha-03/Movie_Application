using Microsoft.Extensions.Configuration;
using MovieAppRepository.IRepository;

namespace MovieAppRepository.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly CommonMethods _commonMethods;

        public UserRepository(IConfiguration configuration)
        {
            _commonMethods = new CommonMethods(configuration);
        }

        /// <summary>
        /// Inserts a new customer using the stored procedure and returns the new CustomerId.
        /// </summary>
        public int InsertUpdateCustomer(
            string cityName, int countryId, string address, string address2, string district,
            int postalCode, int phone, int storeId, string firstName, string lastName,
            string email, bool active, int customerId)
        {
            var parameters = new
            {
                CustomerId = customerId,
                CityName = cityName,
                CountryId = countryId,
                Address = address,
                Address2 = address2,
                District = district,
                PostalCode = postalCode,  
                Phone = phone,            
                StoreId = storeId,
                FirstName = firstName,      
                LastName = lastName,
                Email = email,
                Active = active
            };

            return _commonMethods.ExecuteStoredProcedureScalar("sp_InsertCustomer", parameters);
        }
    }
}
