//=================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to use to bring order in your workplace
//=================================

using Xeptions;

namespace TalentManagement.Core.Models.Foundations.Emails.Exceptions
{
    public class NullEmailException : Xeption
    {
        public NullEmailException()
            : base(message: "Email is null.")
        { }
    }
}