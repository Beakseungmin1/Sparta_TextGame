using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics.Metrics;

namespace Sparta_TextGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] Filednumber = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            Console.WriteLine("      |     |      ");
            Console.WriteLine("  {0}   |  {1}  |  {2}   ", Filednumber[0], Filednumber[1], Filednumber[2]);
            Console.WriteLine("      |     |      ");
            Console.WriteLine("------+-----+-----");
            Console.WriteLine("      |     |      ");
            Console.WriteLine("  {0}   |  {1}  |  {2}   ", Filednumber[4], Filednumber[5], Filednumber[6]);
            Console.WriteLine("      |     |      ");
            Console.WriteLine("------+-----+-----");
            Console.WriteLine("      |     |      ");
            Console.WriteLine("  {0}   |  {1}  |  {2}   ", Filednumber[7], Filednumber[8], Filednumber[9]);
            Console.WriteLine("      |     |      ");
            Console.WriteLine("------+-----+-----");

            while (true)
            {
                Random random = new Random();
                bool turn = true;
                int playerChoice;

                if (turn)
                {
                    Console.WriteLine("당신의 턴입니다");
                    playerChoice = int.Parse(Console.ReadLine());
                    if (Filednumber[playerChoice] == "＠" || Filednumber[playerChoice] == "X")
                    {
                        Console.WriteLine("그곳에는 이미 돌이 놓여져있습니다");
                        continue;
                    }
                    else
                    {
                        Filednumber[playerChoice] = "＠";
                        if ((Filednumber[0] == Filednumber[1] && Filednumber[1] == Filednumber[2]) ||
                           (Filednumber[4] == Filednumber[5] && Filednumber[5] == Filednumber[6]) ||
                           (Filednumber[7] == Filednumber[8] && Filednumber[8] == Filednumber[9]) ||

                           (Filednumber[0] == Filednumber[4] && Filednumber[4] == Filednumber[7]) ||
                           (Filednumber[1] == Filednumber[5] && Filednumber[5] == Filednumber[8]) ||
                           (Filednumber[2] == Filednumber[6] && Filednumber[6] == Filednumber[9]) ||

                           (Filednumber[0] == Filednumber[5] && Filednumber[5] == Filednumber[9]) ||
                           (Filednumber[7] == Filednumber[5] && Filednumber[5] == Filednumber[2]))
                        {
                            if (turn)
                            {
                                Console.WriteLine("      |     |      ");
                                Console.WriteLine("  {0}   |  {1}  |  {2}   ", Filednumber[0], Filednumber[1], Filednumber[2]);
                                Console.WriteLine("      |     |      ");
                                Console.WriteLine("------+-----+-----");
                                Console.WriteLine("      |     |      ");
                                Console.WriteLine("  {0}   |  {1}  |  {2}   ", Filednumber[4], Filednumber[5], Filednumber[6]);
                                Console.WriteLine("      |     |      ");
                                Console.WriteLine("------+-----+-----");
                                Console.WriteLine("      |     |      ");
                                Console.WriteLine("  {0}   |  {1}  |  {2}   ", Filednumber[7], Filednumber[8], Filednumber[9]);
                                Console.WriteLine("      |     |      ");
                                Console.WriteLine("------+-----+-----");
                            }
                            Console.WriteLine("게임끝!");
                            break;
                        }
                        turn = false;
                    }
                }
                if (!turn)
                {
                    Console.WriteLine("컴퓨터의 턴입니다");
                    int computerChoice = random.Next(0, 10);
                    if (Filednumber[computerChoice] == "X" || Filednumber[computerChoice] == "＠")
                    {
                        computerChoice = random.Next(0, 10);
                        continue;
                    }
                    else
                    {
                        Filednumber[computerChoice] = "X";
                        Console.WriteLine("      |     |      ");
                        Console.WriteLine("  {0}   |  {1}  |  {2}   ", Filednumber[0], Filednumber[1], Filednumber[2]);
                        Console.WriteLine("      |     |      ");
                        Console.WriteLine("------+-----+-----");
                        Console.WriteLine("      |     |      ");
                        Console.WriteLine("  {0}   |  {1}  |  {2}   ", Filednumber[4], Filednumber[5], Filednumber[6]);
                        Console.WriteLine("      |     |      ");
                        Console.WriteLine("------+-----+-----");
                        Console.WriteLine("      |     |      ");
                        Console.WriteLine("  {0}   |  {1}  |  {2}   ", Filednumber[7], Filednumber[8], Filednumber[9]);
                        Console.WriteLine("      |     |      ");
                        Console.WriteLine("------+-----+-----");
                        turn = true;
                        if ((Filednumber[0] == Filednumber[1] && Filednumber[1] == Filednumber[2]) ||
                           (Filednumber[4] == Filednumber[5] && Filednumber[5] == Filednumber[6]) ||
                           (Filednumber[7] == Filednumber[8] && Filednumber[8] == Filednumber[9]) ||

                           (Filednumber[0] == Filednumber[4] && Filednumber[4] == Filednumber[7]) ||
                           (Filednumber[1] == Filednumber[5] && Filednumber[5] == Filednumber[8]) ||
                           (Filednumber[2] == Filednumber[6] && Filednumber[6] == Filednumber[9]) ||

                           (Filednumber[0] == Filednumber[5] && Filednumber[5] == Filednumber[9]) ||
                           (Filednumber[7] == Filednumber[5] && Filednumber[5] == Filednumber[2]))
                        {
                            Console.WriteLine("게임끝!");
                            break;
                        }

                    }
                }
            }
        }
    }
}
