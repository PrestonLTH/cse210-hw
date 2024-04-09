using System;
using System.Threading;
using System.Collections.Generic;

class PT_Program
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
                int PT_breathingDuration = int.Parse(Console.ReadLine());
                PT_BreathingActivity PT_breathingActivity = new PT_BreathingActivity(PT_breathingDuration);
                PT_breathingActivity.Start();
                break;
            case "2":
                Console.Write("Enter the duration of the activity in seconds: ");
                int PT_reflectionDuration = int.Parse(Console.ReadLine());
                PT_ReflectionActivity PT_reflectionActivity = new PT_ReflectionActivity(PT_reflectionDuration);
                PT_reflectionActivity.Start();
                break;
            case "3":
                Console.Write("Enter the duration of the activity in seconds: ");
                int PT_listingDuration = int.Parse(Console.ReadLine());
                PT_ListingActivity PT_listingActivity = new PT_ListingActivity(PT_listingDuration);
                PT_listingActivity.Start();
                break;
            default:
                Console.WriteLine("Invalid choice. Exiting...");
                break;
        }
    }
}

class PT_Activity
{
    protected int PT_duration;

    public PT_Activity(int duration)
    {
        this.PT_duration = duration;
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

class PT_Spinner
{
    public static void Show()
    {
        Console.WriteLine("Loading...");
    }
}

class PT_BreathingActivity : PT_Activity
{
    public PT_BreathingActivity(int duration) : base(duration)
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
        while (DateTime.Now - startTime < TimeSpan.FromSeconds(PT_duration))
        {
            Console.WriteLine("Breathe in...");
            PT_Spinner.Show();
            Thread.Sleep(3000);
            Console.WriteLine("Breathe out...");
            PT_Spinner.Show();
            Thread.Sleep(3000);
        }

        End("Breathing", PT_duration);
    }
}

class PT_ReflectionActivity : PT_Activity
{
    private List<string> PT_prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> PT_questions = new List<string>
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

    public PT_ReflectionActivity(int duration) : base(duration)
    {
    }

    public override void Start()
    {
        base.Start();
        Console.WriteLine("This activity will help you reflect on times in your life when you have shown strength and resilience.");
        Console.WriteLine("This will help you recognize the power you have and how you can use it in other aspects of your life.");
        Thread.Sleep(2000);
        string prompt = PT_prompts[new Random().Next(PT_prompts.Count)];
        Console.WriteLine(prompt);
        Thread.Sleep(2000);
        ReflectionQuestions();
    }

    private void ReflectionQuestions()
    {
        foreach (string question in PT_questions)
        {
            Console.WriteLine(question);
            PT_Spinner.Show();
            Thread.Sleep(3000);
        }

        End("Reflection", PT_duration);
    }
}

class PT_ListingActivity : PT_Activity
{
    private List<string> PT_prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public PT_ListingActivity(int duration) : base(duration)
    {
    }

    public override void Start()
    {
        base.Start();
        Console.WriteLine("This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");
        Thread.Sleep(2000);
        string prompt = PT_prompts[new Random().Next(PT_prompts.Count)];
        Console.WriteLine(prompt);
        Thread.Sleep(5000);
        Console.WriteLine("Now, start listing...");
        ListItems();
    }

    private void ListItems()
    {
        List<string> items = new List<string>();
        DateTime startTime = DateTime.Now;
        while (DateTime.Now - startTime < TimeSpan.FromSeconds(PT_duration))
        {
            Console.Write("Enter an item (or type 'done' to finish): ");
            string item = Console.ReadLine();
            if (item.ToLower() == "done")
                break;
            items.Add(item);
        }

        Console.WriteLine($"You listed {items.Count} items.");
        End("Listing", PT_duration);
    }
}
