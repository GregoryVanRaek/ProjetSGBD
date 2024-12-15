﻿namespace LocationVoiture.Presentation;

public static class ValueControl
{
    public static string CheckString(string value, string fieldName)
    {
        do
        {
            value = ConsoleAccess.ReadInput<string>($"{fieldName} ");
            if(string.IsNullOrEmpty(value.Trim()))
                Console.WriteLine($"{fieldName} is required");
        } while (string.IsNullOrEmpty(value.Trim()));

        return value.Trim().ToLower();
    }

    public static int CheckPositiveInt(string message)
    {
        int value = 0;
        do
        {
            value = ConsoleAccess.ReadInput<int>($"{message}");
            if(value <= 0)
                Console.WriteLine("The value must be positive");
        } while (value <= 0);

        return value;
    }
    
    public static decimal CheckPositiveDecimal(string message)
    {
        decimal value = 0;
        do
        {
            value = ConsoleAccess.ReadInput<decimal>($"{message}");
            if(value <= 0)
                Console.WriteLine("The value must be positive");
        } while (value <= 0);

        return value;
    }
    
    public static DateTime CheckDate(string message)
    {
        DateTime dateValue;
        bool incorrect = true;
        
        do
        {
            string input = ConsoleAccess.ReadInput<string>($"{message}");
            if (!DateTime.TryParse(input, out dateValue))
                Console.WriteLine("Invalid date format. Please enter a valid date (e.g. YYYY-MM-DD).");
            else
                incorrect = false;
        } while (incorrect);

        return dateValue;
    }
    
    
    
}