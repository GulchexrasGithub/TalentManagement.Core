// --------------------------------------------------------------- 
// Copyright (c)  the Gulchexra Burxonova
// Talent Management 
// ---------------------------------------------------------------



using System.Threading.Tasks;
using PostmarkDotNet;
using TalentManagement.Core.Models.Foundations.Emails;

namespace TalentManagement.Core.Brokers.Emails
{
    public interface IEmailBroker
    {
        Task<PostmarkResponse> SendEmailAsync(Email email);
    }
}
