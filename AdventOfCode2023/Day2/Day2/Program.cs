// See https://aka.ms/new-console-template for more information

using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        string url = "https://adventofcode.com/2023/day/2/input";
        
        /*
         Open your browser and log in to the Advent of Code website.
        Open the developer tools (usually by pressing F12 or Ctrl+Shift+I).
        Go to the "Application" or "Storage" tab.
        Find the "Cookies" section and locate the cookie named session.
        Copy the value of the session cookie.
        Replace "your-session-cookie-value" with the copied session cookie value in the code above.
         */
        
        string sessionCookieValue = "53616c7465645f5fd05d68c5a92dc4a048b51d68d654aa7b681ee39e1464f8513f1d81ceaaf59b3e4b8970403f3e41ec54d77a08622d54cc7ab71d1b6ae60a86"; // Replace with your actual session cookie value

        using HttpClient client = new HttpClient();

        // Add the session cookie to the request headers
        client.DefaultRequestHeaders.Add("Cookie", $"session={sessionCookieValue}");

        try
        {
            string result = await client.GetStringAsync(url);
            string[] lines = result.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            
            int[] bag = { 12, 13, 14 };
            int sumOfPossibleGame = 0;
            
            foreach (string line in lines)
            {
                if (IsGamePossible(line, bag))
                {
                    int gameId = int.Parse(Regex.Match(line, @"\d+").Value);
                    sumOfPossibleGame += gameId;
                }
                
            }
            Console.WriteLine("Sum of possible game IDs: " + sumOfPossibleGame);
        }
        
        catch (HttpRequestException e)
        {
            Console.WriteLine("Request error: " + e.Message);
        }
    }

    static bool IsGamePossible(string line, int[] bag) 
    {
        Dictionary<string, int> colors = new Dictionary<string, int> 
        { 
            { "red", 0 }, 
            { "green", 1 }, 
            { "blue", 2 } 
        };
    
        // Split the input line properly and check for correctness
        string[] parts = line.Split(":");
        if (parts.Length < 2) 
            return false;
    
        string[] sets = parts[1].Split(";");
    
        foreach (string set in sets) 
        {
            string[] cubes = set.Split(",");
            int[] maxCubes = new int[3];
        
            foreach (string cube in cubes) 
            {
                Match match = Regex.Match(cube.Trim(), @"(\d+)\s+(\w+)");
                if (!match.Success) 
                    continue;
            
                int count = int.Parse(match.Groups[1].Value);
                string color = match.Groups[2].Value;
            
                if (colors.ContainsKey(color)) 
                {
                    maxCubes[colors[color]] += count;
                    if (maxCubes[colors[color]] > bag[colors[color]]) 
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }



}
