﻿//=================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to use to bring order in your workplace
//=================================

using Xeptions;

namespace TalentManagement.Core.Models.Foundations.Users.Exceptions
{
    public class NullUserException : Xeption
    {
        public NullUserException()
            : base(message: "User is null.")
        { }
    }
}