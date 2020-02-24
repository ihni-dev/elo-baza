using System;

namespace EloBaza.Domain.SharedKernel
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string message) : base(message)
        {
        }
    }
}
