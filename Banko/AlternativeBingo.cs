using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Class representing the alternative bingo game
public class AlternativeBingo : BankoPlade
{
    Hashtable bingoPlates = new(); // Hashtable to store bingo plates by name
    readonly List<int> usedBingoNumbers = new(); // List to keep track of used bingo numbers

    // Method to start the bingo game
    public void StartGame()
    {
        Console.WriteLine("Welcome to Bingo!");

        string input = "";
        while (true)
        {
            Console.Write("Please enter action\n" +
                "   make\n" +
                "   view one\n" +
                "   view all\n" +
                "   insert\n" +
                ">");
            input = Console.ReadLine();

            if (input == "make")
            {
                Console.Write("Enter name>");
                CreatePlate(name: Console.ReadLine());
            }
            else if (input == "view one")
            {
                Console.Write("Enter plate name>");
                try
                {
                    PrintPlate(name: Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error caught: " + ex.Message);
                }
            }
            else if (input == "view all")
            {
                Console.WriteLine("All your sexy plates:");
                PrintAllPlates();
            }
            else if (input == "insert")
            {
                Console.Write("Enter number>");
                try
                {
                    int number = ConvertBingoNumberToInt(Console.ReadLine());
                    InsertBingoNumber(number);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error caught: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
    }

    // Method to print all bingo plates
    private void PrintAllPlates()
    {
        BankoPlade plade = new();
        foreach (DictionaryEntry entry in bingoPlates)
        {
            plade = (BankoPlade)entry.Value;
            plade.PrintPlade();
        }
    }

    // Method to create a new bingo plate
    private void CreatePlate(string name)
    {
        BankoPlade tempPlate = new();
        tempPlate.CreateRows();
        tempPlate.SetName(name);
        bingoPlates[name] = tempPlate;
    }

    // Method to print a specific bingo plate by name
    private void PrintPlate(string name)
    {
        BankoPlade tempPlate = new();
        tempPlate = (BankoPlade)bingoPlates[name];
        tempPlate.PrintPlade();
    }

    // Method to convert bingo number from string to integer
    private int ConvertBingoNumberToInt(string strNum)
    {
        return Convert.ToInt32(strNum);
    }

    // Method to insert a bingo number and check for winners
    private void InsertBingoNumber(int bingoNumber)
    {
        if (bingoNumber < 1 || bingoNumber > 90)
        {
            Console.WriteLine("Number is either less than 1, or more than 90.");
            return;
        }
        else if (usedBingoNumbers.Contains(bingoNumber))
        {
            Console.WriteLine($"Number {bingoNumber} has already been said.");
            return;
        }
        else
        {
            bool plateVictory = false;
            string winnerName = "";
            BankoPlade winnerPlate = new();
            usedBingoNumbers.Add(bingoNumber);
            foreach (DictionaryEntry entry in bingoPlates)
            {
                BankoPlade plade = (BankoPlade)entry.Value;
                plade.CheckAndInsertPlateNumber(bingoNumber, plade);

                if (plade.GottenEntirePlate())
                {
                    plateVictory = true;
                    winnerName = plade.GetName();
                    winnerPlate = plade;
                    break;
                }
            }

            if (plateVictory)
            {
                Console.WriteLine(winnerName + " Has won!");
                winnerPlate.PrintPlade();
            }
        }
    }
}
