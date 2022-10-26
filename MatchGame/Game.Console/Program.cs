using System;
using Game.Core;

namespace Game.Consoles
{
    class Program
    {

        static void Main(string[] args)
        {
            GameClient gameClient = new GameClient();
            Console.WriteLine("游戏开始。。。。");
            Console.WriteLine("*************要想撤回游戏请输入命令 un-steps,如：un-3 将撤回到第3轮游戏****************");
            Console.WriteLine("*************玩游戏命令输入 position-number,如：2-4 将在第2行拿走4根火柴****************");


            while (true)
            {
                Console.WriteLine(gameClient.ToString());
                Console.WriteLine($"第{gameClient.CurrentSteps}轮，玩家：[{gameClient.CurrentPlayer.Name}]拿走火柴 position-number：");
                var readStr = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(readStr))
                {
                    ShowMsg();
                    continue;
                }

                var readChars = readStr.Split('-');
                if (readChars.Length != 2)
                {
                    ShowMsg();
                    continue;
                }

                int position = 0;
                int number = 0;


                //撤回游戏命令处理
                if (readChars[0].Equals("un", StringComparison.OrdinalIgnoreCase)
                    && int.TryParse(readChars[1], out number)
                    && gameClient.Undo(number))
                {
                    Console.WriteLine($"游戏撤回到第{number}轮成功！ ");
                    continue;
                }

                //玩游戏命令处理
                if (!int.TryParse(readChars[0], out position) || !int.TryParse(readChars[1], out number))
                {
                    ShowMsg();
                    continue;
                }
                gameClient.Start(position, number);
            }
        }

        static void ShowMsg()
        {
            Console.WriteLine("输入格式不正式，请参照格式 position-number ");
        }
    }
}
