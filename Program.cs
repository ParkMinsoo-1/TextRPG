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
                Console.Clear();
                Console.WriteLine("당신의 직업은 무엇입니까?");
                Console.WriteLine("1. 전사 \n2. 도적 \n3. 궁수 \n4. 마법사 \n");
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

            Console.Clear();
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
                    Console.Clear();
                    Console.WriteLine("==========캐릭터의 정보가 표시됩니다.==========");
                    Console.WriteLine(" ");
                    player.PlayerStatus();
                    Console.WriteLine(" ");
                    Console.WriteLine("===============================================");

                    while (true)
                    {
                        Console.WriteLine(" ");
                        Console.WriteLine("0. 나가기");
                        Console.WriteLine(" ");
                        Console.Write("원하시는 행동을 입력해 주세요. \n>>");

                        bool exit = int.TryParse(Console.ReadLine(), out int exitNum);

                        if (!exit)
                        {
                            Console.WriteLine("숫자를 입력해 주세요.");
                            continue;
                        }
                        if (exitNum == 0)
                        {
                            Console.Clear();
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
                    Console.Clear();
                    player.Inventory.ShowInventory();

                    while (true)
                    {
                        Console.WriteLine(" ");
                        Console.WriteLine("1. 장착 관리 \n2. 나가기");
                        Console.WriteLine(" ");
                        Console.Write("원하시는 행동을 입력해 주세요. \n>>");

                        bool Inventory = int.TryParse(Console.ReadLine(), out int inventoryNum);

                        if (!Inventory)
                        {
                            Console.WriteLine("숫자를 입력해 주세요.");
                            continue;
                        }
                        if (inventoryNum == 1)
                        {
                            Console.Clear();
                            player.Inventory.EquipItem();
                        }
                        else if(inventoryNum == 2)
                        {
                            Console.Clear();
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
                    Console.Clear();
                    Store store = new Store();
                    store.EnterStore(player);
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
                Console.WriteLine($"{"공격력",-4}: {totalAttackpower,-3} (+ {Inventory.TotalAttackPower})");
                Console.WriteLine($"{"방어력",-4}: {totalDefense,-3} (+ {Inventory.TotalDefense})");
                Console.WriteLine($"{"체력",-5}: {totalHealth,-3} (+ {Inventory.TotalHealth})");
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
            public List<Item> GetItems()
            {
                return items;
            }
            public void RemoveItem(Item item)
            {
                items.Remove(item);
            }

            public PlayerInventory()
            {
                items.Add(new Item("무쇠갑옷", 5, 0, 0, "무쇠로 만들어져 튼튼한 갑옷입니다.", 500));
                items.Add(new Item("스파르타의 창", 0, 0, 7, "스파르타의 전사들이 사용했다는 전설의 창 입니다.", 500));
                items.Add(new Item("낡은 검", 0, 0, 2, "쉽게 볼 수 있는 낡은 검 입니다.", 100));
            }

            public void AddItem(Item item)
            {
                items.Add(item);
                Console.WriteLine($"{item.ItemName}을(를) 인벤토리에 추가하였습니다.");
            }

            public void ShowInventory(bool showIndex = false)
            {
                Console.WriteLine("========================= 인벤토리 =============================");

                if (items.Count == 0)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("==================== 인벤토리가 비어있습니다 ===================");
                    Console.WriteLine(" ");
                    Console.WriteLine("================================================================");
                    Console.WriteLine(" ");
                    return;
                }

                for (int i = 0; i < items.Count; i++)
                {
                    string equippedMark = equippedItems.Contains(items[i]) ? "[E]" : "[-]";
                    string inventoryInfo = items[i].ShowItemInfo(); 

                    if (showIndex)
                    {
                        Console.WriteLine($"{i + 1}.{equippedMark}{inventoryInfo}");
                     }
                    else
                    {
                        Console.WriteLine($"{equippedMark}{inventoryInfo}");
                    }

                }
                Console.WriteLine("================================================================");


            }
            public int TotalAttackPower => equippedItems.Sum(item => item.ItemAttackpower);
            public int TotalDefense => equippedItems.Sum(item => item.ItemDefense);
            public int TotalHealth => equippedItems.Sum(item => item.ItemHealth);

            public void EquipItem()
            {
                while (true)
                {
                    Console.Clear();
                    ShowInventory(true);
                    Console.WriteLine(" ");
                    Console.WriteLine("장착/해제할 아이템 번호를 입력하세요");
                    Console.WriteLine(" ");
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine(" ");
                    bool isEquip = int.TryParse(Console.ReadLine(), out int input);
                    
                    if (!isEquip || input < 0 || input > items.Count)
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        continue;
                    }
                    if (input == 0)
                    {
                        Console.Clear();
                        ShowInventory();
                        Console.WriteLine(" ");
                        Console.WriteLine("장착 관리를 종료합니다.");
                        Console.WriteLine(" ");
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
            public bool IsEquipped(Item item)
            {
                return equippedItems.Contains(item);
            }
        }
        class Item
        {
            public string ItemName { get; set; }
            public int ItemDefense { get; set; }
            public int ItemHealth { get; set; }
            public int ItemAttackpower { get; set; }
            public string ItemDescription { get; set; }
            public int ItemPrice { get; set; }

            public Item(string name, int defense, int health, int attackpower, string description, int itemPrice)
            {
                ItemName = name;
                ItemDefense = defense;
                ItemHealth = health;
                ItemAttackpower = attackpower;
                ItemDescription = description;
                ItemPrice = itemPrice;
            }
            public string ShowItemInfo(bool showPrice = false)
            {
                string Replacename = ItemName.Replace(" ", string.Empty);
                int lenght = 16 - Replacename.Length;
                string FixedItemName = ItemName.PadRight(lenght);

                string itemInfo = $"|이름 : {FixedItemName}|방어력 : {ItemDefense,2}|체력 : {ItemHealth,2}|공격력 : {ItemAttackpower,2}|설명 : {ItemDescription,-3}";
                string itemInfoPrice = $"|가격 :{ItemPrice,6}G {itemInfo}";

                if (showPrice)
                {
                    return itemInfoPrice;
                }

                return itemInfo;
            
            }
        }
        class Store
        {
            private List<Item> storeItem = new List<Item>();
            public Store()
            {
                storeItem.Add(new Item("수련자 갑옷", 5, 0, 0, "수련에 도움을 주는 갑옷입니다.", 500));
                storeItem.Add(new Item("무쇠 갑옷", 9, 0, 0, "무쇠로 만들어져 튼튼한 갑옷입니다.", 500));
                storeItem.Add(new Item("스파르타의 갑옷", 15, 0, 0, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 1000));
                storeItem.Add(new Item("낡은 검", 0, 0, 2, "쉽게 볼 수 있는 낡은 검입니다.", 500));
                storeItem.Add(new Item("청동 도끼", 0, 0, 5, "어디선가 사용되었던 느낌이드는 도끼입니다.", 500));
                storeItem.Add(new Item("스파르타의 창", 0, 0, 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 1000));
            }

            public void EnterStore(Player player)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("======================= 상 점 =======================");
                    Console.WriteLine("다양한 물건을 구매/판매 할 수 있습니다.");
                    Console.WriteLine(" ");
                    Console.WriteLine("1. 아이템 구매 ");
                    Console.WriteLine("2. 아이템 판매 ");
                    Console.WriteLine("0. 나가기 ");
                    Console.WriteLine(" ");
                    Console.WriteLine("=====================================================");
                    Console.WriteLine(" ");
                    Console.Write("원하시는 행동을 입력해 주세요. \n>>");
                    
                    bool isNum = int.TryParse(Console.ReadLine(), out int input);

                    if (!isNum) continue;
                    switch (input)
                    {
                        case 1:
                            BuyItem(player);
                            break;
                        case 2:
                            SellItem(player);
                            break;
                        case 0:
                            return;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
            }
            public void BuyItem(Player player)
            {
                while (true)
                {
                    Console.WriteLine("============== 구매 가능한 아이템 목록 ==============");
                    Console.WriteLine(" ");
                    for (int i = 0; i < storeItem.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}.{storeItem[i].ShowItemInfo(true)}");
                    }
                    Console.WriteLine(" ");
                    Console.WriteLine("======================================================");
                    Console.WriteLine(" ");
                    Console.WriteLine("구매를 원하는 아이템의 번호를 입력하세요.");
                    Console.WriteLine(" ");
                    Console.WriteLine("0. 나가기 ");
                    Console.WriteLine(" ");

                    Console.Write(">>");
                    bool isNum = int.TryParse(Console.ReadLine(), out int input);

                    if (!isNum || input < 0 || input > storeItem.Count)
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        continue;
                    }
                    if (input == 0)
                    {
                        return;
                    }

                    Item selectedItem = storeItem[input - 1];

                    if (player.Gold >= selectedItem.ItemPrice)
                    {
                        player.Gold -= selectedItem.ItemPrice;
                        Console.WriteLine($"{selectedItem.ItemName}을(를) 구매하였습니다.");
                        player.ObtainItem(selectedItem);
                    }
                    else
                    {
                        Console.WriteLine("골드가 부족합니다.");
                    }
                }

            }
            public void SellItem(Player player)
            {
                while (true)
                {
                    List<Item> playerItems = player.Inventory.GetItems().Where(item => !player.Inventory.IsEquipped(item)).ToList();

                    Console.WriteLine("============== 판매 가능한 아이템 목록 ==============");
                    Console.WriteLine(" ");
                    
                    if (playerItems.Count == 0)
                    {
                        Console.WriteLine("판매할 아이템이 없습니다.");
                        Console.WriteLine(" ");
                        Console.WriteLine("=====================================================");
                        Console.WriteLine(" ");
                        Console.WriteLine("0. 나가기");
                        Console.WriteLine(" ");

                        Console.Write(">> ");
                        bool isExit = int.TryParse(Console.ReadLine(), out int exitNum);
                        if(!isExit || exitNum < 0)
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                            continue;
                        }

                        if(exitNum == 0)
                        {
                            return;
                        }
                        
                    }

                    for (int i = 0; i < playerItems.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}.|판매가격 : {playerItems[i].ItemPrice * 0.85,3}G {playerItems[i].ShowItemInfo()}");
                    }

                    Console.WriteLine(" ");
                    Console.WriteLine("=====================================================");
                    Console.WriteLine(" ");
                    Console.WriteLine("판매를 원하는 아이템의 번호를 입력하세요. ");
                    Console.WriteLine(" ");
                    Console.WriteLine("0. 나가기 ");
                    Console.WriteLine(" ");

                    Console.WriteLine(">> ");
                    bool isNum = int.TryParse(Console.ReadLine(), out int input);
                    if (!isNum || input < 0 || input > playerItems.Count)
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        continue;
                    }
                    if (input == 0) return;

                    Item selectedItem = playerItems[input - 1];
                    player.Inventory.RemoveItem(selectedItem);
                    int sellPrice = (int)(selectedItem.ItemPrice * 0.85);
                    player.Gold += sellPrice;
                    Console.WriteLine($"{selectedItem.ItemName}을(를) {sellPrice}G에 판매하였습니다.");
                }
            }

        }

    }
}
