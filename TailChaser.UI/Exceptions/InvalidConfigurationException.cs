using System;

namespace TailChaser.UI.Exceptions
{
    public class InvalidConfigurationException : Exception
    {
        public InvalidConfigurationException(Exception ex) : base("The configuration is invalid", ex)
        {
            
        }
    }
}
