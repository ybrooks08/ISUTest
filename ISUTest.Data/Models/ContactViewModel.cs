using System;
using System.Collections.Generic;
using System.Text;

namespace ISUTest.Data.Models
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int ContactTypeId { get; set; }
        public string ContactType { get; set; }
    }
}
