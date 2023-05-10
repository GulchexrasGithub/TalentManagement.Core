// --------------------------------------------------------------- 
// Copyright (c)  the Gulchexra Burxonova
// Talent Management 
// ---------------------------------------------------------------

using TalentManagement.Core.Brokers.Loggings;
using TalentManagement.Core.Brokers.Tokens;

namespace TalentManagement.Core.Services.Foundations.Securities;

public partial class SecurityService : ISecurityService
{
    private readonly ITokenBroker tokenBroker;
    private readonly ILoggingBroker loggingBroker;

    public SecurityService(
        ITokenBroker tokenBroker,
        ILoggingBroker loggingBroker)
    {
        this.tokenBroker = tokenBroker;
        this.loggingBroker = loggingBroker;
    }

    public string CreateToken(User user) =>
    TryCatch(() =>
    {
        ValidateUser(user);

        return tokenBroker.GenerateJWT(user);
    });
}