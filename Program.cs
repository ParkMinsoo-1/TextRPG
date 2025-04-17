using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace TextRPG
{
    internal class Program
    {
        
        static void Main(string[] args)
        {


            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("플레이할 캐릭터의 이름을 적어 주세요.\n");
            
            string playerName = Console.ReadLine();
            string job = null;
            
            while (true)
            {
                
                Console.WriteLine("");
                Console.WriteLine("당신의 직업은 무엇입니까?");
                Console.WriteLine("1. 전사 \n2. 도적 \n3. 궁수 \n4. 마법사");
                bool isNum = int.TryParse(Console.ReadLine(), out int num);

                if (!isNum)
                {
                    Console.WriteLine("숫자를 입력해 주세요.");
                    continue;
                }
                if (num == 1)
                {
                    job = "전사";
                    break;
                }
                else if (num == 2)
                {
                    job = "도적";
                    break;
                }
                else if (num == 3)
                {
                    job = "궁수";
                    break;
                }
                else if (num == 4)
                {
                    job = "마법사";
                    break;
                }
                else
                {
                    Console.WriteLine("다시 입력해 주세요.");
                }
            }
            Player player = new Player(playerName, job);
            
            Console.WriteLine(" ");
            Console.WriteLine($"환영합니다. 전설의 {job} {playerName}님.");

            SelectMenu(player);
        }

        static void SelectMenu(Player player)
        {
            
            while (true)
            {
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다. \n \n1. 상태 보기 \n2. 인벤토리 \n3. 상점 \n");
                Console.Write("원하시는 행동을 입력해주세요. \n>>");
                bool isNum = int.TryParse(Console.ReadLine(), out int num);

                if (!isNum)
                {
                    Console.WriteLine("숫자를 입력해 주세요.");
                    continue;
                }
                if (num == 1)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("상태 보기");
                    Console.WriteLine("캐릭터의 정보가 표시됩니다.");

                    player.PlayerStatus();

                    while (true)
                    {
                        Console.WriteLine(" ");
                        Console.WriteLine("0. 나가기");
                        Console.Write("원하시는 행동을 입력해 주세요. \n>>");

                        bool exit = int.TryParse(Console.ReadLine(), out int exitNum);

                        if (!exit)
                        {
                            Console.WriteLine("숫자를 입력해 주세요.");
                            continue;
                        }
                        if (exitNum == 0)
                        {
                            SelectMenu(player);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다. 다시 선택해 주세요.");
                        }
                     }
                }
                else if (num == 2)
                {
                    player.Inventory.ShowInventory();

                    while (true)
                    {
                        Console.WriteLine(" ");
                        Console.WriteLine("1. 장착 관리 \n2. 나가기");
                        Console.Write("원하시는 행동을 입력해 주세요. \n>>");

                        bool Inventory = int.TryParse(Console.ReadLine(), out int inventoryNum);

                        if (!Inventory)
                        {
                            Console.WriteLine("숫자를 입력해 주세요.");
                            continue;
                        }
                        if (inventoryNum == 1)
                        {
                            player.Inventory.EquipItem();
                        }
                        else if(inventoryNum == 2)
                        {
                            SelectMenu(player);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다. 다시 선택해 주세요.");
                        }
                    }
                }
                else if (num == 3)
                {
                    Store();
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 선택해 주세요.");

                }

            }
            
        }

        class Player
        {

            public Player(string name, string job)
            {
                PlayerName = name;
                Job = job;
                Inventory = new PlayerInventory();
            }
            
            public PlayerInventory Inventory { get; set; }

            public int Level = 1;
            public string PlayerName ;
            public string Job;
            public int BaseAttackpower = 10;
            public int BaseDefense = 5;
            public int BaseHealth = 50;
            public int Gold = 1500;


            public void PlayerStatus()
            {
                int totalAttackpower = BaseAttackpower + Inventory.TotalAttackPower;
                int totalDefense = BaseDefense + Inventory.TotalDefense;
                int totalHealth = BaseHealth + Inventory.TotalHealth;

                Console.WriteLine($"{"Lv. ",-7}: {Level}");
                Console.WriteLine($"{"이름",-5}: {PlayerName}");
                Console.WriteLine($"{"직업",-5}: {Job}");
                Console.WriteLine($"{"공격력",-4}: {totalAttackpower} + {Inventory.TotalAttackPower}");
                Console.WriteLine($"{"방어력",-4}: {totalDefense} + {Inventory.TotalDefense}");
                Console.WriteLine($"{"체력",-5}: {totalHealth} + {Inventory.TotalHealth}");
                Console.WriteLine($"{"Gold",-7}: {Gold,3}G");
            }

            public void ObtainItem(Item item)
            {
                Inventory.AddItem(item);
            }
         }

        class PlayerInventory
        {
            private List<Item> items = new List<Item>();
            private HashSet<Item> equippedItems = new HashSet<Item>();


            public PlayerInventory()
            {
                items.Add(new Item("무쇠갑옷", 5, 0, 0, "무쇠로 만들어져 튼튼한 갑옷입니다."));
                items.Add(new Item("스파르타의 창", 0, 0, 7, "스파르타의 전사들이 사용했다는 전설의 창 입니다."));
                items.Add(new Item("낡은 검", 0, 0, 2, "쉽게 볼 수 있는 낡은 검 입니다."));
            }

            public void AddItem(Item item)
            {
                items.Add(item);
                Console.WriteLine($"{item.ItemName}을(를) 인벤토리에 추가하였습니다.");
            }

            public void ShowInventory()
            {
                if(items.Count == 0)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("==인벤토리가 비어있습니다.==");
                }

                for (int i = 0; i < items.Count; i++)
                {
                    string equippedMark = equippedItems.Contains(items[i]) ? "[E]" : "[-]";
                    Console.WriteLine ($"{equippedMark}{items[i].ShowItemInfo()}");                  
                }
            }
            public int TotalAttackPower => equippedItems.Sum(item => item.ItemAttackpower);
            public int TotalDefense => equippedItems.Sum(item => item.ItemDefense);
            public int TotalHealth => equippedItems.Sum(item => item.ItemHealth);

            public void EquipItem()
            {
                while (true)
                {
                    //장착 관리를 열었을 때 아이템 앞에 숫자가 나오게 하는 방법을 생각해보자
                    ShowInventory();
                    Console.WriteLine(" ");
                    Console.WriteLine("장착/해제할 아이템 번호를 입력하세요");
                    Console.WriteLine(" ");
                    Console.WriteLine("0. 나가기");
                    bool isEquip = int.TryParse(Console.ReadLine(), out int input);
                    
                    if (!isEquip || input < 0 || input > items.Count)
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        continue;
                    }
                    if (input == 0)
                    {
                        ShowInventory();
                        Console.WriteLine("장착 관리를 종료합니다.");
                        break;
                    }
                    
                    int index = input - 1;
                    Item selectedItem = items[index];

                    if (equippedItems.Contains(selectedItem))
                    {
                        equippedItems.Remove(selectedItem);
                        Console.WriteLine($"{selectedItem.ItemName}을(를) 장착 해제했습니다.");
                    }
                    else
                    {
                        equippedItems.Add(selectedItem);
                        Console.WriteLine($"{selectedItem.ItemName}을(를) 장착 했습니다.");
                    }

                }
            }
        }
        class Item
        {
            public string ItemName { get; set; }
            public int ItemDefense{ get; set; }
            public int ItemHealth { get; set; }
            public int ItemAttackpower { get; set; }
            public string ItemDescription { get; set; }

            public Item(string name, int defense, int health, int attackpower, string description)
            {
                ItemName = name;
                ItemDefense = defense;
                ItemHealth = health;
                ItemAttackpower = attackpower;
                ItemDescription = description;
            }
            public string ShowItemInfo()
            {
                string Replacename = ItemName.Replace(" ", string.Empty);
                int lenght = 15 - Replacename.Length ;
                string FixedItemName = ItemName.PadRight(lenght);

                return string.Format($"이름 : {FixedItemName}|방어력 : {ItemDefense,3}|체력 : {ItemHealth,3}|공격력 : {ItemAttackpower,3}|설명 : {ItemDescription,-3}");
            }
        }


        static void Store()
        {
            Console.WriteLine("상점보기 입니다.");
        }
    }
}
