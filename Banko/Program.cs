using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.IO;
using System.Collections;

class Program
{
    static void Main()
    {
        AlternativeBingo test = new();
        test.StartGame();
    }
}