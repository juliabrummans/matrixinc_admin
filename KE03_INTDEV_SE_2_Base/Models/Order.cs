namespace KE03_INTDEV_SE_2_Base.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string ProductNaam { get; set; }
        public int Aantal { get; set; }
        public DateTime Datum { get; set; }
    }
}
