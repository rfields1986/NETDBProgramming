using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NLog;
namespace Week3Assign
{
    class Program
    {
        public static NLog.Logger nlog = NLog.LogManager.GetCurrentClassLogger();
 
        

        static void Main(string[] args)
        {

            nlog.Info("Program started");
            string file = "movies.csv";
            int exit = 0, input;

            
            List<int> movieID = new List<int>();
            List<string> movieTitle = new List<string>();
            List<string> movieGenre = new List<string>();
            

            //program loop begins
            do
            {
                
                try
                {  


                    //Read file and fill lists
                    
                    StreamReader fileReader = new StreamReader(file);
                    nlog.Info("Opened Streamreader");
                    string line0 = fileReader.ReadLine();
                    string[] header = line0.Split(',');

                    while (!fileReader.EndOfStream)
                    {

                        string lines = fileReader.ReadLine();
                        int quoteMark = lines.IndexOf('"');

                        // if there is not quote in the line then the details have no comma and the index is returned as -1
                        if (quoteMark == -1)
                        {
                            //fill the lists from the array 
                            string[] movieInfo = lines.Split(',');
                            movieID.Add(Int32.Parse(movieInfo[0]));
                            movieTitle.Add(movieInfo[1]);
                            //using replace method to get rid of the pipe keys
                            movieGenre.Add(movieInfo[2].Replace('|', ','));

                        }
                        else
                        {
                            //if the index is anything other than -1 then there is a quote in the info
                            //use substring to extract the info into peices without quotation marks and fill into lists.
                            movieID.Add(Int32.Parse(lines.Substring(0, quoteMark - 1)));
                            lines = lines.Substring(quoteMark + 1);
                            quoteMark = lines.IndexOf('"');
                            movieTitle.Add(lines.Substring(0, quoteMark));
                            lines = lines.Substring(quoteMark + 2);
                            movieGenre.Add(lines.Replace('|', ','));

                        }

                    }
                    fileReader.Close();
                    nlog.Info("Closed Streamreader");
                }
                catch (Exception execpt)
                {
                    nlog.Error(execpt.Message);
                }

                //menu
                Console.Clear(); 
                Console.WriteLine("1. Read The Movie List.\n2. Add A Movie To List\n3. Exit Program");
                Int32.TryParse(Console.ReadLine(), out input);

                switch (input)
                {
                    case 1:
                        exit = 1;


                        for (int i = 0; i < movieID.Count; i++)
                        { 
                            Console.WriteLine(movieID[i]+" "+movieTitle[i]+" "+movieGenre[i]);
                            
                        }
                        Console.ReadKey();
                        break;
                        

                    case 2:
                        //make the movie id and push it into a variable
                        int movieIDinput = movieID.Max()+1;

                        //Enter all info, push into user input into file
                        exit = 1;
                        Console.Clear();
                        Console.WriteLine("What is the title of the movie you would like to enter(Include year made if possible)");
                        string movieTitleInput = Console.ReadLine();

                        //make sure there is not matching entry
                        //change case in movie title list and case of input to upper to perform check
                        List<string> movieTitleUpper = movieTitle.ConvertAll(c => c.ToUpper());
                        while (movieTitleUpper.Contains(movieTitleInput.ToUpper()))
                        {
                            Console.WriteLine("The movie is already in the list!!");
                            nlog.Info("User tried to enter title already in file");
                            break;
                        }
                        

                        //take in as many genres as the user wants to enter and store in a list with pipe key between all genres.
                        Console.Clear();
                        Console.Write("Please Enter The Number Of Genres(Enter 0 For No Genre): ");
                        int genreCount = 0;
                        Int32.TryParse(Console.ReadLine(), out genreCount);
                        if (genreCount == 0)
                        {
                            string movieGenreInput = "No Genre";
                            List<string> movieGenreInputList = new List<string>();
                            movieGenreInputList.Add(movieGenreInput);
                            break;
                        }
                        else 
                        {
                            List<string> movieGenreInputList = new List<string>();
                            for (int i0 = 0; i0 < genreCount; i0++)
                            {
                                Console.Write($"\nPlease Enter Genre #{i0+1}:  ");
                                string movieGenreInput = Console.ReadLine();
                                
                                if (i0 + 1 == genreCount)
                                {
                                    movieGenreInputList.Add(movieGenreInput);
                                }
                                else
                                
                                    movieGenreInputList.Add(movieGenreInput+" |");
                                 


                            }

                            //check for commas in movie title so the file format stays the same for any inputed infromation
                            int commaIndex = movieTitleInput.IndexOf(',');
                            if (commaIndex != -1)
                            {
                                movieTitleInput = $"\" {movieTitleInput} \"";
                            }
                            else;
                               
                            

                            //open Streamwriter and write to file in correct order
                            StreamWriter inputToFile = new StreamWriter(file, append: true);
                            string movieGenreInputJoin = string.Join(" ", movieGenreInputList);
                            inputToFile.WriteLine($"{movieIDinput},{movieTitleInput},{movieGenreInputJoin}");
                            inputToFile.Close();

                        }

                        

                        break;

                    case 3:
                        exit = 0;
                        Console.Clear();
                        Console.WriteLine("Goodbye");
                        break;

                        

                }
            } while (exit != 0);



    }
    }
}
