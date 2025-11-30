﻿using System;
using System.Collections.Generic;

namespace StudentGradeManager
{
    class Program
    {
        static List<string> students = new List<string>();
        static List<string> studentsID = new List<string>();

        // each student has their own dictionary: subject & score
        static List<Dictionary<string, double>> studentScores = new List<Dictionary<string, double>>();

        static void Main(string[] args)
        {
            while (true)
            {
                // Display menu
                Console.WriteLine("\n--- Student Grade Manager ---");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Add Score");
                Console.WriteLine("3. Calculate Average");
                Console.WriteLine("4. Show Information");
                Console.WriteLine("5. Exit");

                string option = Console.ReadLine();

                // Handle menu options
                switch (option)
                {
                    case "1":
                        AddStudent();
                        break;

                    case "2":
                        AddScore();
                        break;

                    case "3":
                        CalculateAverage();
                        break;

                    case "4":
                        ShowInformation();
                        break;

                    case "5":
                        ShowClosingMessage();
                        return;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        // Here goes the logic to add a student
        static void AddStudent()
        {
            string name;
            string id;

            Console.WriteLine("\n--- Add a Student ---");

            //While loop to validate name and check for duplicates
            while (true)
            {
                Console.WriteLine("Input student name:");
                name = Console.ReadLine();

                // validate name
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Name cannot be empty.");
                    continue;
                }

                // check for duplicates
                if (students.Contains(name))
                {
                    Console.WriteLine("This student name already exists. Enter another.");
                    continue;
                }

                break;
            }

            //While loop to validate ID and check for duplicates
            while (true)
            {
                Console.WriteLine("Input student ID:");
                id = Console.ReadLine();

                // validate ID
                if (string.IsNullOrWhiteSpace(id))
                {
                    Console.WriteLine("ID cannot be empty.");
                    continue;
                }

                // check for duplicates
                if (studentsID.Contains(id))
                {
                    Console.WriteLine("This student ID already exists. Enter another.");
                    continue;
                }

                break;
            }

            // After validation, add student
            students.Add(name);
            studentsID.Add(id);
            studentScores.Add(new Dictionary<string, double>()); // IMPORTANT, THIS LINE ADDS A NEW DICTIONARY FOR THE NEW STUDENT (subjects & scores)

            Console.WriteLine("Student added successfully.");
        }


        // Here goes the logic to add a score
        static void AddScore()
        {
            Console.WriteLine("Enter student ID:");
            string id = Console.ReadLine();

            // validate student ID
            if (!studentsID.Contains(id))
            {
                Console.WriteLine("Student ID not found.");
                return;
            }

            int index = studentsID.IndexOf(id); // get index of student to access their scores

            Console.WriteLine("Enter subject name:");
            string subject = Console.ReadLine();

            // validate subject
            if (string.IsNullOrWhiteSpace(subject))
            {
                Console.WriteLine("Subject cannot be empty.");
                return;
            }

            // prevent adding same subject twice
            if (studentScores[index].ContainsKey(subject))
            {
                Console.WriteLine("This subject already has a score for this student.");
                return;
            }

            Console.WriteLine("Enter score:");
            string scoreInput = Console.ReadLine();

            // validate score
            if (!double.TryParse(scoreInput, out double score) || score < 0 || score > 100)
            {
                Console.WriteLine("Score must be a number between 0 and 100.");
                return;
            }

            studentScores[index].Add(subject, score); // add subject & score to the student's dictionary

            Console.WriteLine("Score added successfully.");
        }

        static void CalculateAverage()
        {
            Console.WriteLine("Enter student ID to calculate average:");
            string id = Console.ReadLine();

            // validate student ID
            if (!studentsID.Contains(id))
            {
                Console.WriteLine("Student not found.");
                return;
            }

            int index = studentsID.IndexOf(id); // get index of student to access their scores

            // check if student has scores
            if (studentScores[index].Count == 0)
            {
                Console.WriteLine("This student has no scores.");
                return;
            }

            // After validation, calculate average
            double total = 0;
            foreach (var score in studentScores[index])
                total += score.Value;

            double avg = total / studentScores[index].Count;

            Console.WriteLine("Average score for:" + students[index] + " (ID: " + id +") = " + avg); 
        }

        static void ShowInformation()
        {
            Console.WriteLine("\n--- Student Information ---");

            // check if there are students
            if (students.Count == 0)
            {
                Console.WriteLine("No students available.");
                return;
            }

            // Display information for each student
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine("\nStudent: " + students[i]);
                Console.WriteLine("ID: " + studentsID[i]);

                // check if student has scores
                if (studentScores[i].Count == 0)
                {
                    Console.WriteLine("No scores recorded.");
                    continue;
                }

                double total = 0;
                
                Console.WriteLine("Subjects & Scores:");

                // Display each subject and score
                foreach (var subj in studentScores[i])
                {
                    Console.WriteLine(" - " + subj.Key + ": " + subj.Value);
                    total += subj.Value;
                }

                double avg = total / studentScores[i].Count;

                Console.WriteLine($"Average: {avg:F2}");
            }
        }
        
        static void ShowClosingMessage()
        {
            Console.WriteLine("\nThank you for using the Student Grade Manager. Goodbye!");
            return;
        }
    }

    class Student
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public Dictionary<string, double> Scores { get; set; } // subject & score

        public Student(string name, string id)
        {
            Name = name;
            ID = id;
            Scores = new Dictionary<string, double>(); // initialize empty dictionary
        }
    }
}