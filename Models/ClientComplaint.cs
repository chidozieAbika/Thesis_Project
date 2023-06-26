using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Motte_IT.Models
{
    public class ClientComplaint
    {
            [Key]
            public int ComplaintID { get; set; }
            public int ComputerNumber { get; set; }
            public String StartDate { get; set; }
            public String CategorySubject { get; set; }
            public String Description { get; set; }
            public int ClientID { get; set; }
            public String Status { get; set; }
            public int AdminID { get; set; }
            public String FinishedDate { get; set; }
            public int KeyValue { get; set; }

            
    }
}
