using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your grade percentage? ");
        string userInput = Console.ReadLine();
        int grade = int.Parse(userInput);

        if (grade >= 93)
        {
            Console.WriteLine("A");
        }
        else if (grade >= 90)
        {
            Console.WriteLine("A-");
        }
        else if (grade >= 87)
        {
            Console.WriteLine("B+");
        }
        else if (grade >= 83)
        {
            Console.WriteLine("B");
        }
        else if (grade >= 80)
        {
            Console.WriteLine("B-");
        }
        else if (grade >= 77)
        {
            Console.WriteLine("C+");
        }
        else if (grade >= 73)
        {
            Console.WriteLine("C");
        }
        else if (grade >= 70)
        {
            Console.WriteLine("C-");
        }
        else if (grade >= 67)
        {
            Console.WriteLine("D+");
        }
        else if (grade >= 63)
        {
            Console.WriteLine("D");
        }
        else if (grade >= 60)
        {
            Console.WriteLine("D-");
        }
        else
        {
            Console.WriteLine("F");
        }

        if (grade >= 70)
        {
            Console.WriteLine("You passed the course");
        }
        else
        {
            Console.WriteLine("You suck. Try getting a better grade next time loser");
        }

        if (grade >= 77)
        {
            Console.WriteLine("Congratulations! You made it onto the Deans List!");
        }
    }
}


// if percent >= 70:
//   print("You passed the course")
// else:
//   print("You suck. Try getting a better grade next time loser")

// if percent >= 77:
//   print("Congratulations! You Made it onto the Deans List!")

//   print()

