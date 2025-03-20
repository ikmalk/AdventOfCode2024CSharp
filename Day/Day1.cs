namespace AdventOfCode2024.Day;

public class Day1 : IDay
{
    public void Start(string inputPath)
    {
        List<int> l1 = new List<int>();
        List<int> l2 = new List<int>();

        try
        {
            StreamReader sr = new StreamReader(inputPath);

            String line = sr.ReadLine();
            string[] values = line.Split("   ");
            l1.Add(Int32.Parse(values[0]));
            l2.Add(Int32.Parse(values[1]));

            while (line != null)
            {
                line = sr.ReadLine();
                values = line.Split("   ");

                l1.Add(Int32.Parse(values[0]));
                l2.Add(Int32.Parse(values[1]));
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

        int[] list1 = l1.ToArray();
        int[] list2 = l2.ToArray();

        // PART 1
        Array.Sort(list1);
        Array.Sort(list2);

        int distance = 0;
        for (int i = 0; i < list1.Length; i++)
        {
            int a = 0;
            int b = 0;

            if (list1[i] > list2[i])
            {
                a = list1[i];
                b = list2[i];
            }
            else
            {
                b = list1[i];
                a = list2[i];
            }

            distance = distance + (a - b);
        }

        Console.WriteLine("Distance is : {0}", distance);

        // PART 2
        Dictionary<int, int> dict = new Dictionary<int, int>();
        int similarityScore = 0;
        for (int i = 0; i < list1.Length; i++)
        {
            int leftNumber = list1[i];
            int count = 0;
            if (dict.TryGetValue(leftNumber, out count))
            {
                count = dict[leftNumber];
            }
            else
            {
                count = CountRightSide(list2, leftNumber);
                dict.Add(leftNumber, count);
            }

            similarityScore += (count * leftNumber);
        }

        Console.WriteLine("Similarity Score is : {0}", similarityScore);
    }

    int CountRightSide(int[] array, int value)
    {
        int count = 0;
        Array.Sort(array);
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] > value)
            {
                break;
            }
            else if (array[i] == value)
            {
                count += 1;
            }
        }

        return count;
    }
}