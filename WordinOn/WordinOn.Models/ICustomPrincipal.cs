using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace WordinOn.Models
{
    public interface ICustomPrincipal : IPrincipal
    {
        int Cod { get; set; }
        string Nome { get; set; }
        string Email { get; set; }
    }
}
