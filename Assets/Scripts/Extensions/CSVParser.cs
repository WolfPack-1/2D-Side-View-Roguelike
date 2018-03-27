using UnityEngine;
using System.Collections;
using System.Linq; 
 
public class CSVReader : MonoBehaviour 
{
    public TextAsset csvFile; 
    public void Start()
    {
        string[,] grid = SplitCsvGrid(csvFile.text);
        Debug.Log("size = " + (1+ grid.GetUpperBound(0)) + "," + (1 + grid.GetUpperBound(1))); 
 
        DebugOutputGrid(grid); 
    }
 
    public static void DebugOutputGrid(string[,] grid)
    {
        string textOutput = ""; 
        for (int y = 0; y < grid.GetUpperBound(1); y++) {	
            for (int x = 0; x < grid.GetUpperBound(0); x++) {
 
                textOutput += grid[x,y]; 
                textOutput += "|"; 
            }
            textOutput += "\n"; 
        }
        Debug.Log(textOutput);
    }
 
    public static string[,] SplitCsvGrid(string csvText)
    {
        string[] lines = csvText.Split("\n"[0]); 
 
        int width = 0; 
        for (int i = 0; i < lines.Length; i++)
        {
            string[] row = SplitCsvLine( lines[i] ); 
            width = Mathf.Max(width, row.Length); 
        }
 
        string[,] outputGrid = new string[width + 1, lines.Length + 1]; 
        for (int y = 0; y < lines.Length; y++)
        {
            string[] row = SplitCsvLine( lines[y] ); 
            for (int x = 0; x < row.Length; x++) 
            {
                outputGrid[x,y] = row[x]; 
                outputGrid[x,y] = outputGrid[x,y].Replace("\"\"", "\"");
            }
        }
 
        return outputGrid; 
    }
 
    public static string[] SplitCsvLine(string line)
    {
        return (from System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(line,
                @"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)", 
                System.Text.RegularExpressions.RegexOptions.ExplicitCapture)
            select m.Groups[1].Value).ToArray();
    }
}