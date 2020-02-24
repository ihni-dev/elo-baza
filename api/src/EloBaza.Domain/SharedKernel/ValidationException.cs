using System;
using System.Collections.Generic;

namespace EloBaza.Domain.SharedKernel
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(string message, IDictionary<string, string[]> errors) : base(message)
        {
            Errors = errors;
        }
    }
}
