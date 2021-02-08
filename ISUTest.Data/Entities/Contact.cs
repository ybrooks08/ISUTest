using System;
using System.Collections.Generic;

#nullable disable

namespace ISUTest.Data.Entities
{
    public partial class Contact
    {
        public Contact()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birth { get; set; }
        public int ContactTypeId { get; set; }

        public virtual ContactType ContactType { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
