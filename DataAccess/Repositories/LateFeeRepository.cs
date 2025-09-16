using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class LateFeeRepository : GenericRepository<LateFee>
    {
        public LateFeeRepository(ToolContext context) : base(context)
        {
        }
    }
}
