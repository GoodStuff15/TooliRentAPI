using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    // Cannot be changed without restarting system
    // These are business rules that are unlikely to change often
    internal static class Business_Parameters
    {
        internal const decimal LateFee = 10.00m;

        internal const int MaxExtensions = 1;

        internal const int MaxExtensionDays = 3;

        internal const int MaxLoanDays = 7;
    }
}
