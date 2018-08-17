using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister
{   

    class Program
    {
        // Function to populate the list with data
        public static List<Items> populateTheList()
        {
            Items item1 = new Items()
            {
                itemID= "1",
                itemName = "Hersheys",
                itemQuantity = 1,
                itemWeight = 20,
                itemPrice = 5
            };
            Items item2 = new Items()
            {
                itemID = "2",
                itemName = "M&M",
                itemQuantity = 1,
                itemWeight = 20,
                itemPrice = 2
            };
            Items item3 = new Items()
            {
                itemID = "3",
                itemName = "Snickers",
                itemQuantity = 1,
                itemWeight = 20,
                itemPrice = 2
            };
            Items item4 = new Items()
            {
                itemID = "4",
                itemName = "Reese",
                itemQuantity = 1,
                itemWeight = 20,
                itemPrice = 3
            };
            Items item5 = new Items()
            {
                itemID = "5",
                itemName = "Godiva",
                itemQuantity = 1,
                itemWeight = 20,
                itemPrice = 10
            };

            List<Items> items = new List<Items>(5);
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            items.Add(item4);
            items.Add(item5);

            return items;

        }

        // function to display the list
        public static void displayList(List<Items> items)
        {
            Console.WriteLine("ID    ||    Name    ||    Price");
            try
            {
                foreach (Items i in items)
                {
                    Console.WriteLine("{0}    ||    {1}    ||    {2}", i.itemID, i.itemName, i.itemPrice);
                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("An Exception has occured, Please try again later!");
            }
            
        }

        // Scan by quantity function modifying price, weight and quantity
        public static Items scanByQuantity(Items i)
        {
            Console.WriteLine("How many {0}s do you want?", i.itemName);
            string qty = Console.ReadLine();
            int actualqty = int.Parse(qty);
            //Console.WriteLine(i.itemQuantity);
            if (actualqty > 0)
            {
                i.itemQuantity = actualqty;
                //Console.WriteLine(actualqty);
                //Console.WriteLine(i.itemQuantity);
                i.itemPrice = (int)i.itemPrice * actualqty;
                i.itemWeight = i.itemWeight * actualqty;
                Console.WriteLine("Thank you!");
            }
            else
            {
                Console.WriteLine("Please enter valid quantity");
            }
            return i;
        }

        // Scan by weight function modifying price, weight and quantity
        public static Items scanByWeight(Items i)
        {

            Console.WriteLine("What is the weight of your {0}?", i.itemName);
            string wt = Console.ReadLine();
            int actualwt = int.Parse(wt);
            if (actualwt > i.itemWeight)
            {
                float wtcal = actualwt / i.itemWeight;
                i.itemQuantity = i.itemQuantity * (int)wtcal;
                i.itemPrice = (int)i.itemPrice * (int)wtcal;
            }
            else {
                Console.WriteLine("Please enter valid weight");
            }
            
            Console.WriteLine("Thank you!");
            return i;
        }

        // Continue to shop
        public static string contShopping()
        {
            //Console.WriteLine("Do you wish to continue? Y/N");
            string cont = Console.ReadLine();
            return cont;
        }

        // Function to make decisions regarding adding the items in cart
        public static Items addItemsToCart(List<Items> items)
        {
            //List<Items> itemsInCart = new List<Items>(5);
            Items tempItem = null;

            int itemSelect = 0;
            string ctshop = "Y";

            while(ctshop == "Y")
            {
                Console.WriteLine("Please print the item ID from the list provided");
                string itemSelected = Console.ReadLine();
                

                int.TryParse(itemSelected, out itemSelect);

                if (itemSelect == 0 || itemSelect > 5)
                {
                    Console.WriteLine("Please enter a valid item ID");
                }
                else
                {
                    foreach (Items i in items)
                    {
                                                
                        if (itemSelected == i.itemID)
                        {
                            Console.WriteLine("Do you want to scan by quantity or by weight? Q/W");
                            string measure = Console.ReadLine();
                            
                            if (measure.ToUpper() == "Q")
                            {                                
                                tempItem = scanByQuantity(i);
                                return tempItem;
                                //itemsInCart.Add(tempItem);
                                                             
                                //break;
                            }
                            else if (measure.ToUpper() == "W")
                            {
                                tempItem = scanByWeight(i);
                                return tempItem;
                                //itemsInCart.Add(tempItem);
                                //break;
                            }
                            else
                            {
                                Console.WriteLine("Please select valid option! Press enter to continue.");                                
                            }
                        
                        }

                    }

                }
                ctshop = contShopping();
                
            }
            return tempItem;
        }

        // To calculate discount and modify the price
        public static float discount(List<Items> newItems, int index)
        {
            float sum = 0;
            foreach (Items i in newItems)
            {
                sum += i.itemPrice;
            }
            switch (index)
            {
                case 0:
                    sum = (float)(sum - (0.1 * sum));

                    break;
                case 1:
                    sum = (float)(sum - (0.2 * sum));
                    break;
                case 2:
                    sum = (float)(sum - (0.3 * sum));
                    break;
                case 3:
                    float tempPrice;
                    foreach (Items i in newItems)
                    {
                        if(i.itemQuantity > 3 && i.itemQuantity % 3 != 0)
                        {
                            tempPrice = i.itemPrice / i.itemQuantity;
                            sum -= tempPrice;
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Discount Code cannot be implemented. Please try later.");
                    break;
                    
            };
            return sum;
        }


        static void Main(string[] args)
        {
            string continueShopping = "Y";
            Console.WriteLine("Welcome to Piyush's Cart!!");
            Console.WriteLine("___________________________");

            // call function populateTheList and store the return value in a variable.
            List<Items> items = populateTheList();

            // Display the list and price
            displayList(items);


            // scanning...
            List<Items> newItems = new List<Items>();

            while (continueShopping.ToUpper() == "Y") {
                // Items abc = item77;
                // newItems.Add(abc);
                try { newItems.Add(addItemsToCart(items)); }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Exception occured!! Please retry");
                }
                //newItems.Add(addItemsToCart(items));
                Console.WriteLine("Do you wish to continue? Y/N");
                continueShopping = contShopping();
            }

            // Intermediate steps
            float finalCost = 0;
            displayList(newItems);
            foreach (Items i in newItems)
            {
                try
                {
                    //Console.WriteLine(i.itemName);
                    finalCost += i.itemPrice;
                }
                catch (NullReferenceException) { Console.WriteLine("An Exception has occured, Please try again later!"); }

            }
            Console.WriteLine("Your total is {0}", finalCost);
            //DisplayList(newItems);

            // Discount coupon asking
            Console.WriteLine("Do you have a discount coupon? If yes, print it here or else press enter");
            string discountCouponExists = Console.ReadLine();
            
            
            // Declaring the coupons
            List<string> Coupons = new List<string>(4) {
            "10PERCENTOFF", "20PERCENTOFF", "30PERCENTOFF", "BUY3GET4"
            };

            // Discount calculation
            if (Coupons.Contains(discountCouponExists)) {
                int index = Coupons.IndexOf(discountCouponExists);
                finalCost = discount(newItems, index);
            }
            else
            {
                Console.WriteLine("Discount Coupon invalid! Please try again");
            }

            // call for total cost and list of items
            Console.WriteLine("Your checkout items list");
            displayList(newItems);
            Console.WriteLine("Your total is {0}", finalCost);

            Console.WriteLine("Press enter to pay and exit the code");
            Console.ReadLine();

        }
    }
}
