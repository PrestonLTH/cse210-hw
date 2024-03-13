using System;
using System.Collections.Generic;

public class Job
{
    public string _jobTitle;
    public string _company;
    public int _startYear;
    public int _endYear;

    public void Display()
    {
        Console.WriteLine($"{_jobTitle} ({_company}) {_startYear}-{_endYear}");
    }
}

public class Resume
{
    public string _name;
    public List<Job> _jobs = new List<Job>();

    public void Display()
    {
        Console.WriteLine($"Name: {_name}");
        Console.WriteLine("Jobs:");

        foreach (Job job in _jobs)
        {
            job.Display();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Resume myResume = new Resume();
        
        Console.Write("Enter your name: ");
        myResume._name = Console.ReadLine();

        for (int i = 1; ; i++)
        {
            Job job = new Job();
            
            Console.WriteLine($"\nEnter details for job {i} (press Enter to skip):");

            Console.Write("Job title: ");
            job._jobTitle = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(job._jobTitle))
                break;

            Console.Write("Company: ");
            job._company = Console.ReadLine();

            Console.Write("Start year: ");
            int.TryParse(Console.ReadLine(), out job._startYear);

            Console.Write("End year: ");
            int.TryParse(Console.ReadLine(), out job._endYear);

            myResume._jobs.Add(job);
        }

        myResume.Display();
    }
}
