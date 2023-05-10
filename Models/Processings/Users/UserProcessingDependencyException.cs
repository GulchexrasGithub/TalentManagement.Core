// --------------------------------------------------------------- 
// Copyright (c)  the Gulchexra Burxonova
// Talent Management 
// ---------------------------------------------------------------

using Xeptions;

namespace TalentManagement.Core.Models.Processings.Users
{
    public class UserProcessingDependencyException : Xeption
    {
        public UserProcessingDependencyException(Xeption innerException)
            : base(message: "User dependency error occurred, contact support.", innerException)
        { }
    }
}