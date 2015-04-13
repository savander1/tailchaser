using System;

namespace TailChaser.Exceptions
{
    public class InvalidPropertyNameException : Exception
    {
        public InvalidPropertyNameException(string propertyName) : base("Invalid property name: " + propertyName)
        {
            
        }
    }
}
