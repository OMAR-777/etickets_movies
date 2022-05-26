using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data.Base;

namespace eTickets.Models
{
    public class Cinema : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Cinema logo")]
        [Required]
        public string Logo { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 chars")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        //Relationships
        public List<Movie> Movies { get; set; }
    }
}