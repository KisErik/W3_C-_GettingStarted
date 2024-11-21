// See https://aka.ms/new-console-template for more information

using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        string url = "https://adventofcode.com/2023/day/1/input";
        
        /*
         Open your browser and log in to the Advent of Code website.
        Open the developer tools (usually by pressing F12 or Ctrl+Shift+I).
        Go to the "Application" or "Storage" tab.
        Find the "Cookies" section and locate the cookie named session.
        Copy the value of the session cookie.
        Replace "your-session-cookie-value" with the copied session cookie value in the code above.
         */
        
        string sessionCookieValue = "your-session-cookie-value"; // Replace with your actual session cookie value

        using HttpClient client = new HttpClient();

        // Add the session cookie to the request headers
        client.DefaultRequestHeaders.Add("Cookie", $"session={sessionCookieValue}");

        try
        {
            string result = await client.GetStringAsync(url);
            string[] lines = result.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            
            int totalSum = 0;
            
            foreach (string line in lines)
            {
                char firstDigit = '\0';
                char lastDigit = '\0';
                
                for (int i = 0; i < line.Length; i++ )
                {
                    if (Char.IsDigit(line[i]) && firstDigit == '\0')
                    {
                        firstDigit = line[i];
                    }
                    
                    if (Char.IsDigit(line[line.Length - i - 1]) && lastDigit == '\0')
                    {
                        lastDigit = line[line.Length - i - 1];
                    }

                    if (firstDigit != '\0' && lastDigit != '\0')
                    { 
                        //Console.Write(firstDigit.ToString() + lastDigit.ToString());
                        break;
                    }
                }

                if (firstDigit != '\0' && lastDigit != '\0')
                {
                    int calibrationValue = int.Parse(firstDigit.ToString() + lastDigit.ToString());
                    totalSum += calibrationValue;
                }
            }
            Console.WriteLine($"Total sum: {totalSum}");
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Request error: " + e.Message);
        }
    }
}

