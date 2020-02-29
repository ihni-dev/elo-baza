using System;
using System.Collections.Generic;
using System.Linq;

namespace EloBaza.Domain.SharedKernel
{
    public class ValidationException : Exception
    {
        private const string ValidationFailedMessage = "One or more validation errors occured";
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(IDictionary<string, List<string>> errors) : base(ValidationFailedMessage)
        {
            Errors = errors.ToDictionary(e => e.Key, e => e.Value.ToArray());
        }

        public ValidationException(IDictionary<string, string[]> errors) : base(ValidationFailedMessage)
        {
            Errors = errors;
        }
    }
}
