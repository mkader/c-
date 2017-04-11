using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC
{
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class StudentView
    {
        public void Print(int id, string name) {
            Console.WriteLine("Student");
            Console.WriteLine("ID : "+ id);
            Console.WriteLine("Name : "+ name);
        }
    }

    public class StudentController
    {
        private Student model;
        private StudentView view;

        public StudentController(Student model, StudentView view)
        {
            this.model = model;
            this.view = view;
        }

        public void SetName(string name)
        {
            model.Name = name;
        }

        public string GetName()
        {
            return model.Name;
        }

        public void SetID(int id)
        {
            model.ID = id;
        }

        public int GetID()
        {
            return model.ID;
        }

        public void UpdateView()
        {
            view.Print(model.ID, model.Name);
        }
    }

    public static class Client
    {
        public static void Demo()
        {
            //fetch student record based on his roll no from the database
            Student s = new Student() { Name = "A", ID = 1 };

            //Create a view : to write student details on console
            StudentView v = new StudentView();

            StudentController sc = new StudentController(s, v);
            sc.UpdateView();
            
            //update model
            sc.SetName("B");
            sc.UpdateView();
            Console.Read();
        }
    }
}
