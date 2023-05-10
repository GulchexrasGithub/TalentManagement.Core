// --------------------------------------------------------------- 
// Copyright (c)  the Gulchexra Burxonova
// Talent Management 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using TalentManagement.Core.Models.Foundations.Emails;

namespace TalentManagement.Core.Services.Foundations.Emails
{
    public interface IEmailService
    {
        ValueTask<Email> SendEmailAsync(Email email);
    }
}