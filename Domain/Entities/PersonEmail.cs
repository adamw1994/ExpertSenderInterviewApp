using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PersonEmail: BaseEntity
    {
        public string Email { get; set; }
        public Person Person { get; set; }
        public int PersonId { get; set; }
    }
}
