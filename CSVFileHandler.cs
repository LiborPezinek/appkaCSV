// write a record into a csv based on user input
// search for a record in a csv file based on user input
// print the whole csv file content to console


class CSVFileHandler
{
    private readonly string _path;
    private readonly List<string> _header;

    public CSVFileHandler(List<string> header, string path = "database.csv")
    {
        _path = path;
        _header = header;

        ClearFile();
    }

    // Clears (or creates if doesnt exist) the CSV file and writes a header
    private void ClearFile()
    {
        foreach (string head in _header)
        {
            File.WriteAllText(_path, _header + Environment.NewLine + Environment.NewLine);
        }
    }

    // Adds a record to the CSV file
    public void AddRecord(List<string> record)
    {
        if (record.Count != _header.Count)
        {
            throw new ArgumentException("Record does not match header length.");
        }
        foreach (string field in record)
        {
            if (string.IsNullOrWhiteSpace(field))
            {
                throw new ArgumentException("Record fields cannot be empty.");
            }
        }
        // takes all fields and joins them into one string, divided by a semicolon
        string line = string.Join(";", record);
        File.AppendAllText(_path, line);
    }

    public void PrintAllRecords()
    {
        string[] lines = File.ReadAllLines(_path);
        foreach (var line in lines)
        {
            Console.WriteLine(line);
        }
    }

    public bool SearchRecord(string name)
    {
        string[] lines = File.ReadAllLines(_path);
        foreach (var line in lines)
        {
            if (line.StartsWith(name + ";"))
            {
                Console.WriteLine("Record found: " + line);
                return true;
            }
        }
        Console.WriteLine("Record not found.");
        return false;
    }
}    
