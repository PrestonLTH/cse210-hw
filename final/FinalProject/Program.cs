using System;
using System.Collections.Generic;
using System.IO;

abstract class PT_Workout
{
    public string PT_Exercise { get; set; }
    public int PT_Sets { get; set; }
    public int PT_Reps { get; set; }
    public double PT_Weight { get; set; }
    public DateTime PT_Date { get; set; }

    public abstract double PT_CalculateTotalVolume();
}

class PT_StrengthWorkout : PT_Workout
{
    public override double PT_CalculateTotalVolume()
    {
        return PT_Sets * PT_Reps * PT_Weight;
    }
}

class PT_CardioWorkout : PT_Workout
{
    public double PT_Distance { get; set; }
    public TimeSpan PT_Duration { get; set; }

    public override double PT_CalculateTotalVolume()
    {
        return 0;
    }
}

class PT_Program
{
    static List<PT_Workout> PT_workouts = new List<PT_Workout>();
    const string PT_dataFilePath = "workout_data.txt";

    static void Main(string[] args)
    {
        PT_LoadWorkoutData();

        Console.WriteLine("Welcome to the Gym Workout Tracker!");

        while (true)
        {
            Console.WriteLine("\n1. Add Workout");
            Console.WriteLine("2. View Workouts");
            Console.WriteLine("3. Exit");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    PT_AddWorkout();
                    PT_SaveWorkoutData();
                    break;
                case "2":
                    PT_ViewWorkouts();
                    break;
                case "3":
                    Console.WriteLine("Exiting program...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void PT_AddWorkout()
    {
        Console.WriteLine("\nEnter workout details:");

        Console.Write("Exercise name: ");
        string exercise = Console.ReadLine();

        Console.Write("Number of sets: ");
        int sets = int.Parse(Console.ReadLine());

        Console.Write("Number of reps: ");
        int reps = int.Parse(Console.ReadLine());

        Console.Write("Weight (in lbs): ");
        double weight = double.Parse(Console.ReadLine());

        char workoutType;

        while (true)
        {
            Console.WriteLine("Is this a strength workout or cardio workout? (S/C)");
            string input = Console.ReadLine().ToUpper();

            if (input == "S" || input == "C")
            {
                workoutType = input[0];
                break;
            }
            else
            {
                Console.WriteLine("Invalid workout type. Please enter 'S' for strength workout or 'C' for cardio workout.");
            }
        }

        PT_Workout workout;
        switch (workoutType)
        {
            case 'S':
                workout = new PT_StrengthWorkout();
                break;
            case 'C':
                workout = new PT_CardioWorkout();
                break;
            default:
                Console.WriteLine("Invalid workout type. Defaulting to strength workout.");
                workout = new PT_StrengthWorkout();
                break;
        }

        workout.PT_Exercise = exercise;
        workout.PT_Sets = sets;
        workout.PT_Reps = reps;
        workout.PT_Weight = weight;
        workout.PT_Date = DateTime.Now;

        PT_workouts.Add(workout);

        double totalWeight = PT_CalculateTotalWeight(workout);
        Console.WriteLine($"\nTotal weight lifted for this workout: {totalWeight} lbs");

        Console.WriteLine("\nWorkout added successfully!");
    }

    static double PT_CalculateTotalWeight(PT_Workout workout)
    {
        return workout.PT_Weight * workout.PT_Reps * workout.PT_Sets;
    }

    static void PT_ViewWorkouts()
    {
        if (PT_workouts.Count == 0)
        {
            Console.WriteLine("No workouts added yet.");
            return;
        }

        Console.WriteLine("\n-- Your Workouts --");
        foreach (var workout in PT_workouts)
        {
            Console.WriteLine($"\nDate: {workout.PT_Date.ToShortDateString()}");
            Console.WriteLine($"Exercise: {workout.PT_Exercise}\nSets: {workout.PT_Sets}\nReps: {workout.PT_Reps}\nWeight: {workout.PT_Weight}lbs");

            double totalWeight = PT_CalculateTotalWeight(workout);
            Console.WriteLine($"Accumulated weight lifted: {totalWeight} lbs");

            Console.WriteLine();
        }
    }

    static void PT_SaveWorkoutData()
    {
        using (StreamWriter writer = new StreamWriter(PT_dataFilePath))
        {
            foreach (var workout in PT_workouts)
            {
                string type = (workout is PT_StrengthWorkout) ? "S" : "C";
                writer.WriteLine($"{type},{workout.PT_Exercise},{workout.PT_Sets},{workout.PT_Reps},{workout.PT_Weight},{workout.PT_Date}");
            }
        }
    }

    static void PT_LoadWorkoutData()
    {
        if (File.Exists(PT_dataFilePath))
        {
            string[] lines = File.ReadAllLines(PT_dataFilePath);
            foreach (var line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length >= 6)
                {
                    char typeChar = parts[0][0];
                    string exercise = parts[1];
                    int sets = int.Parse(parts[2]);
                    int reps = int.Parse(parts[3]);
                    double weight = double.Parse(parts[4]);
                    DateTime date = DateTime.Parse(parts[5]);

                    PT_Workout workout;
                    switch (typeChar)
                    {
                        case 'S':
                            workout = new PT_StrengthWorkout();
                            break;
                        case 'C':
                            workout = new PT_CardioWorkout();
                            break;
                        default:
                            workout = new PT_StrengthWorkout();
                            break;
                    }

                    workout.PT_Exercise = exercise;
                    workout.PT_Sets = sets;
                    workout.PT_Reps = reps;
                    workout.PT_Weight = weight;
                    workout.PT_Date = date;

                    PT_workouts.Add(workout);
                }
            }
        }
    }
}
