﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    struct Item
    {
        public string name;
        public int cost;        
    }    
    class Game
    {
        private Player _player;
        private Shop _shop;
        private Item _arrow;
        private Item _shield;
        private Item _gem;
        private Item[] _shopInventory;
        private bool _gameOver;
        private int _currentScene;

        //Run the game
        public void Run()
        {
            Start();

            while(_gameOver == false)
            {
                Update();
            }

            End();
        }

        private void InitializeItems()
        {
            _arrow.name = "Arrow ";
            _arrow.cost = 10;
            _shield.name = "Shield ";
            _shield.cost = 15;
            _gem.name = "Ghem ";
            _gem.cost = 50;
        }

        public void PrintInventory(Item[] inventory)
        {
            for(int i = 0; i < inventory.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + inventory[i].name + inventory[i].cost);
            }
        }

        private void DisplayShopMenu()
        {
            int choice = GetInput("Welcome to Choose Goose Shop where all we got is a sock. ", "Buy Items", "Check Player Gold");
            if (choice == 0)
            {
                _currentScene = _currentScene++;
            }
            else if (choice == 1)
            {
                
            }
                                        
            
        }

        //Performed once when the game begins
        public void Start()
        {
            _gameOver = false;
            _player = new Player();
            InitializeItems();
            _shopInventory = new Item[] { _arrow, _shield, _gem };
            _shop = new Shop(_shopInventory);
            _currentScene = 0;
        }
        
        //Repeated until the game ends
        public void Update()
        {
            DisplayCurrentScene();
        }

        //Performed once when the game ends
        public void End()
        {
            
        }
         //Get input Function 
        private int GetInput(string description, params string[] options)
        {
            string input = "";
            int inputReceived = -1;

            while (inputReceived == -1)
            {
                //Print all of the options
                Console.WriteLine(description);
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine((i + 1) + ". " + options[i]);
                }
                Console.Write("> ");

                //Gets players input
                input = Console.ReadLine();

                //If the player typed an int...
                if (int.TryParse(input, out inputReceived))
                {
                    //...decrement the input and check if its within the bounds of the array if they choose 1 they atwally choose 0 in the array
                    inputReceived--;
                    if (inputReceived < 0 || inputReceived >= options.Length)
                    {
                        //sets input recived to be the default value
                        inputReceived = -1;
                        //Displays a error message
                        Console.WriteLine("Invalid Input");
                        Console.ReadKey(true);
                    }
                }
                //If the player didnt type an int
                else
                {
                    //Set input recived to be the default value
                    inputReceived = -1;
                    Console.WriteLine("Invalid Input");
                    Console.ReadKey(true);
                }

                Console.Clear();
            }
            return inputReceived;
        }
        private void DisplayCurrentScene()
        {
            switch(_currentScene++)
            {
                case 1:
                    DisplayOpeningMenu();
                    break;
                case 2:
                    DisplayShopMenu();
                    break;
                case 3:
                    GetShopMenuOptions();
                    break;
            }

        }
        //Alows player to buy item
        private void GetShopMenuOptions()
        {                       
            //Pulls the item list from the shop class and displays it.
            int input = GetInput("Items To Buy.", _shop.GetItemNames());

            if(input == 0)
            {
                _player.Buy(_arrow, input);
                                
            }
            else if(input == 1)
            {
                _player.Buy(_shield, input);
                
            }
            else if(input == 2)
            {
                _player.Buy(_gem, input);
                
            }
                            
            //Set itemIndex to be the index the player selected
            int itemIndex = -1;
            switch (input)
            {
                case '1':
                    {
                        itemIndex = 0;
                        break;
                    }
                case '2':
                    {
                        itemIndex = 1;
                        break;
                    }
                case '3':
                    {
                        itemIndex = 2;
                        break;
                    }
                default:
                    {
                        return  ;
                    }
            }
                        
            //Ask the player to replace a slot in their own inventory
            Console.WriteLine("Choose a slot to replace.");
            PrintInventory(_player.GetInventory());
            //Get player input
            input = Console.ReadKey().KeyChar;

            //Set the value of the playerIndex based on the player's choice
            int playerIndex = -1;
            switch (input)
            {
                case '1':
                    {
                        playerIndex = 0;
                        break;
                    }
                case '2':
                    {
                        playerIndex = 1;
                        break;
                    }
                case '3':
                    {
                        playerIndex = 2;
                        break;
                    }
                default:
                    {
                        return;
                    }
            }
            //sell item to player and replace the weapon at the index with the newly purchased weapon
            _shop.Sell(_player, itemIndex, playerIndex);
        }
        public void DisplayOpeningMenu()
        {
            int choice = GetInput("Welcome To Test Shop ", "Start New Game", "Load Game");
            if (choice == 0)
            {
                _currentScene = _currentScene++;
            }
            else if (choice == 1)
            {
                //Load input
            }
        }
    }
}
