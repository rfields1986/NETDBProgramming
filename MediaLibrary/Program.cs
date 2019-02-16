﻿using System;
using NLog;

namespace MediaLibrary
{
    class MainClass
    {
        // create a class level instance of logger (can be used in methods other than Main)
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            logger.Info("Program started");

            string scrubbedFile = FileScrubber.ScrubMovies("../../movies.csv");
            MovieFile movieFile = new MovieFile(scrubbedFile);

            string choice = "";
            do
            {
                // display choices to user
                Console.WriteLine("1) Add Movie");
                Console.WriteLine("2) Display All Movies");
                Console.WriteLine("Enter to quit");
                // input selection
                choice = Console.ReadLine();
                logger.Info("User choice: {Choice}", choice);
                if (choice == "1")
                {
                    // Add movie
                    Movie movie = new Movie();
                    // ask user to input movie title
                    Console.WriteLine("Enter movie title");
                    // input title
                    movie.title = Console.ReadLine();
                    // verify title is unique
                    if (movieFile.isUniqueTitle(movie.title))
                    {
                        // input genres
                        string input;
                        do
                        {
                            // ask user to enter genre
                            Console.WriteLine("Enter genre (or done to quit)");
                            // input genre
                            input = Console.ReadLine();
                            // if user enters "done"
                            // or does not enter a genre do not add it to list
                            if (input != "done" && input.Length > 0)
                            {
                                movie.genres.Add(input);
                            }
                        } while (input != "done");
                        // specify if no genres are entered
                        if (movie.genres.Count == 0)
                        {
                            movie.genres.Add("(no genres listed)");
                        }
                        // ask user to enter director
                        Console.WriteLine("Enter movie director");
                        input = Console.ReadLine();
                        movie.director = input.Length == 0 ? "unassigned" : input;
                        // ask user to enter running time
                        Console.WriteLine("Enter running time (h:m:s)");
                        input = Console.ReadLine();
                        movie.runningTime = input.Length == 0 ? new TimeSpan(0) : TimeSpan.Parse(input);
                        // add movie
                        movieFile.AddMovie(movie);
                    }
                    else
                    {
                        Console.WriteLine("Movie title already exists\n");
                    }

                }
                else if (choice == "2")
                {
                    // Display All Movies
                    foreach (Movie m in movieFile.Movies)
                    {
                        Console.WriteLine(m.Display());
                    }
                }
            } while (choice == "1" || choice == "2");

            logger.Info("Program ended");
        }
    }
}
