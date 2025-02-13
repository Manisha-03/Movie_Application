using MovieApplicationBackend.Models;
using MovieAppRepository.Model;
using MovieAppRepository.Models;

namespace MovieAppServices.IServices
{
    public interface IUserServices
    {
        int AddUser(UserModel userAddModel);
        List<CountryModel> GetCountries();
        List<UserModel> GetUsers();
        Task<bool> UpdateUserStatusAsync(int customerId, bool? isActive);
    }
}
