using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Contoso.Domain;

namespace Contoso.Data
{
    public class ContosoContext1 :DbContext
    {
      public  DbSet<Student> Students { get; set; }
    }
}
