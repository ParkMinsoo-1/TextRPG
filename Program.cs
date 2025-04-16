using System.Security.Cryptography.X509Certificates;

namespace TextRPG
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            string job = "";
            int num = 0;
            
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("플레이할 캐릭터의 이름을 적어 주세요.\n");
            
            string playerName = Console.ReadLine();
            

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("당신의 직업은 무엇입니까?");
                Console.WriteLine("1. 전사 \n2. 도적 \n3. 궁수 \n4. 마법사");
                bool isNum = int.TryParse(Console.ReadLine(), out num);

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
            Player player = new Player();
            player.Job = job;
            player.PlayerName = playerName;

            Console.WriteLine(" ");
            Console.WriteLine($"환영합니다. 전설의 {player.Job} {player.PlayerName}님.");

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
                    break;
                }
                else if (num == 2)
                {
                    ShowInventory();
                    break;
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

        class Player()
        {
            public int Level = 1;
            public string PlayerName ;
            public string Job;
            public int Attackpower = 10;
            public int Defense = 5;
            public int Health = 50;
            public int Gold = 1500;

            public void PlayerStatus()
            {
                Console.WriteLine("Lv. " + Level);
                Console.WriteLine("이름 : " + PlayerName);
                Console.WriteLine("직업 : " + Job);
                Console.WriteLine("공격력 : " + Attackpower);
                Console.WriteLine("방어력 : " + Defense);
                Console.WriteLine("체 력 : " + Health);
                Console.WriteLine("Gold : " + Gold + "G");
            }
         }
        
        static void ShowInventory()
        {
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        }

        static void Store()
        {
            Console.WriteLine("상점보기 입니다.");
        }
    }
}
