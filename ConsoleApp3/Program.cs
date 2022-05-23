using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            Student student = new Student(host.Services.GetService<IConsolePrinter>()){ Name = "Test"};
            student.PrintStudentName();
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                 .ConfigureServices((_, services) =>
                     services
                     .AddScoped<IConsolePrinter, FilePrinter>()
                     .AddScoped<IConsolePrinter, ConsolePrinter>());
                            
        }
    }

    public class Student
    {
        private readonly IConsolePrinter _consolePrinter;
        public Student()
        {

        }
        public Student(IConsolePrinter consolePrinter)
        {
            _consolePrinter = consolePrinter;
        }
        public String Name { get; set; }
        public void PrintStudentName()
        {
            _consolePrinter.Print(Name);
        }
    }

    public interface IConsolePrinter
    {
        public void Print(string text);
    }

    public class ConsolePrinter:IConsolePrinter
    {
        public void Print(string text)
        {
            Console.WriteLine(text);
        }
    }

    public class FilePrinter : IConsolePrinter
    {
        public void Print(string text)
        {
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "StudentNames.txt"), text);
        }
    }
}
