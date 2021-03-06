using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HelloWorld
{
    struct Item
    {
        public string Name;
        public int Cost;
    }
    class Game
    {
        private Player _player;
        private Shop _shop;
        private Item arrow;
        private Item shield;
        private Item gem;
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
             arrow = new Item { Name = "Arrow", Cost = 10 };
             shield = new Item { Name = "Shield", Cost = 25 };
             gem = new Item { Name = "Gem", Cost = 50 };
            
        }

        public void PrintInventory(Item[] inventory)
        {
            for(int i = 0; i < inventory.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + inventory[i].Name + inventory[i].Cost);
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
                Console.WriteLine("Player Gold: " + _player.Gold());
                Console.ReadKey();
                Console.Clear();
            }
                                        
            
        }

        //Performed once when the game begins
        public void Start()
        {
            _gameOver = false;
            _player = new Player();
            InitializeItems();
            _shopInventory = new Item[] { arrow, shield, gem };
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
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("No More Shop For UUUU");
            Console.ReadKey();
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
            Console.WriteLine("Player Gold: "+ _player.Gold());
            //Pulls the item list from the shop class and displays it.            
            int input = GetInput("Items To Buy.", _shop.GetItemNames());

            if(input == 0)
            {
                _player.Buy(arrow, input);                
            }
            else if(input == 1)
            {
                _player.Buy(shield, input);                
            }
            else if(input == 2)
            {
                _player.Buy(gem, input);                
            }

            //Gets players Inventory and displays it.
            Item[] playerInventory =_player.GetInventory();
            //increments each item in players inventory
            for (int i = 0; i < playerInventory.Length; i++)
            {
                //Console.WriteLine($"{i + 1}: {playerInventory[i].Name}");
                Console.WriteLine((i + 1) + ". " + playerInventory[i].Name);                
            }
            _gameOver = true;
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
        private void Save()
        {
            //Create a new stream writer
            StreamWriter writer = new StreamWriter("SaveData.txt");

            //Saves players stats
            _player.Save(writer);

            //Saves shops inventory
            writer.WriteLine(_shopInventory.Length);

            for (int i = 0; i < _shopInventory.Length; i++)
            {
                writer.WriteLine(_shopInventory[i].Name);
                writer.WriteLine(_shopInventory[i].Cost);
            }

            

            //Closes the writer when done saving
            writer.Close();
        }
        private bool Load()
        {
            bool loadSuccessful = true;

            //If File doesnot exist
            if (!File.Exists("SaveData.txt"))
                //Return false
                loadSuccessful = false;

            //Creates a new reader to read from the text file
            StreamReader reader = new StreamReader("SaveData.txt");

            //Load shop INventory
            string ShopInventoryLength = Console.ReadLine();

            _shopInventory = new Item[int.Parse(ShopInventoryLength)];

            for (int i = 0; i < _shopInventory.Length; i++)
            {
                _shopInventory[i].Name = reader.ReadLine();
                _shopInventory[i].Cost = int.Parse(reader.ReadLine());
            }
            loadSuccessful = false;
            
        }
    }
}
