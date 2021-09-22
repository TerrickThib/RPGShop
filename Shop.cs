using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    class Shop
    {
        private int _gold;
        private Item[] _inventory;
        public string name;
        public int cost;

        public Shop()
        {
            _gold = 100;
            _inventory = new Item[3];
        }
        
        public Shop(Item[] items)
        {
            _gold = 100;
            //Set our inventory array to be the array of items that was passed in.
            _inventory = items;
        }

        public bool Sell(Player player,int itemIndex, int playerIndex)
        {
            //Find the item to buy in the inventory array
            Item itemToBuy = _inventory[itemIndex];
            //Check to see if the player has anofe gold
            if (player.Gold() >= itemToBuy.cost)
            {
                //Increase shops gold by item cost to complete the transaction
                _gold += itemToBuy.cost;
                return true;
            }
            return true;
        }
        public string[] GetItemNames()
        {
            string[] itemNames = new string[_inventory.Length];

            for (int i = 0; i < _inventory.Length; i++)
            {
                itemNames[i] = _inventory[i].name;
            }

            return itemNames;
        }
    }

}
