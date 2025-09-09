using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(ToolContext context) : base(context)
        {
        }
    }
}
