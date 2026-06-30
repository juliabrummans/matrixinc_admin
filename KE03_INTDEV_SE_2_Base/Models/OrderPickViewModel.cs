using System.Collections.Generic;

namespace KE03_INTDEV_SE_2_Base.Models
{
    public class OrderPickViewModel
    {
        public int OrderId { get; set; }
        public string? DropLocation { get; set; }
        public string? Status { get; set; }
        public string? StatusColor { get; set; } // Ontbrekende eigenschap toegevoegd
        public List<PickItemViewModel> Items { get; set; } = new List<PickItemViewModel>(); // Standaard lege lijst
    }

    public class PickItemViewModel
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public string? ProductLocation { get; set; }
        public bool IsPicked { get; set; }
    }
}