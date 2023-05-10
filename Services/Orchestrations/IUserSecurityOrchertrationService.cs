// --------------------------------------------------------------- 
// Copyright (c)  the Gulchexra Burxonova
// Talent Management 
// ---------------------------------------------------------------

using System.Threading.Tasks;

namespace TalentManagement.Core.Services.Orchestrations
{
    public interface IUserSecurityOrchertrationService
    {
        ValueTask<User> CreateUserAccountAsync(User user, string requestUrl);
        //UserToken CreateUserToken(string email, string password);
    }
}