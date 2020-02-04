using System;
using System.IO;

namespace SleepData
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // ask for input
            Console.WriteLine("Enter 1 to create data file.");
            Console.WriteLine("Enter 2 to parse data.");
            Console.WriteLine("Enter anything else to quit.");
            // input response
            string resp = Console.ReadLine();

            // specify path for data file
            string file = AppDomain.CurrentDomain.BaseDirectory + "data.txt";

            if (resp == "1")
            {
                // create data file

                // ask a question
                Console.WriteLine("How many weeks of data is needed?");
                // input the response (convert to int)
                int weeks = int.Parse(Console.ReadLine());

                // determine start and end date
                DateTime today = DateTime.Now;
                // we want full weeks sunday - saturday
                DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
                // subtract # of weeks from endDate to get startDate
                DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));

                // random number generator
                Random rnd = new Random();

                // create file
                StreamWriter sw = new StreamWriter(file);
                // loop for the desired # of weeks
                while (dataDate < dataEndDate)
                {
                    // 7 days in a week
                    int[] hours = new int[7];
                    for (int i = 0; i < hours.Length; i++)
                    {
                        // generate random number of hours slept between 4-12 (inclusive)
                        hours[i] = rnd.Next(4, 13);
                    }
                    // M/d/yyyy,#|#|#|#|#|#|#
                    //Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
                    sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
                    // add 1 week to date
                    dataDate = dataDate.AddDays(7);
                }
                sw.Close();
            }
            else if (resp == "2")
            {
                // TODO: parse data file
                Console.Clear();

                //make arrays of weekdays and dashes underneath.
                string[] weekDay = new string[7] { "Su", "Mo", "Tu", "We", "Th" ,"Fr","Sa",};
                string[] dashes = new string[7] { "--", "--", "--", "--", "--", "--", "--",};

                //open up file for reading.
                StreamReader reader1 = new StreamReader(file);

                //start loop to read file line by line whilst spliting the line into two arrays based on dual delimiters ',' and '|'.
                while (!reader1.EndOfStream)
                {
                    string line1 = reader1.ReadLine();
                    string[] line1Array1 = line1.Split(',');
                    string[] line1Array2 = line1Array1[1].Split('|');
                    int[] hour = Array.ConvertAll(line1Array2, int.Parse);

                    //Convert the date in the file to a datetime type.
                    DateTime date = Convert.ToDateTime(line1Array1[0]);

                    //print out date with correct formating with space to match powerpoint
                    Console.WriteLine($"\nWeek of {date:MMM}, {date:dd}, {date:yyyy}");

                    //print out headers for hours using foreach loops
                    foreach (var item in weekDay)
                    {
                        Console.Write($"{item,3}");
                    }
                    Console.Write(" Tot Avg");
                    Console.WriteLine();
                    foreach (var item in dashes)
                    {
                        Console.Write($"{item,3}");
                    }
                    Console.Write(" --- ---");
                    Console.WriteLine();

                    //sum up the elements in the hour array
                    int hoursum = 0;

                    foreach (int item in hour)
                    {
                        hoursum += item;
                    }

                    //print out hours from file using a foreach loop
                    foreach (var item in hour)
                    {
                        Console.Write($"{item,3}");
                    }

                    //print out tot and avg 
                    Console.Write($" {hoursum}  {hoursum / 7.0:N1}\n\n");
                    

                }


            }
        }
    }
}
