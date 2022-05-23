using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Course course = new Course();
            course.students.Add(new Student() { Name = "Test"});
            course.students.Add(new Student() { Name = "Test1" });
            course.students.Add(new Student() { Name = "Test2" });

            course.PrintStudents();
        }
    }

    public class Student :IStudent
    {
        public string Name { get; set; }

        public void PrintName()
        {
            Console.WriteLine(Name);
        }
    }

    public interface IStudent
    {
        public void PrintName();
    }


    public class Course
    {
        public Course()
        {
            students = new List<IStudent>();
        }
       public List<IStudent> students { get; set; }

        public void PrintStudents()
        {
            foreach (var student in students)
            {
                student.PrintName();
            }
        }
    }


    public class Printer<T>
    {
        public T Property { get; set; }

        public void Print()
        {
            Console.WriteLine(Property.GetType().Name);
        }
    }

}
