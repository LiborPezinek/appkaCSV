// write a record into a csv based on user input
// search for a record in a csv file based on user input
// print the whole csv file content to console


class CSVFileHandler
{
    private readonly string _path;
    private readonly List<string> _header;

    // Constructor
    public CSVFileHandler(List<string> header, string path)
    {
        _path = path;
        _header = header;

        WriteHeader();
    }

    // Clears (or creates if doesn't exist) the CSV file and writes a header
    private void WriteHeader()
    {
        // Truncate the file if it exists, or create a new one
        File.WriteAllText(_path, "");

        // takes all fields and joins them into one string, divided by a semicolon (proper CSV format), writes to file
        string line = string.Join(";", _header);
        File.AppendAllText(_path, line + Environment.NewLine + Environment.NewLine);
    }

    // Adds a record to the CSV file
    public void AddRecord(List<string> record)
    {
        // check for empty fields
        foreach (string field in record)
        {
            if (string.IsNullOrWhiteSpace(field)) {
                throw new ArgumentException("Record fields cannot be empty.");
            }
        }

        // takes all fields and joins them into one string, divided by a semicolon (proper CSV format)
        string line = string.Join(";", record);
        File.AppendAllText(_path, line + Environment.NewLine);
    }

    // Searches for a record by name in the CSV file
    public void SearchRecord(string searchTerm)
    {
        string[] lines = File.ReadAllLines(_path);
        bool found = false;
        foreach (var line in lines)
        {
            if (line.Contains(searchTerm))
            {
                Console.WriteLine("Record found: " + line);
                found = true;
            }
        }
        if (!found)
        {
            Console.WriteLine("Record not found.");
        }
        Console.WriteLine(); // extra line for better readability
    }

    // print all records of the CSV file to the console (at least the header)
    public void PrintAllRecords()
    {
        Console.WriteLine("\nCSV File Content:");

        string[] lines = File.ReadAllLines(_path);
        foreach (var line in lines)
        {
            Console.WriteLine(line);
        }
        Console.WriteLine(); // extra line for better readability
    }
}    
