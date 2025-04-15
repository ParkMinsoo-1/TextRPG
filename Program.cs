using System.Security.Cryptography.X509Certificates;

namespace TextRPG
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            string job = "";
            
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("플레이할 캐릭터의 이름을 적어 주세요.\n");
            
            string playerName = Console.ReadLine();
            
            Console.WriteLine(" ");
            Console.WriteLine("당신의 직업은 무엇입니까?");
            Console.WriteLine("1. 전사 \n2. 도적 \n3. 궁수 \n4. 마법사");

            int num = int.Parse(Console.ReadLine());
            
            if (num ==1)
            {
               job = "전사";
            }
            else if (num == 2)
            {
                job = "도적";
            }
            else if (num == 3)
            {
                job = "궁수";
            }
            else if (num == 4)
            {
                job = "마법사";
            }
            else
            {
                
            }
            Player player = new Player();
            player.Job = job;
            player.PlayerName = playerName;

            Console.WriteLine(" ");
            Console.WriteLine($"환영합니다. 전설의 {player.Job} {player.PlayerName}님.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다. \n \n1. 상태 보기 \n2. 인벤토리 \n3. 상점 \n");
            SelectMenu(player);
        }

        static void SelectMenu(Player player)
        {

            Console.Write("원하시는 행동을 입력해주세요. ");
            int num = int.Parse(Console.ReadLine());
            if (num == 1)
            {
                Console.WriteLine(" ");
                Console.WriteLine("상태 보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.");

                player.PlayerStatus();

                if (num == 0)
                {

                }
            }
            if (num == 2)
            {
                ShowInventory();
            }
            if (num == 3)
            {
                Store();
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 선택해 주세요.");
            }
        }

        class Player()
        {
            public int Level = 1;
            public string PlayerName ;
            public string Job;
            public int Attackpower;
            public int Defense;
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
