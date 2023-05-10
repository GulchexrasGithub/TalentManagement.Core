// --------------------------------------------------------------- 
// Copyright (c)  the Gulchexra Burxonova
// Talent Management 
// ---------------------------------------------------------------



using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PostmarkDotNet;
using TalentManagement.Core.Models.Foundations.Emails;

namespace TalentManagement.Core.Brokers.Emails
{
    public class EmailBroker : IEmailBroker
    {
        private readonly PostmarkClient postmarkClient;

        public EmailBroker(IConfiguration configuration)
        {
            this.postmarkClient = new PostmarkClient(
                serverToken: configuration.GetValue<string>("EmailServerToken"));
        }

        public async Task<PostmarkResponse> SendEmailAsync(Email email)
        {
            var message = new PostmarkMessage()
            {
                To = email.ReceiverAddress,
                From = email.SenderAddress,
                TrackOpens = email.TrackOpens,
                Subject = email.Subject,
                HtmlBody = email.HtmlBody,
                TextBody = email.TextBody,
                Cc = email.Cc
            };

            return await this.postmarkClient.SendMessageAsync(message);
        }
    }
}