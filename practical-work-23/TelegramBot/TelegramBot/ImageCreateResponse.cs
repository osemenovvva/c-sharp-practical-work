using System.Numerics;

namespace TelegramBot;

/// <summary>
/// Класс, описывающий ответ от сервера OpenAI
/// </summary>
public class ImageCreateResponse
{
    public BigInteger Created { get; set; }
    public List<ImageData> Data { get; set; }

    public class ImageData
    {
        public string Url { get; set; }
    }
}