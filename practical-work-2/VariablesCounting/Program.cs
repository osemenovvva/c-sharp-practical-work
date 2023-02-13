using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariablesCounting
{
    class Program
    {
        static void Main(string[] args)
        {
            //Задание 1
            string fullName = "Семенова Ольга Владимировна";
            string email = "o.semenovvva@gmail.com";

            int age = 29;

            double programmingPoint = 4.5;
            double mathPoint = 5.0;
            double physicsPoint = 3.9;

            string person = "ФИО: {0} \nВозраст: {1} \nEmail: {2} \nБаллы по программированию: {3} \nБаллы по математике: {4} \nБаллы по физике: {5}";

            Console.WriteLine(person,
                              fullName,
                              age,
                              email,
                              programmingPoint,
                              mathPoint,
                              physicsPoint
                              );
            Console.ReadKey();

            //Задание 2
            double sumPoint = programmingPoint + mathPoint + physicsPoint;
            double avgPoint = sumPoint / 3;

            Console.WriteLine($"Сумма баллов: {sumPoint} \nСредний балл: {avgPoint.ToString("#.#")}");

        }
    }
}

