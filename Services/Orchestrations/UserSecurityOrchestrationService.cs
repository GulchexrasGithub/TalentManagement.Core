// --------------------------------------------------------------- 
// Copyright (c)  the Gulchexra Burxonova
// Talent Management 
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using TalentManagement.Core.Brokers.Loggings;
using TalentManagement.Core.Models.Foundations.Emails;
using TalentManagement.Core.Models.Orchestrations.UserTokens;
using TalentManagement.Core.Services.Foundations.Emails;
using TalentManagement.Core.Services.Foundations.Securities;
using TalentManagement.Core.Services.Foundations.Users;

namespace TalentManagement.Core.Services.Orchestrations
{
    public partial class UserSecurityOrchestrationService : IUserSecurityOrchestrationService
    {
        private readonly IUserService userService;
        private readonly ISecurityService securityService;
        private readonly IEmailService emailService;
        private readonly ILoggingBroker loggingBroker;

        public UserSecurityOrchestrationService(
            IUserService userService,
            ISecurityService securityService,
            IEmailService emailService,
            ILoggingBroker loggingBroker)
        {
            this.userService = userService;
            this.securityService = securityService;
            this.emailService = emailService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<User> CreateUserAccountAsync(User user, string requestUrl) =>
        TryCatch(async () =>
        {
            User persistedUser = await this.userService.AddUserAsync(user);
            Email email = CreateUserEmail(persistedUser, requestUrl);

            //Diyorjon tomonidan comentga olindi, sababi serverda email yuvorilmayapti
            //await this.emailService.SendEmailAsync(email);

            return persistedUser;
        });

        public UserToken CreateUserToken(string email, string password) =>
        TryCatch(() =>
        {
            ValidateEmailAndPassword(email, password);
            User maybeUser = RetrieveUserByEmailAndPassword(email, password);
            ValidateUserExists(maybeUser);
            string token = this.securityService.CreateToken(maybeUser);

            return new UserToken
            {
                UserId = maybeUser.Id,
                Token = token
            };
        });

        private User RetrieveUserByEmailAndPassword(string email, string password)
        {
            IQueryable<User> allUser = this.userService.RetrieveAllUsers();

            return allUser.FirstOrDefault(retrievedUser => retrievedUser.Email.Equals(email)
                    && retrievedUser.Password.Equals(password));
        }

        private Email CreateUserEmail(User user, string requestUrl)
        {
            string endpoint = "api/Users/VerifyUserById";
            string verificationAddress = $"{requestUrl}/{endpoint}/{user.Id}";
            string subject = "Confirm your email";
            string htmlBody = @$"
<!DOCTYPE html>
<html>
  <body>
    <h1>Hey {user.FirstName}</h1>
    <p>Thank you for registering for our schooling system. Please confirm your email address by clicking the button below.</p>
    <a href=""{verificationAddress}"">
      <button>Confirm Email</button>
    </a>
  </body>
</html>
";

            return new Email
            {
                Id = Guid.NewGuid(),
                Subject = subject,
                HtmlBody = htmlBody,
                SenderAddress = "no-reply@tarteeb.uz",
                ReceiverAddress = user.Email,
                TrackOpens = true
            };
        }
    }
}