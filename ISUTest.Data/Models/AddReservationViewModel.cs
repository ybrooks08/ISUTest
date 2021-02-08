using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ISUTest.Data.Models
{
    public class AddReservationViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ContactId { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
