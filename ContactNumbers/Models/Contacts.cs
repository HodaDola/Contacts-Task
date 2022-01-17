using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactNumbers.Models
{
    public class Contacts
    {
        public int Id { get; set; }

        [Required,StringLength(60)]
        public string Name { get; set; }
        //[Required, MaxLength(14),MinLength(9), RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
        //           ErrorMessage = "Entered phone format is not valid.")]
        [Required, MaxLength(14), MinLength(9), RegularExpression(@"^\(?([0][1][0-5]{1}[0-9]{8})$",
                    ErrorMessage = "Entered phone format is not valid.")]
        public string Phone { get; set; }

        [MaxLength(150)]
        public string Address { get; set; }
        [MaxLength(200)]
        public string Notes { get; set; }
    }
}
