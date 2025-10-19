using System.ComponentModel.DataAnnotations.Schema;

class Program
{
    static string NormalizeCsvPath(string path)
{
    // Make absolute and, if a directory is given, append default filename.
    string full = Path.GetFullPath(path);
    if (Directory.Exists(full) || full.EndsWith(Path.DirectorySeparatorChar) || full.EndsWith(Path.AltDirectorySeparatorChar))
    {
        full = Path.Combine(full, "database.csv");
    }
    return full;
}

    static void Main()
    {
        List<string> header = new List<string>();

        Console.WriteLine("Welcome to the CSV File Handler!");
        Console.WriteLine("Please specify the path for the CSV file (or press Enter to get default \"AppkaCSV\\bin\\Debug\\net9.0\\database.csv\"):");
        Console.WriteLine("Please provide the path along with the filename (e.g., C:\\path\\to\\your\\file.csv).");
        string path = Console.ReadLine();

        Console.WriteLine("You will now be prompted to enter the header fields for the CSV file.");
        Console.WriteLine("Enter the first field and press enter to confirm, when done, just press enter on an empty line.");
        Console.WriteLine("Please specify the header fields (providing no fields will close the application):");
        while (true)
        {
            string field = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(field)) { break; }
            header.Add(field);
        }
        if (header.Count == 0)
        {
            Console.WriteLine("No header fields provided. Exiting application.");
            return;
        }

        path = string.IsNullOrWhiteSpace(path) ? "database.csv" : path;
        
        CSVFileHandler csvHandler = new CSVFileHandler(header, path);

        Console.WriteLine($"CSV file created at {path}.");

        int choice = 0;
        Console.WriteLine("Choose action: 1 - Add Record, 2 - Search for a record, 3 - Print all records, 4 - Exit");

        // choice loop
        while (true)
        {
            try
            {
                choice = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid input, please enter a number between 1 and 4.");
                continue;
            }

            if (string.IsNullOrWhiteSpace(choice.ToString())) { break; }

            switch (choice)
            {
                case 1:
                    // Add Record
                    List<string> record = new List<string>();
                    Console.WriteLine("Enter the record fields one by one:");
                    for (int i = 0; i < header.Count; i++)
                    {
                        Console.WriteLine($"Enter value for {header[i]}:");
                        string value = Console.ReadLine();
                        record.Add(value);
                    }
                    try {
                        csvHandler.AddRecord(record);
                    }
                    catch (ArgumentException ex) {
                        Console.WriteLine("Error adding record: " + ex.Message);
                        break;
                    }
                    Console.WriteLine("Record added successfully.");
                    break;

                case 2:
                    // Search for a record
                    Console.WriteLine("\nEnter the search term:");
                    string searchTerm = Console.ReadLine();
                    csvHandler.SearchRecord(searchTerm);
                    break;

                case 3:
                    // Print all records
                    csvHandler.PrintAllRecords();
                    break;

                case 4:
                    // Exit
                    Console.WriteLine($"Exiting application. You can find your CSV database at {path}. \nGoodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid choice, please enter a number between 1 and 4.");
                    break;
            }

            Console.WriteLine("Choose action: 1 - Add Record, 2 - Search for a record, 3 - Print all records, 4 - Exit");
        }

        return;
    }
}
