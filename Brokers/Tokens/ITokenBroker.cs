// --------------------------------------------------------------- 
// Copyright (c)  the Gulchexra Burxonova
// Talent Management 
// ---------------------------------------------------------------

namespace TalentManagement.Core.Brokers.Tokens
{
    public interface ITokenBroker
    {
        string GenerateJWT(User user);
        string HashToken(string password);
    }
}