namespace LocationVoiture.Presentation;

public static class AccesConsole
{
    public static void CreateScreen(string message)
    {
        Console.Clear();
        Console.WriteLine(message);
    }

    public static T ReadInput<T>(string message)
    {
        Console.WriteLine(message);
        
        string input = Console.ReadLine()!;
        
        try
        {
            return (T)Convert.ChangeType(input, typeof(T));
        }
        catch
        {
            Console.WriteLine($"Entrée invalide. Veuillez entrer une valeur de type {typeof(T).Name}.");
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