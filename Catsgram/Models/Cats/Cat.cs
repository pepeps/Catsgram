using Catsgram.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catsgram.Models
{
    using static Validation.Cat;

    public class Cat
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
