// --------------------------------------------------------------- 
// Copyright (c)  the Gulchexra Burxonova
// Talent Management 
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace TalentManagement.Core.Services.Processings.Users
{
    public interface IUserProcessingService
    {
        User RetrieveUserByCredentails(string email, string password);
        ValueTask<Guid> VerifyUserByIdAsync(Guid userId);
        ValueTask<Guid> ActivateUserByIdAsync(Guid userId);
    }
}