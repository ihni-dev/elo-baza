using System;
using System.Collections.Generic;
using System.Linq;

namespace EloBaza.Domain.SharedKernel.Exceptions
{
    public class ValidationException : Exception
    {
        private const string ValidationFailedMessage = "One or more validation errors occured";

        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(IDictionary<string, List<string>> errors) : base(ValidationFailedMessage)
        {
            Errors = errors.ToDictionary(e => e.Key, e => e.Value.ToArray());
        }

        protected ValidationException()
        {
            Errors = new Dictionary<string, string[]>();
        }

        protected ValidationException(string message) : base(message)
        {
            Errors = new Dictionary<string, string[]>();
        }

        protected ValidationException(string message, Exception innerException) : base(message, innerException)
        {
            Errors = new Dictionary<string, string[]>();
        }
    }
}
