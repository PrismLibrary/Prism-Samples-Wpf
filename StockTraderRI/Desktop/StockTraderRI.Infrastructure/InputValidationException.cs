

using System;

namespace StockTraderRI.Infrastructure
{
    [Serializable]
    public class InputValidationException : Exception
    {
        public InputValidationException()
        {
        }

        public InputValidationException(string message) : base(message)
        {
        }

        public InputValidationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InputValidationException(
                System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
