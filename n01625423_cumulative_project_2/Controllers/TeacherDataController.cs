using MySql.Data.MySqlClient;
using n01625423_cumulative_project_2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace n01625423_cumulative_project_2.Controllers
{
    /// <summary>
    /// A WebAPI Controller which allows you to access information about teachers, add teachers, and remove teachers from the database.
    /// </summary>
    public class TeacherDataController : ApiController
    {
        /// The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        /// This Controller Will access the teachers table of our school database. Non-Deterministic.
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <returns>
        /// A list of Teacher Objects with fields mapped to the database column values (first name, last name, employee number, hiredate, salary).
        /// </returns>
        /// <example> GET api/TeacherData/ListTeachers -> {Teacher Object, Teacher Object, Teacher Object...}</example>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {
            /// Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            /// Open the connection between the web server and database
            Conn.Open();

            /// Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            /// SQL QUERY
            cmd.CommandText = "Select * from teacher where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            /// Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            /// Create an empty list of Teachers
            List<Teacher> Teachers = new List<Teacher> { };

            /// Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                /// Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                string Salary = ResultSet["salary"].ToString();

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                /// Add the Teacher Name to the List
                Teachers.Add(NewTeacher);
            }

            /// Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            /// Return the final list of teacher names
            return Teachers;
        }

        /// <summary>
        /// Finds a teacher from the MySQL Database through an id. 
        /// </summary>
        /// <param name="id"> The Teacher ID </param>
        /// <returns> Teacher object containing information about the teacher with a matching ID. Empty Teacher Object if the ID does not match any teachers in the system. 
        /// </returns>
        /// <example> api/TeacherData/FindTeacher/6 -> {Teacher Object}</example>
        /// <example> api/TeacherData/FindTeacher/10 -> {Teacher Object}</example>
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            /// Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            /// Open the connection between the web server and database
            Conn.Open();

            /// Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            /// SQL QUERY
            cmd.CommandText = "Select * from teacher where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            /// Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                /// Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                string Salary = ResultSet["salary"].ToString();

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
            }
            Conn.Close();

            return NewTeacher;
        }

        /// <summary>
        /// Deletes a Teacher from the connected MySQL Database if the ID of that teacher exists. Does NOT maintain relational integrity. 
        /// </summary>
        /// <param name="id"> The ID of the teacher </param>
        /// <example> POST /api/TeacherData/DeleteTeacher/3 </example>
        
       [HttpPost]
        public void DeleteTeacher(int id)
        {
            /// Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            /// Delete Teacher Data From Classes Table
            /// Open the connection between the web server and database
            Conn.Open();

            /// Establish a new command (query) for our database
            MySqlCommand cmdd = Conn.CreateCommand();

            /// SQL QUERY
            cmdd.CommandText = "DELETE FROM classes WHERE classes.teacherid = @key";

            cmdd.Parameters.AddWithValue("@key", id);
            cmdd.Prepare();
            cmdd.ExecuteNonQuery();
            Conn.Close();

            /// Delete  Teacher From Teacher Table
            /// Open the connection between the web server and database
            Conn.Open();

            /// Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            /// SQL QUERY
            cmd.CommandText = "Delete from teacher where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            Conn.Close();
        } 

        /// <summary>
        /// Adds a Teacher to the MySQL Database.
        /// </summary>
        /// <param name="NewTeacher"> An object with fields that map to the columns of the teacher's table. </param>
        /// <example>
        /// POST api/TeacherData/AddTeacher 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"Owen",
        ///	"TeacherLname":"Laing",
        ///	"EmployeeNumber":"T212",
        ///	"Salary":"80.24"
        /// }
        /// </example>
        /// 
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void AddTeacher([FromBody]Teacher NewTeacher)
        {            
            /// Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            Debug.WriteLine(NewTeacher.TeacherFname);

            /// Open the connection between the web server and database
            Conn.Open();

            /// Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            /// SQL QUERY
            cmd.CommandText = "insert into teacher (teacherfname, teacherlname, employeenumber, hiredate, salary) values (@TeacherFname,@TeacherLname,@EmployeeNumber, CURRENT_DATE(), @Salary)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close(); 
        }
    }
}
