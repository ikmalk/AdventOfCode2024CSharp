namespace AdventOfCode2024.Day;

public class Day2 : IDay
{
    public void Start(string inputPath)
    {
        List<int?[]> list = new List<int?[]>();
        try
        {
            StreamReader sr = new StreamReader(inputPath);

            String line = sr.ReadLine();

            while (line != null)
            {
                String[] parts = line.Split(" ");
                int?[] numbers = new int?[parts.Length];
                for (int i = 0; i < parts.Length; i++)
                {
                    numbers[i] = (int?)int.Parse(parts[i]);
                }

                list.Add(numbers);

                line = sr.ReadLine();
            }

            sr.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Executing finally block.");
        }

        int?[][] levels = list.ToArray();
        // int?[][] levels =
        // {
        //     new int?[]{7, 6, 4, 2, 1},
        //     new int?[]{1, 2, 7, 8, 9},
        //     new int?[]{9, 7, 6, 2, 1},
        //     new int?[]{1, 3, 2, 4, 5},
        //     new int?[]{8, 6, 4, 4, 1},
        //     new int?[]{1, 3, 6, 7, 9},
        // };

        
        string[] p1IndexSafety = new string[levels.Length];
        string[] p2IndexSafety = new string[levels.Length];

        for (int i = 0; i < levels.Length; i++)
        {
            int?[] items = levels[i];
            string safety = CheckSafety(items);
            p1IndexSafety[i] = safety;
            
            // P2 Problem Dampener
            if (safety == "Unsafe")
            {
                for (int j = 0; j < items.Length; j++)
                {
                    int?[] tempItems = new int?[items.Length];
                    items.CopyTo(tempItems, 0);
                    tempItems[j] = null;
                    safety = CheckSafety(tempItems);
                    if (safety == "Safe")
                    {
                        break;
                    }
                }
            }

            p2IndexSafety[i] = safety;
        }

        int p1CountSafe = 0;
        for (int i = 0; i < p1IndexSafety.Length; i++)
        {
            if (p1IndexSafety[i] == "Safe")
            {
                p1CountSafe++;
            }
        }
        
        int p2CountSafe = 0;
        for (int i = 0; i < p2IndexSafety.Length; i++)
        {
            if (p2IndexSafety[i] == "Safe")
            {
                p2CountSafe++;
            }
        }

        Console.WriteLine("P1 Safety Count: {0}", p1CountSafe);
        Console.WriteLine("P2 Safety Count With Problem Dampener: {0}", p2CountSafe);
  
    }

    string CheckSafety(int?[] items)
    {
        string safety = "Safe";
        int? first = items[0];
        string initialState = null;
        for (int i = 1; i < items.Length; i++)
        {
            int? second = items[i];
            
            // check for problem dampener in part 2
            if (first == null)
            {
                first = second;
                continue;
            }
            if (second == null)
            {
                continue;
            }

            int? diff = first - second;
            string state = null;
            if (diff < 0)
            {
                diff *= -1;
                state = "negative";
            }
            else if (diff > 0)
            {
                state = "positive";
            }

            if (initialState == null && state != null)
            {
                initialState = state;
            }
            else if (initialState != state && state != null)
            {
                safety = "Unsafe";
            }

            if (diff > 3 || diff < 1)
            {
                safety = "Unsafe";
                break;
            }

            first = items[i];
        }

        return safety;
    }
}