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
            string[] sqlQueries =
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

            for (int i = 0; i < sqlQueries.Length; i++)
            {
                Console.WriteLine($"SQL query #{i+1} : ");
                DataBaseQueries(sqlQueries[i]);
                Console.WriteLine();
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
