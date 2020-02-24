using System;

namespace EloBaza.Domain.SharedKernel
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
