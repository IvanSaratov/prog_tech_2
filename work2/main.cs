using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SGU_database
{

    class StudentContext:DbContext
    {
        public StudentContext() : base("DbConnection")
        { }
        public DbSet<Student> Students { get; set; }
    }

    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class StudentAction
    {
        static void AddStudents()
        {
            Console.WriteLine("Введите колличество студентов:");
            int n = int.Parse(Console.ReadLine());

            using (StudentContext db = new StudentContext())
            {
                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine($"Данные студента {i + 1}:");

                    Console.Write("Имя: ");
                    string name = Console.ReadLine();

                    Console.Write("Возраст: ");
                    int age = int.Parse(Console.ReadLine());

                    Student student = new Student
                    {
                        Name = name,
                        Age = age
                    };

                    db.Students.Add(student);
                    db.SaveChanges();
                    Console.Write($"Студент {i + 1} добавлен.\n");
                }
            }
        }


        static void DisplayAll()
        {
            using (StudentContext db = new StudentContext())
            {
                var students = db.Students;

                Console.WriteLine("Список всех студентов:");
                foreach (Student student in students)
                {
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine($"ID: {student.Id}");
                    Console.WriteLine($"Имя: {student.Name}");
                    Console.WriteLine($"Возраст: {student.Age}");
                    Console.WriteLine("---------------------------------------");
                }
            }
        }

        static void DeleteByID()
        {
            Console.Write("Введите ID для удаления: ");
            int id = int.Parse(Console.ReadLine());

            using (StudentContext db = new StudentContext())
            {
                var student = db.Students.Find(id);
                if (student != null)
                {
                    db.Students.Remove(student);
                }
                db.SaveChanges();
            }
        }
    }
}
