using System;
using static Program;

public class Row
{
    // Properties of a row
    public int[] rowNumbers { get; set; } = new int[9];
    public int fullRows { get; set; } = 0;
    public bool rowCompletion { get; set; } = false;

    // Method to create a row with random numbers
    public void CreateRow(List<int> usedNumbers)
    {
        int[] indexes = GetIndexies(); // Where the numbers will be placed.
        rowNumbers = GetNumbersForRow(indexes, usedNumbers); // What numbers will be placed.
    }

    // Method to examine the row for errors (number of empty spots)
    public int ExamineRow()
    {
        int zeroCounter = 0;
        foreach (int number in rowNumbers)
        {
            if (number == 0)
                zeroCounter++;
        }
        return zeroCounter;
    }

    // Method to insert a bingo number into the row
    public void InsertBingoNumberIntoRow(Row row, int bingoNumberIndex, int rowNum, BingoPlate plade)
    {
        row.rowNumbers[bingoNumberIndex] = 99;
        row.fullRows++;
        if (row.fullRows == 5)
        {
            row.rowCompletion = true;
            row.fullRows++;
            plade.completedRows++;
            if (plade.completedRows < 3)
                Console.WriteLine($"You got row {rowNum}!");
        }
        //Console.WriteLine("Row1" + Row1.points);
        //Console.WriteLine(bingoNumber);
        //PrintPlade();

    }

    // Helper method to get numbers for the row
    private int[] GetNumbersForRow(int[] indexes, List<int> usedNumbers)
    {
        int number;
        Random r = new();
        int[] row = new int[9];

        foreach (int index in indexes)
        {
            do
            {
                // For the last column of a Bingo Board, we use different code, because we also need to include 90.
                if (index == 8)
                {
                    int adder = 10 * index;
                    number = r.Next(1, 11) + adder;
                }
                else
                {
                    int adder = 10 * index;
                    number = r.Next(1, 10) + adder;
                }

            }
            while (CheckIfDuplicateNumber(usedNumbers, number));

            usedNumbers.Add(number);
            row[index] = number;
        }

        return row;
    }

    // Helper method to check for duplicate numbers
    private bool CheckIfDuplicateNumber(List<int> usedNumbers, int number)
    {
        if (usedNumbers.Contains(number))
            return true;
        else
            return false;
    }

    // Helper method to get indexes for number placement in the row
    private int[] GetIndexies()
    {
        int[] indexes = new int[5]; // Indexes where our values will be placed.
        List<int> availabileIndexes = new() { 0, 1, 2, 3, 4, 5, 6, 7, 8 }; // List, so that we can change the size.

        Random r = new Random();
        for (int i = 0; i < 5; i++)
        {
            int j = r.Next(0, availabileIndexes.Count);
            indexes[i] = availabileIndexes[j];
            availabileIndexes.RemoveAt(j);
        }

        return indexes;
    }
}