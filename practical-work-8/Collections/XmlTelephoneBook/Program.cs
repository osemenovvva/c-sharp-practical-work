using System.Xml.Linq;

namespace XmlTelephoneBook
{
    class Program
    {
        static void Main(string[] args)
        {
            Person concretePerson = new Person();

            Console.Write("Введите Ф.И.О.: ");
            concretePerson.FullName = Console.ReadLine();
            Console.Write("Введите улицу: ");
            concretePerson.Street = Console.ReadLine();
            Console.Write("Введите номер дома: ");
            concretePerson.Building = Console.ReadLine();
            Console.Write("Введите номер квартиры: ");
            concretePerson.FlatNumber = int.Parse(Console.ReadLine());
            Console.Write("Введите мобильный телефон: ");
            concretePerson.MobilePhone = Console.ReadLine();
            Console.Write("Введите домашний телефон: ");
            concretePerson.FlatPhone = Console.ReadLine();

            AddContactToFile(concretePerson, "contact.xml");

        }
        
        /// <summary>
        /// Метод для добавления данных о контакте в xml-файл
        /// </summary>
        /// <param name="concretePerson">Контакты человека</param>
        /// <param name="path">Путь к xml-файлу</param>
        static void AddContactToFile(Person concretePerson, string path)
        {
            var contactXml = new XDocument();
            XElement root = new XElement("Root",   
                new XAttribute("name", concretePerson.FullName)  
            );
            contactXml.Add(root);
            root.Add(
                new XElement("Address", 
                    new XElement("Street", concretePerson.Street),
                    new XElement("Building", concretePerson.Building),
                    new XElement("FlatNumber", concretePerson.FlatNumber.ToString())),
                new XElement("Phones",
                    new XElement("MobilePhone", concretePerson.MobilePhone),
                    new XElement("FlatPhone", concretePerson.FlatPhone))
                );
            Console.WriteLine(contactXml.ToString());  
            contactXml.Save(path);  
        }
 
    }
}
