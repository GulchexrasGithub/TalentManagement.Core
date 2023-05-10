// --------------------------------------------------------------- 
// Copyright (c)  the Gulchexra Burxonova
// Talent Management 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using TalentManagement.Core.Models.Orchestrations.UserTokens;

namespace TalentManagement.Core.Services.Orchestrations
{
    public interface IUserSecurityOrchestrationService
    {
        ValueTask<User> CreateUserAccountAsync(User user, string requestUrl);
        UserToken CreateUserToken(string email, string password);
    }
}