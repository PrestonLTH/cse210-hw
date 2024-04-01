using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public abstract class Goal
{
    public string Name { get; set; }
    public int Value { get; protected set; }
    public bool IsComplete { get; protected set; }

    public Goal(string name)
    {
        Name = name;
        IsComplete = false;
    }

    public virtual int CompleteGoal()
    {
        IsComplete = true;
        return Value;
    }

    public virtual string GetStatus()
    {
        return IsComplete ? "[X] " + Name : "[ ] " + Name;
    }
}

public class SimpleGoal : Goal
{
    public SimpleGoal(string name, int value) : base(name)
    {
        Value = value;
    }
}

public class EternalGoal : Goal
{
    public EternalGoal(string name, int value) : base(name)
    {
        Value = value;
    }

    public override int CompleteGoal()
    {
        return Value;
    }
}

public class ChecklistGoal : Goal
{
    private int targetCount;
    private int currentCount;

    public ChecklistGoal(string name, int value, int targetCount) : base(name)
    {
        Value = value;
        this.targetCount = targetCount;
        currentCount = 0;
    }

    public override int CompleteGoal()
    {
        currentCount++;
        if (currentCount >= targetCount)
        {
            IsComplete = true;
            return Value + 500;
        }
        return Value;
    }

    public override string GetStatus()
    {
        return IsComplete ? "[X] " + Name + " (Completed " + currentCount + "/" + targetCount + " times)" : "[ ] " + Name + " (Completed " + currentCount + "/" + targetCount + " times)";
    }
}

public class EternalQuestManager
{
    private List<Goal> goals;
    private int score;

    public EternalQuestManager()
    {
        goals = new List<Goal>();
        score = 0;
    }

    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
    }

    public void RecordEvent(int index)
    {
        if (index >= 0 && index < goals.Count)
        {
            score += goals[index].CompleteGoal();
        }
    }

    public void DisplayGoals()
    {
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + goals[i].GetStatus());
        }
    }

    public void DisplayScore()
    {
        Console.WriteLine("Your current score is: " + score);
    }

    public void SaveProgress(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (Goal goal in goals)
            {
                writer.WriteLine(goal.GetType().Name + "," + goal.Name + "," + goal.IsComplete);
            }
            writer.WriteLine("Score," + score);
        }
    }

public void LoadProgress(string fileName)
{
    goals.Clear();
    using (StreamReader reader = new StreamReader(fileName))
    {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] parts = line.Split(',');
            if (parts.Length < 2)
            {
                //Console.WriteLine("Invalid data format: " + line);
                continue;
            }
            if (parts[0] == "SimpleGoal" && parts.Length >= 3)
            {
                int value;
                if (int.TryParse(parts[2], out value))
                {
                    goals.Add(new SimpleGoal(parts[1], value));
                }
                else
                {
                    //Console.WriteLine("Invalid data format: " + line);
                }
            }
            else if (parts[0] == "EternalGoal" && parts.Length >= 3)
            {
                int value;
                if (int.TryParse(parts[2], out value))
                {
                    goals.Add(new EternalGoal(parts[1], value));
                }
                else
                {
                    //Console.WriteLine("Invalid data format: " + line);
                }
            }
            else if (parts[0] == "ChecklistGoal" && parts.Length >= 4)
            {
                int value, targetCount;
                if (int.TryParse(parts[2], out value) && int.TryParse(parts[3], out targetCount))
                {
                    goals.Add(new ChecklistGoal(parts[1], value, targetCount));
                }
                else
                {
                    //Console.WriteLine("Invalid data format: " + line);
                }
            }
            else if (parts[0] == "Score" && parts.Length >= 2)
            {
                score = int.Parse(parts[1]);
            }
            else
            {
                //Console.WriteLine("Invalid data format: " + line);
            }
        }
    }
}
}
class Program
{
    static void Main(string[] args)
    {
        EternalQuestManager manager = new EternalQuestManager();

        manager.LoadProgress("progress.txt");

        manager.AddGoal(new SimpleGoal("Run a marathon", 1000));
        manager.AddGoal(new EternalGoal("Read scriptures", 100));
        manager.AddGoal(new ChecklistGoal("Attend the temple", 50, 10));

        manager.DisplayGoals();

        Console.WriteLine("Record an event (enter goal number): ");
        int goalIndex = int.Parse(Console.ReadLine()) - 1;
        manager.RecordEvent(goalIndex);

        manager.DisplayScore();

        manager.SaveProgress("progress.txt");
    }
}