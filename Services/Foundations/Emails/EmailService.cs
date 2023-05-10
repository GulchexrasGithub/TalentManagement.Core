// --------------------------------------------------------------- 
// Copyright (c)  the Gulchexra Burxonova
// Talent Management 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using PostmarkDotNet;
using TalentManagement.Core.Brokers.Emails;
using TalentManagement.Core.Brokers.Loggings;
using TalentManagement.Core.Models.Foundations.Emails;

namespace TalentManagement.Core.Services.Foundations.Emails
{
    public partial class EmailService : IEmailService
    {
        private readonly IEmailBroker emailBroker;
        private readonly ILoggingBroker loggingBroker;

        public EmailService(
            IEmailBroker emailBroker,
            ILoggingBroker loggingBroker)
        {
            this.emailBroker = emailBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Email> SendEmailAsync(Email email) =>
        TryCatch(async () =>
        {
            ValidateEmailOnSend(email);
            PostmarkResponse postmarkResponse = await this.emailBroker.SendEmailAsync(email);

            return postmarkResponse.Status is PostmarkStatus.Success
                ? email
                : ConvertToMeaningfulError(postmarkResponse);
        });
    }
}