using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Class representing an individual bingo plate
public class BankoPlade
{
    // Properties of a bingo plate
    public string? name { get; set; } // Name of the plate
    public int? id { get; set; } // ID of the plate
    public int earnedRows { get; set; } = 0; // Number of rows earned (marked)
    public Row row1 { get; set; } = new(); // First row of the plate
    public Row row2 { get; set; } = new(); // Second row of the plate
    public Row row3 { get; set; } = new(); // Third row of the plate

    // Method to assign an ID to the plate
    public void AssignID(int idNum)
    {
        id = idNum;
    }

    // Method to get the name of the plate
    public string GetName()
    {
        return name;
    }

    // Method to check if the entire plate is completed
    public bool GottenEntirePlate()
    {
        if (row1.lineStatus == true && row2.lineStatus == true && row3.lineStatus == true)
            return true;
        else
            return false;
    }

    // Method to check for errors in the rows of the plate
    public void ErrorCheckingRows()
    {
        int rowOneCounter = row1.ExamineRow();
        int rowTwoCounter = row2.ExamineRow();
        int rowThreeCounter = row3.ExamineRow();

        if (rowOneCounter != 4)
            Console.WriteLine("Error at row 1");
        else if (rowTwoCounter != 4)
            Console.WriteLine("Error at row 2");
        else if (rowThreeCounter != 4)
            Console.WriteLine("Error at row 3");
        else
            Console.WriteLine("We good");
    }

    // Method to check a bingo number on the plate and update status
    public void CheckAndInsertPlateNumber(int bingoNumber, BankoPlade plade)
    {
        int bingoNumberIndex = bingoNumber / 10;
        if (bingoNumberIndex == 9)
            bingoNumberIndex--;

        if (!row1.lineStatus && row1.numbers[bingoNumberIndex] == bingoNumber)
            row1.InsertBingoNumberIntoRow(row1, bingoNumberIndex, 1, plade);
        else if (!row2.lineStatus && row2.numbers[bingoNumberIndex] == bingoNumber)
            row2.InsertBingoNumberIntoRow(row2, bingoNumberIndex, 2, plade);
        else if (!row3.lineStatus && row3.numbers[bingoNumberIndex] == bingoNumber)
            row3.InsertBingoNumberIntoRow(row3, bingoNumberIndex, 3, plade);
    }

    // Method to get a random bingo number
    public int GetBingoNumber()
    {
        Random r = new();
        return r.Next(1, 91);
    }

    // Method to create rows for the bingo plate
    public void CreateRows()
    {
        List<int> usedNumbers = new();
        row1.CreateRow(usedNumbers);
        row2.CreateRow(usedNumbers);
        row3.CreateRow(usedNumbers);
    }

    // Method to print the bingo plate
    public void PrintPlade()
    {
        Console.WriteLine($"{name}:");
        PrintRow(row1);
        PrintRow(row2);
        PrintRow(row3);
    }

    // Helper method to print a row
    private void PrintRow(Row row)
    {
        foreach (int elm in row.numbers)
        {
            string elementStr = elm.ToString();
            Console.Write(elementStr.PadRight(3));
        }
        Console.WriteLine();
    }

    // Method to set the name of the plate
    public void SetName(string passedName)
    {
        name = passedName;
    }
}