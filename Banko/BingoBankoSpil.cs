using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

// Class representing a basic bingo game
public class BingoBankoSpil
{
    BankoPlade[] bankoPlader; // Array of bingo plates
    readonly List<int> usedBingoNumbers = new(); // List to keep track of used bingo numbers

    /// <summary>
    /// Constructor for BingoBankoSpil.
    /// Takes the amount of bingo plates as parameter.
    /// </summary>
    /// <param name="initSize"></param>
    public BingoBankoSpil(int initSize)
    {
        bankoPlader = new BankoPlade[initSize];

        for (int i = 0; i < bankoPlader.Length; i++)
        {
            bankoPlader[i] = new BankoPlade();
            bankoPlader[i].AssignID(i);
            bankoPlader[i].SetName($"{i} of {i}");
            bankoPlader[i].CreateRows();
        }
    }

    // Method to simulate the game and determine the winner
    public void ResolvePlates()
    {
        bool plateVictory = false;
        string winnerName = "";
        BankoPlade winnerPlate = new();
        for (int i = 0; i < 90; i++)
        {
            int bingoNumber = GetBingoNumber();
            foreach (BankoPlade plade in bankoPlader)
            {
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
                Console.WriteLine(i + " in, and " + winnerName + " Has won!");
                winnerPlate.PrintPlade();
                break;
            }
        }
    }

    // Method to get a random bingo number
    private int GetBingoNumber()
    {
        Random randCaller = new Random();
        while (true)
        {
            int bingoNumber = randCaller.Next(1, 91);
            if (usedBingoNumbers.Contains(bingoNumber))
            {
                continue;
            }
            else
            {
                usedBingoNumbers.Add(bingoNumber);
                return bingoNumber;
            }
        }
    }

    // Method to print a bingo plate by name
    public void PrintByName(string name)
    {
        foreach (BankoPlade plade in bankoPlader)
        {
            if (plade.GetName() == name)
            {
                plade.PrintPlade();
                return;
            }
        }
        Console.WriteLine("Could not find plate based on name.");
    }

    // Method to export bingo plates to a JSON file
    public void ExportPlatesToJSON(string filePath)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string jsonString = JsonSerializer.Serialize(bankoPlader, options);
            File.WriteAllText(filePath, jsonString);
            Console.WriteLine($"Successfully exported to {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Method to check for errors in bingo plates
    public void ErrorCheckingPlates()
    {
        foreach (var plade in bankoPlader)
            plade.ErrorCheckingRows();
    }



    // Method to print all bingo plates in the game
    public void PrintGame()
    {
        foreach (BankoPlade plade in bankoPlader)
            plade.PrintPlade();
    }

    // Method to print a bingo plate by ID
    public void PrintById(int idNum)
    {
        foreach (BankoPlade plade in bankoPlader)
        {
            if (plade.id == idNum)
                plade.PrintPlade();
        }
    }
}
