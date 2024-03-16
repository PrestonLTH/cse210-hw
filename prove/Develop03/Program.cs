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
            new Scripture("3 Nephi 115:9", "Behold, I am the law, and the light. Look unto me, and endure to the end, and ye shall live; for unto him that endureth to the end will I give eternal life."),
            new Scripture("2 Nephi 1:23", "Awake, my sons; put on the armor of righteousness. Shake off the chains with which ye are bound, and come forth out of obscurity, and arise from the dust."),
            new Scripture("Psalm 34:13-15", "Keep they tongue from evil, and thy lips from speaking guile. Depart from evil, and do good; seek peace and pursue it. The eyes of the Lord are upon the righteous, and his ears are open unto their cry."),
            new Scripture("Matthew 11:6", "And blessed is he, whosoever shall not be offended in me."),
        };

        Random rand = new Random();

        do
        {
            // Randomly choose a scripture
            var currentScripture = scriptures[rand.Next(scriptures.Length)];

            // Display the complete scripture
            DisplayScripture(currentScripture);

            // Prompt the user to press enter or type quit
            Console.WriteLine("Press Enter to reveal a word or type 'quit' to exit:");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
                return;

            HideRandomWord(currentScripture);
        } while (true);
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