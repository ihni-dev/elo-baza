using System;
using System.Collections.Generic;
using System.Linq;

namespace EloBaza.Domain.SharedKernel
{
    public class ValidationContext : IDisposable
    {
        private readonly Dictionary<string, List<string>> _errors;

        public ValidationContext()
        {
            _errors = new Dictionary<string, List<string>>();
        }

        public void Dispose()
        {
            if (_errors.Any())
            {
                throw new ValidationException(_errors);
            }
        }

        public void Validate(Func<bool> isInvalid, string validationSubject, string validationMessage)
        {
            if (isInvalid())
            {
                AddError(validationSubject, validationMessage);
            }
        }

        private void AddError(string validationSubject, string validationMessage)
        {
            if (_errors.ContainsKey(validationSubject))
            {
                _errors[validationSubject].Add(validationMessage);
            }
            else
            {
                _errors.Add(validationSubject, new List<string>() { validationMessage });
            }
        }
    }
}
