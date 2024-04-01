using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Create an array of Scripture objects
        var scriptures = new Scripture[]
        {
            new Scripture("John 3:16", "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."),
            new Scripture("Matthew 6:33", "But seek ye first the kingdom of God, and his righteousness; and all these things shall be added unto you."),
            new Scripture("Psalm 23:1", "The Lord is my shepherd; I shall not want."),
            new Scripture("Proverbs 12:24", "The hand of the diligent shall bear rule: but the slothful shall be under tribute."),
        };

        Random rand = new Random();

        // Randomly choose a scripture
        var currentScripture = scriptures[rand.Next(scriptures.Length)];

        do
        {
            // Display the complete scripture
            DisplayScripture(currentScripture);

            // Prompt the user to press enter or type quit
            Console.WriteLine("Press Enter to reveal a word or type 'quit' to exit:");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
                return;

            HideRandomWord(currentScripture);
        } while (!currentScripture.AllWordsHidden());
    }

    static void DisplayScripture(Scripture scripture)
    {
        Console.WriteLine(scripture.GetReference());
        Console.WriteLine(scripture.GetHiddenText());
    }

    static void HideRandomWord(Scripture scripture)
    {
        Random rand = new Random();
        List<Word> words = scripture.GetWords().Where(word => !word.IsHidden()).ToList();
        int index = rand.Next(0, words.Count);
        words[index].Hide();
    }
}

class Scripture
{
    private string reference;
    private string text;
    private List<Word> words;

    public Scripture(string reference, string text)
    {
        this.reference = reference;
        this.text = text;
        InitializeWords();
    }

    public string GetReference()
    {
        return reference;
    }

    public string GetHiddenText()
    {
        string hiddenText = text;
        foreach (Word word in words)
        {
            if (word.IsHidden())
            {
                hiddenText = hiddenText.Replace(word.GetText(), new string('_', word.GetText().Length));
            }
        }
        return hiddenText;
    }

    public List<Word> GetWords()
    {
        return words;
    }

    public bool AllWordsHidden()
    {
        return words.All(word => word.IsHidden());
    }

    private void InitializeWords()
    {
        words = new List<Word>();
        string[] tokens = text.Split(' ');
        foreach (string token in tokens)
        {
            words.Add(new Word(token));
        }
    }
}

class Word
{
    private string text;
    private bool hidden;

    public Word(string text)
    {
        this.text = text;
        this.hidden = false;
    }

    public string GetText()
    {
        return text;
    }

    public bool IsHidden()
    {
        return hidden;
    }

    public void Hide()
    {
        hidden = true;
    }
}