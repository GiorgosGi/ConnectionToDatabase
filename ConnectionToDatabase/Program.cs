using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConnectionToDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] sqlQueries1 =
            {
                "A list of all the students",
                "A list of all the trainers", 
                "A list of all the assignments", 
                "A list of all the courses", 
                "All the students per course", 
                "All the trainers per course",
                "All the assignments per course",
                "All the assignments per course per student",
                "A list of students that belong to more than one courses"
            };

            string[] sqlQueries2 =
            {
            "SELECT * FROM Students",
            "SELECT * FROM Trainers",
            "SELECT * FROM Assignments",
            "SELECT * FROM Courses",
            "SELECT Students.id, FirtsName, LastName FROM Students INNER JOIN Classrooms ON Classrooms.Studentid = Students.id WHERE Classrooms.Courseid = 1",
            "SELECT DISTINCT Trainers.id, FirstName, LastName FROM Trainers INNER JOIN Classrooms ON Classrooms.Trainerid = Trainers.id WHERE Classrooms.Courseid = 1",
            "SELECT * FROM Assignments WHERE Courseid = 1",
            "SELECT * FROM Assignments WHERE Courseid = 1 AND Studentid = 1",
            "SELECT FirtsName, LastName, COUNT(Classrooms.Studentid) AS NoOfCourses FROM (Students INNER JOIN Classrooms ON Classrooms.Studentid = Students.id) GROUP BY FirtsName, LastName, Classrooms.Studentid HAVING COUNT(Classrooms.Studentid) > 1"
            };

            Console.WriteLine("Press a key to see the SQL queries:");
            Console.WriteLine();
            Console.ReadKey();

            for (int i = 0; i < sqlQueries2.Length; i++)
            {
                Console.WriteLine($"SQL query #{i+1}: {sqlQueries1[i]}: ");
                DataBaseQueries(sqlQueries2[i]);
                Console.WriteLine();
                Console.ReadKey();
            }


        }

        
        private static void DataBaseQueries(string sqlQuer)
        {
            SqlConnection conn = new SqlConnection("Server =.; Database = IndividualProjectPartB; Trusted_Connection = True;");
            SqlDataReader rdr = null;

            try
            {
                conn.Open();
                string tempQuery = sqlQuer;
                SqlCommand cmd = new SqlCommand(sqlQuer, conn);
                rdr = cmd.ExecuteReader();
                if (rdr != null)
                {
                    while (rdr.Read())
                    {
                        Console.WriteLine("\t{0}\t{1}\t{2}", rdr[0], rdr[1], rdr[2]);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (rdr != null) rdr.Close();
                if (conn != null) conn.Close();
            }
        }
    }
}
