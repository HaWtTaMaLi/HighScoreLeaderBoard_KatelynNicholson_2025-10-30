using System;
using System.Linq;
using System.Threading;
using System.IO;

namespace HighScoreLeaderBoard_KatelynNicholson_2025_10_28
{
    internal class Program
    {

        static void Main()
        {
            //String Path
            string path = @"LeaderBoard.txt";

            //random gen between 1 and 1000
            Random rnd = new Random();
            int score = rnd.Next(1, 1001);
            Console.WriteLine($"Your score: {score}");

            Thread.Sleep(1000);

            //prompt for initials
            string initials = "";
            do
            {
                Console.WriteLine("Enter your 3 initials: ");
                initials = Console.ReadLine().Trim().ToUpper();

                if (initials.Length != 3)
                    Console.WriteLine("Please enter exactly 3 characters.");
            }
            while (initials.Length != 3);

            //read data
            string allData = "";
            if (File.Exists(path))
            {
                allData = File.ReadAllText(path);
            }

            if (!string.IsNullOrWhiteSpace(allData))
                allData += ",";

            allData += ($"{score} {initials}");

            string[] entries = allData.Split(',');

            var sorted = entries
                .Select(i => i.Trim())
                .Select(i => new
                {
                    Score = int.Parse(i.Split(' ')[0]),
                    Initials = i.Split(' ')[1]
                })
                .OrderByDescending(i => i.Score)
                .ToList();

            string newData = string.Join(",", sorted.Select(i => $"{i.Score} {i.Initials}"));
            File.WriteAllText(path, newData);

            Console.WriteLine($"\n ---- High Scores ----");
            foreach (var entry in sorted)
            {
                Console.WriteLine($"{entry.Score} {entry.Initials}");
            }

            Console.WriteLine("\nPress any key to exit the game.");
            Console.ReadKey();
        }
    }
}
