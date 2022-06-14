using System;
using System.Runtime.Serialization;

namespace Estetika.Models.Exceptions
{
    public class ChoiceActionNotFoundException : Exception
    {
        public ChoiceActionNotFoundException()
        {
        }

        public ChoiceActionNotFoundException(string message) : base(message)
        {
        }

        public ChoiceActionNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ChoiceActionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}