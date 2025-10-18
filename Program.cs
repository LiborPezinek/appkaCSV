class Program
{
    static void Main()
    {
        string path = "worker_database.csv";

        // Clear the file
        File.WriteAllText(path, "Name;Age" + Environment.NewLine + Environment.NewLine);

        while (true)
        {
            Console.Write("Enter name (leave empty to finish): ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) break;

            Console.Write("Enter age (leave empty to finish): ");
            string age = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(age)) break;

            string line = $"{name};{age}";
            File.AppendAllText(path, line + Environment.NewLine);
            Console.WriteLine("Record saved!");
        }

        return;
    }
}
