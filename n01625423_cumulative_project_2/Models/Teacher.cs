using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace n01625423_cumulative_project_2.Models
{
    /// <summary>
    /// A Model which allows you to represent information about a teacher
    /// </summary>
    public class Teacher
    {
        /// The following fields define a Teacher
        public int TeacherId;
        public string TeacherFname;
        public string TeacherLname;
        public string EmployeeNumber;
        public DateTime HireDate;
        public string Salary;

        ///  Constructor function [ Without Parameter ]
        public Teacher() { }
    }
}