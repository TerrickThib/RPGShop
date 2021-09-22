using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    class Player
    {
        private int _gold;
        private Item[] _inventory;

        public Player()
        {
            _gold = 100;
            //Creates a new item array with three items with default values
            _inventory = new Item[3];
        }

        public void Buy(Item item, int inventoryIndex)
        {
            //Check to see if the player can afford the item
            if(_gold >= item.Cost)
            {
                //Pay for item.
                _gold -= item.Cost;
                //Place item in inventory array.
                _inventory[inventoryIndex] = item;
                Console.WriteLine("You Purchased A Item");
                
            }   
            else if(_gold < item.Cost)
            {
                Console.WriteLine("You Broke");
                return;
            }
        }

        public int Gold()
        {            
           return _gold;                           
        }

        public Item[] GetInventory()
        {
            return _inventory;
        }
        public string[] GetItemNames()
        {
            string[] itemNames = new string[_inventory.Length];

            for (int i = 0; i < _inventory.Length; i++)
            {
                itemNames[i] = _inventory[i].Name;
            }
            return itemNames;
        }
        
    }
}
