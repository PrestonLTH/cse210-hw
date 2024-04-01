using System;
using System.Threading;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Activity Program!");
        Console.WriteLine("Choose an activity:");
        Console.WriteLine("1. Breathing Activity");
        Console.WriteLine("2. Reflection Activity");
        Console.WriteLine("3. Listing Activity");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("Enter the duration of the activity in seconds: ");
                int breathingDuration = int.Parse(Console.ReadLine());
                BreathingActivity breathingActivity = new BreathingActivity(breathingDuration);
                breathingActivity.Start();
                break;
            case "2":
                Console.Write("Enter the duration of the activity in seconds: ");
                int reflectionDuration = int.Parse(Console.ReadLine());
                ReflectionActivity reflectionActivity = new ReflectionActivity(reflectionDuration);
                reflectionActivity.Start();
                break;
            case "3":
                Console.Write("Enter the duration of the activity in seconds: ");
                int listingDuration = int.Parse(Console.ReadLine());
                ListingActivity listingActivity = new ListingActivity(listingDuration);
                listingActivity.Start();
                break;
            default:
                Console.WriteLine("Invalid choice. Exiting...");
                break;
        }
    }
}

class Activity
{
    protected int duration;

    public Activity(int duration)
    {
        this.duration = duration;
    }

    public virtual void Start()
    {
        Console.WriteLine("Prepare to begin...");
        Thread.Sleep(3000);
    }

    public virtual void End(string activityName, int duration)
    {
        Console.WriteLine("You've done a good job!");
        Thread.Sleep(2000);
        Console.WriteLine($"You've completed the {activityName} activity in {duration} seconds.");
        Thread.Sleep(3000);
    }
}

class Spinner
{
    public static void Show()
    {
        Console.WriteLine("Loading...");
    }
}

class BreathingActivity : Activity
{
    public BreathingActivity(int duration) : base(duration)
    {
    }

    public override void Start()
    {
        base.Start();
        Console.WriteLine("This activity will help you relax by walking you through breathing in and out slowly.");
        Console.WriteLine("Clear your mind and focus on your breathing.");
        Thread.Sleep(2000);
        BreathingCycle();
    }

    private void BreathingCycle()
    {
        DateTime startTime = DateTime.Now;
        while (DateTime.Now - startTime < TimeSpan.FromSeconds(duration))
        {
            Console.WriteLine("Breathe in...");
            Spinner.Show();
            Thread.Sleep(3000);
            Console.WriteLine("Breathe out...");
            Spinner.Show();
            Thread.Sleep(3000);
        }

        End("Breathing", duration);
    }
}

class ReflectionActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity(int duration) : base(duration)
    {
    }

    public override void Start()
    {
        base.Start();
        Console.WriteLine("This activity will help you reflect on times in your life when you have shown strength and resilience.");
        Console.WriteLine("This will help you recognize the power you have and how you can use it in other aspects of your life.");
        Thread.Sleep(2000);
        string prompt = prompts[new Random().Next(prompts.Count)];
        Console.WriteLine(prompt);
        Thread.Sleep(2000);
        ReflectionQuestions();
    }

    private void ReflectionQuestions()
    {
        foreach (string question in questions)
        {
            Console.WriteLine(question);
            Spinner.Show();
            Thread.Sleep(3000);
        }

        End("Reflection", duration);
    }
}

class ListingActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity(int duration) : base(duration)
    {
    }

    public override void Start()
    {
        base.Start();
        Console.WriteLine("This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");
        Thread.Sleep(2000);
        string prompt = prompts[new Random().Next(prompts.Count)];
        Console.WriteLine(prompt);
        Thread.Sleep(5000);
        Console.WriteLine("Now, start listing...");
        ListItems();
    }

    private void ListItems()
    {
        List<string> items = new List<string>();
        DateTime startTime = DateTime.Now;
        while (DateTime.Now - startTime < TimeSpan.FromSeconds(duration))
        {
            Console.Write("Enter an item (or type 'done' to finish): ");
            string item = Console.ReadLine();
            if (item.ToLower() == "done")
                break;
            items.Add(item);
        }

        Console.WriteLine($"You listed {items.Count} items.");
        End("Listing", duration);
    }
}
