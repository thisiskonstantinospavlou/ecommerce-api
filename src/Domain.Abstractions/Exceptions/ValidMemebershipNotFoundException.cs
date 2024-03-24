using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions.Exceptions
{
    public class ValidMembershipNotFoundException : Exception
    {
        public ValidMembershipNotFoundException()
            : base()
        {
        }

        public ValidMembershipNotFoundException(string message)
            : base(message)
        {
        }

        public ValidMembershipNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
