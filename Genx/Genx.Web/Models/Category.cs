using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Genx.Web.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Please enter category name.")]
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
