using MovieApplicationBackend.Models;
using MovieAppRepository.IRepository;
using MovieAppRepository.Model;
using MovieAppRepository.Models;
using MovieAppServices.IServices;

namespace MovieAppServices.Services
{
    public class UserService : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly MovieApplicationContext _context;

        public UserService(IUserRepository userRepository, MovieApplicationContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        /// <summary>
        /// Inserts a new user and returns the new User ID.
        /// </summary>
        public int AddUser(UserModel userAddModel)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Customer customer;

                    // Step 1: Check if the City exists, if not, add it
                    var city = _context.Cities.FirstOrDefault(c => c.City1 == userAddModel.CityName);
                    if (city == null)
                    {
                        city = new City
                        {
                            City1 = userAddModel.CityName,
                            CountryId = userAddModel.CountryId, // Assuming you have CountryId
                            LastUpdate = DateTime.Now
                        };
                        _context.Cities.Add(city);
                        _context.SaveChanges();
                    }

                    short cityId = city.CityId; // Get CityId after insertion

                    // Step 2: Check if the customer exists
                    customer = _context.Customers.FirstOrDefault(c => c.CustomerId == userAddModel.CustomerId);

                    if (customer != null)
                    {
                        // Step 3: Update existing customer details
                        customer.StoreId = userAddModel.StoreId;
                        customer.FirstName = userAddModel.FirstName;
                        customer.LastName = userAddModel.LastName;
                        customer.Email = userAddModel.Email;
                        customer.Active = userAddModel.Active;
                        customer.LastUpdate = DateTime.Now;

                        // Step 4: Update existing address
                        var address = _context.Addresses.FirstOrDefault(a => a.AddressId == customer.AddressId);
                        if (address != null)
                        {
                            address.Address1 = userAddModel.Address;
                            address.Address2 = userAddModel.Address2;
                            address.District = userAddModel.District;
                            address.CityId = cityId;
                            address.PostalCode = userAddModel.PostalCode;
                            address.Phone = userAddModel.Phone;
                            address.LastUpdate = DateTime.Now;
                        }

                        _context.SaveChanges();
                    }
                    else
                    {
                        // Step 5: Insert new Address
                        var address = new Address
                        {
                            Address1 = userAddModel.Address,
                            Address2 = userAddModel.Address2,
                            District = userAddModel.District,
                            CityId = cityId, 
                            PostalCode = userAddModel.PostalCode,
                            Phone = userAddModel.Phone,
                            LastUpdate = DateTime.Now
                        };
                        _context.Addresses.Add(address);
                        _context.SaveChanges(); 

                        // Step 6: Insert new Customer
                        customer = new Customer
                        {
                            StoreId = userAddModel.StoreId,
                            FirstName = userAddModel.FirstName,
                            LastName = userAddModel.LastName,
                            Email = userAddModel.Email,
                            AddressId = address.AddressId, // Set AddressId
                            Active = userAddModel.Active,
                            CreateDate = DateTime.Now,
                            LastUpdate = DateTime.Now
                        };
                        _context.Customers.Add(customer);
                        _context.SaveChanges(); // Get generated CustomerId
                    }

                    transaction.Commit();
                    return customer.CustomerId;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public List<CountryModel> GetCountries()
        {
            return _context.Countries
                .Select(c => new CountryModel
                {
                    CountryId = c.CountryId,
                    Country1 = c.Country1 // Assuming Country1 is the column name in DB
                })
                .ToList();
        }



        public List<UserModel> GetUsers()
        {
            return _context.Customers
                .Select(c => new UserModel
                {
                    CustomerId = c.CustomerId,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    Active = c.Active
                })
                .ToList();
        }

        public async Task<bool> UpdateUserStatusAsync(int customerId, bool? isActive)
        {
            var user = await _context.Customers.FindAsync(customerId);
            if (user == null)
            {
                return false; // User not found
            }

            user.Active = isActive;
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
