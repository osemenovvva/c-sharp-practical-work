
namespace BankSystemLibrary.Model
{
    public class SaveAccountException : Exception
    {
        public SaveAccountException(string message) : base(message)
        {
            Message = message;
        }

        public string Message { get; set; }

    }
}
