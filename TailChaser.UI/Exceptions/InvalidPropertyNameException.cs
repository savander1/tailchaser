using System;

namespace TailChaser.UI.Exceptions
{
    public class InvalidPropertyNameException : Exception
    {
        public InvalidPropertyNameException(string propertyName) : base("Invalid property name: " + propertyName)
        {
            
        }
    }
}
