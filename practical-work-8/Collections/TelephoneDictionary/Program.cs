namespace TelephoneDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> telephoneBook = new Dictionary<string, string>();

            Console.WriteLine("-----Телефонная книга-----\n");
            while (true)
            {
                Console.Write("Введите Ф.И.О.: ");
                var name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    break;
                }
                Console.Write("Введите номер телефона: ");
                var telephone = Console.ReadLine();
                if (string.IsNullOrEmpty(telephone))
                {
                    break;
                }
                AddTelephone(telephoneBook, name, telephone);
                Console.WriteLine();
            }

            Console.Write("Введите Ф.И.О. владельца телефона для поиска: ");
            var nameToSearch = Console.ReadLine();
            SearchTelephoneOwner(telephoneBook, nameToSearch);
        }

        /// <summary>
        /// Метод добавления данных в словарь
        /// </summary>
        /// <param name="telephoneBook">Телефонная книга</param>
        /// <param name="name">Имя владельца</param>
        /// <param name="telephone">Номер телефона</param>
        public static void AddTelephone(Dictionary<string, string> telephoneBook, string name, string telephone)
        {
            telephoneBook.Add(name, telephone);
        }

        /// <summary>
        /// Метод для поиска телефона по имени владельца
        /// </summary>
        /// <param name="telephoneBook">Телефонная книга</param>
        /// <param name="name">Имя владельца</param>
        public static void SearchTelephoneOwner(Dictionary<string, string> telephoneBook, string name)
        {
            Console.WriteLine(telephoneBook.TryGetValue(name, out string result) ? result : "Владелец телефона не найден");
        }
    }
}