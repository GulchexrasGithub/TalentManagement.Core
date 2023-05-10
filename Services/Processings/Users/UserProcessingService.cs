// --------------------------------------------------------------- 
// Copyright (c)  the Gulchexra Burxonova
// Talent Management 
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using TalentManagement.Core.Brokers.DateTimes;
using TalentManagement.Core.Brokers.Loggings;
using TalentManagement.Core.Services.Foundations.Users;

namespace TalentManagement.Core.Services.Processings.Users
{
    public partial class UserProcessingService : IUserProcessingService
    {
        private readonly IUserService userService;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public UserProcessingService(
            IUserService userService,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.userService = userService;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public User RetrieveUserByCredentails(string email, string password) =>
        TryCatch(() =>
        {
            ValidateEmailAndPassword(email, password);
            IQueryable<User> allUser = this.userService.RetrieveAllUsers();

            return allUser.FirstOrDefault(retrievedUser => retrievedUser.Email.Equals(email)
                    && retrievedUser.Password.Equals(password));
        });

        public async ValueTask<Guid> VerifyUserByIdAsync(Guid userId)
        {
            User maybeUser = await this.userService.RetrieveUserByIdAsync(userId);
            maybeUser.UpdatedDate = this.dateTimeBroker.GetCurrentDateTime();
            await this.userService.ModifyUserAsync(maybeUser);

            return maybeUser.Id;
        }

        public async ValueTask<Guid> ActivateUserByIdAsync(Guid userId)
        {
            User maybeUser = await this.userService.RetrieveUserByIdAsync(userId);
            maybeUser.UpdatedDate = this.dateTimeBroker.GetCurrentDateTime();
            await this.userService.ModifyUserAsync(maybeUser);

            return maybeUser.Id;
        }
    }
}