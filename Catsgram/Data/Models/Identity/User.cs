using Catsgram.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Catsgram.Models
{
    public class User : IdentityUser
    {
        public IEnumerable<Cat> Cats { get; set; } = new HashSet<Cat>();
    }
}
