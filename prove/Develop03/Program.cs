using System;

class Program
{
    static void Main(string[] args)
    {
        // Scripture library: pick one at random each run
        List<Scripture> library = new List<Scripture>
        {
            new Scripture(new Reference("John", 3, 16),
                "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."),
            new Scripture(new Reference("Proverbs", 3, 5, 6),
                "Trust in the LORD with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths."),
            new Scripture(new Reference("Moroni", 10, 4, 5),
                "And when ye shall receive these things, I would exhort you that ye would ask God, the Eternal Father, in the name of Christ, if these things are not true; and if ye shall ask with a sincere heart, with real intent, having faith in Christ, he will manifest the truth of it unto you, by the power of the Holy Ghost. And by the power of the Holy Ghost ye may know the truth of all things."),
            new Scripture(new Reference("Philippians", 4, 13),
                "I can do all things through Christ which strengtheneth me.")
            new Scripture(new Reference("BYUI", 1, 1),
                "Brigham Young University-Idaho was founded and is supported and guided by The Church of Jesus Christ of Latter-day Saints. Its mission is to develop disciples of Jesus Christ who are leaders in their homes, the Church, and their communities. The university does this by: Building testimonies of the restored gospel of Jesus Christ and fostering its principles in a wholesome academic, cultural, and social environment. Providing a high-quality education that prepares students of diverse interests and abilities for lifelong learning and employment. Serving as many students as possible within resource constraints. Delivering education that is affordable for students and the Church.")

The university does this by: 
        };

        Scripture scripture = library[new Random().Next(library.Count)];

        bool running = true;
        while (running && !scripture.IsCompletelyHidden())
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\nPress Enter to continue or type 'quit' to exit.");

            string input = Console.ReadLine() ?? "";

            if (input.Trim().ToLower() == "quit")
            {
                running = false;
            }
            else
            {
                scripture.HideRandomWords(3);
            }
        }

        Console.Clear();
        Console.WriteLine(scripture.GetDisplayText());
    }
}
