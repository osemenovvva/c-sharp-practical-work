namespace Notebook;

struct Worker
{
    #region Конструктор

    /// <summary>
    /// Создание сотрудника
    /// </summary>
    /// <param name="id">Идентификатор сотрудника</param>
    /// <param name="creationDate">Дата создания записи</param>
    /// <param name="fullName">Ф.И.О.</param>
    /// <param name="age">Возраст</param>
    /// <param name="height">Рост</param>
    /// <param name="birthDate">Дата рождения</param>
    /// <param name="birthPlace">Место рождения</param>
    public Worker(int id, DateTime creationDate, string fullName, int age, int height, DateTime birthDate, string birthPlace) 
    {
        Id = id;
        CreationDate = creationDate;
        FullName = fullName;
        Age = age;
        Height = height;
        BirthDate = birthDate;
        BirthPlace = birthPlace;
    }
    #endregion
    
    
    #region Свойства

    /// <summary>
    /// Идентификатор сотрудника
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Дата создания записи
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// Ф.И.О.
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Возраст
    /// </summary>
    public int Age { get; set; }

    /// <summary>
    /// Рост
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Место рождения
    /// </summary>
    public string BirthPlace { get; set; }

    #endregion

    /// <summary>
    /// Метод формирования строки с данными о сотруднике для вывода в консоль
    /// </summary>
    /// <returns>Строка с записью о сотруднике</returns>
    public override string ToString()
    {
        return $"\nИдентификатор: {Id}\n" +
            $"Дата добавления записи: {CreationDate}\n" +
            $"Ф.И.О.: {FullName}\n" +
            $"Возраст: {Age}\n" +
            $"Рост: {Height}\n" +
            $"Дата рождения: {BirthDate}\n" +
            $"Место рождения: {BirthPlace}";
    }
}