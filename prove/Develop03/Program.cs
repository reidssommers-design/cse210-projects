using System;

class Program
{
    static void Main(string[] args)
    {
        string trythis = "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.";
        Console.WriteLine(trythis);
        
        List<string> words = new List<string>();


        string word = "";
        foreach (char i in trythis)
        {
            if (i == ' ' || i == '.')
            {
                // Console.WriteLine(word);
                words.Add(word);
                word = "";
            }
            else 
            {
                word = word + i;
            }
        }


        foreach (string i in words)
        {
            Console.WriteLine(i);
        }
    }
}