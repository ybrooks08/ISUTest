using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ISUTest.Data.Models
{
    public class AddContactViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime Birth { get; set; }
        [Required]
        public int ContactTypeId { get; set; }
    }
}
