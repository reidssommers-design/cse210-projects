using System;

class Program
{
    static void Main(string[] args)
    {
        Random randomGenerator = new Random();
        int magicNum = randomGenerator.Next(1, 100);
        
        // Console.WriteLine("What is the magic number?");
        // int magicNum = int.Parse(Console.ReadLine());

        int guessNum;
        int tries = 0;
        do
        {
            tries ++;
            Console.WriteLine("What is your guess?");
            guessNum = int.Parse(Console.ReadLine());

            if (guessNum > magicNum)
            {
                Console.WriteLine("Lower");
            }
            else if (guessNum < magicNum)
            {
                Console.WriteLine("Higher");
            }
        } while (guessNum != magicNum);

        Console.WriteLine($"You guessed it! It only took you {tries} guesses!");
    }
}