// --------------------------------------------------------------- 
// Copyright (c)  the Gulchexra Burxonova
// Talent Management 
// ---------------------------------------------------------------

using System;

namespace TalentManagement.Core.Brokers.Loggings
{
    public interface ILoggingBroker
    {
        void LogError(Exception exception);
        void LogCritical(Exception exception);
    }
}