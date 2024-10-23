using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Class representing an individual bingo plate
public class BingoPlate
{
    public string? name { get; set; }
    public int? id { get; set; }
    public int completedRows { get; set; } = 0;
    public Row row1 { get; set; } = new();
    public Row row2 { get; set; } = new();
    public Row row3 { get; set; } = new();

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
        if (row1.rowCompletion == true && row2.rowCompletion == true && row3.rowCompletion == true)
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
    public void CheckAndInsertPlateNumber(int bingoNumber, BingoPlate plade)
    {
        // By dividing the bingo number by 10 as integers, we get the corresponing index.
        // Except for the bingo number "90", that gives us 9, which is out of bound, so we minus 1.
        int bingoNumberIndex = bingoNumber / 10;
        if (bingoNumberIndex == 9)
            bingoNumberIndex--;

        if (!row1.rowCompletion && row1.rowNumbers[bingoNumberIndex] == bingoNumber)
            row1.InsertBingoNumberIntoRow(row1, bingoNumberIndex, 1, plade);

        else if (!row2.rowCompletion && row2.rowNumbers[bingoNumberIndex] == bingoNumber)
            row2.InsertBingoNumberIntoRow(row2, bingoNumberIndex, 2, plade);

        else if (!row3.rowCompletion && row3.rowNumbers[bingoNumberIndex] == bingoNumber)
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
    public void PrintPlate()
    {
        Console.WriteLine($"{name}:");
        PrintRow(row1);
        PrintRow(row2);
        PrintRow(row3);
    }

    // Helper method to print a row
    private void PrintRow(Row row)
    {
        foreach (int elm in row.rowNumbers)
        {
            // We pad by 3 to make it look more nicer.
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