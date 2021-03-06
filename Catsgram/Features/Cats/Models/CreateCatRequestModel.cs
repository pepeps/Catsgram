﻿using Catsgram.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catsgram.Features.Cats.Models
{
    using static Validation.Cat;
    public class CreateCatRequestModel
    {
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
