using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister
{
    class ItemsProperties
    {
        public string itemID { get; set; }
        public string itemName { get; set; }
        public int itemQuantity { get; set; }
        public float itemWeight { get; set; }
        public float itemPrice { get; set; }
    }

    class Items : ItemsProperties
    {
        List<ItemsProperties> allItems = new List<ItemsProperties>();
    }
}
