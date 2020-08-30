using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.DataAccess.Model
{
    public class Venue
    {




        int Wight { get; set; } // in meters
        int Lenght { get; set; }

        int SizeBySeats { get { return Wight * Lenght; } } // one seat by meter^2
 
    }
}
