namespace LocationVoiture.Presentation;

public static class ConsoleAccess
{
    public static void CreateScreen(string title)
    {
        Console.Clear();
        
        const int width = 50;
        string border = new string('*', width);
        
        int padding = width - 2 - title.Length; 
        int paddingLeft = padding / 2;
        int paddingRight = padding - paddingLeft;
        
        string titleLine = $"*{new string(' ', paddingLeft)}{title}{new string(' ', paddingRight)}*";
        
        Console.WriteLine(border);
        Console.WriteLine(titleLine);
        Console.WriteLine(border);
    }
    
    public static T ReadInput<T>(string message)
    {
        Console.Write(message);
        
        string input = Console.ReadLine()!;
        
        try
        {
            return (T)Convert.ChangeType(input, typeof(T));
        }
        catch
        {
            Console.WriteLine($"Invalid entry. Please enter a value with the following type : {typeof(T).Name}.");
        }

        return default; //(renvoyer la valeur par défaut du type générique)
    }

    public static void Wait()
    {
        Console.WriteLine("Press enter ton continue...");
        Console.ReadKey();
        Console.WriteLine("\n");
    }
}