using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

// Class representing a basic bingo game
public class BingoBankoSpil
{
    BingoPlate[] bingoPlates; // Array of bingo plates
    readonly List<int> usedBingoNumbers = new(); // List to keep track of used bingo numbers

    /// <summary>
    /// Constructor for BingoBankoSpil.
    /// Takes the amount of bingo plates as parameter.
    /// </summary>
    /// <param name="initSize"></param>
    public BingoBankoSpil(int initSize)
    {
        bingoPlates = new BingoPlate[initSize];

        for (int i = 0; i < bingoPlates.Length; i++)
        {
            bingoPlates[i] = new BingoPlate();
            bingoPlates[i].AssignID(i);
            bingoPlates[i].SetName($"{i} of {i}");
            bingoPlates[i].CreateRows();
        }
    }

    // Method to simulate the game and determine the winner
    public void ResolvePlates()
    {
        bool plateVictory = false;
        string winnerName = "";
        BingoPlate winnerPlate = new();
        for (int i = 0; i < 90; i++)
        {
            int bingoNumber = GetBingoNumber();
            foreach (BingoPlate plate in bingoPlates)
            {
                plate.CheckAndInsertPlateNumber(bingoNumber, plate);

                if (plate.GottenEntirePlate())
                {
                    plateVictory = true;
                    winnerName = plate.GetName();
                    winnerPlate = plate;
                    break;
                }
            }

            if (plateVictory)
            {
                Console.WriteLine(i + " in, and " + winnerName + " Has won!");
                winnerPlate.PrintPlate();
                break;
            }
        }
    }

    // Method to get a random bingo number
    private int GetBingoNumber()
    {
        Random r = new Random();
        while (true)
        {
            int bingoNumber = r.Next(1, 91);
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
        foreach (BingoPlate plate in bingoPlates)
        {
            if (plate.GetName() == name)
            {
                plate.PrintPlate();
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

            string jsonString = JsonSerializer.Serialize(bingoPlates, options);
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
        foreach (BingoPlate plate in bingoPlates)
            plate.ErrorCheckingRows();
    }



    // Method to print all bingo plates in the game
    public void PrintGame()
    {
        foreach (BingoPlate plate in bingoPlates)
            plate.PrintPlate();
    }

    // Method to print a bingo plate by ID
    public void PrintById(int idNum)
    {
        foreach (BingoPlate plate in bingoPlates)
        {
            if (plate.id == idNum)
                plate.PrintPlate();
        }
    }
}
