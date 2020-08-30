namespace TicketManagement.DataAccess.Model
{
    public class Venue
    {
        // in meters
        public int Wight { get; set; }

        public int Lenght { get; set; }

        public int SizeBySeats
        {
            get { return Wight * Lenght; }
        } // one seat by meter^2
    }
}
