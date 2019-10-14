using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contoso.Domain;

namespace Contoso.Data
{
    public class ContosoContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
    }
}
