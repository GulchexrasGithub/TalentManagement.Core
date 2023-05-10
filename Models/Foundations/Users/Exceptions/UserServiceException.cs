//=================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to use to bring order in your workplace
//=================================

using System;
using Xeptions;

namespace TalentManagement.Core.Models.Foundations.Users.Exceptions
{
    public class UserServiceException : Xeption
    {
        public UserServiceException(Exception innerException)
            : base(message: "User service error occurred, contact support.", innerException)
        { }
    }
}