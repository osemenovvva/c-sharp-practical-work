using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TelegramBot;

public sealed class OpenAiImageGenerator
{
    /// <summary>
    /// Конструктор Http-клиента
    /// </summary>
    private static HttpClient sharedClient = new()
    {
        BaseAddress = new Uri("https://api.openai.com/v1/images/generations")
    };
    
    /// <summary>
    /// Поле для хранения единственного экземпляра класса OpenAiImageGenerator
    /// </summary>
    private static OpenAiImageGenerator? _openAiImageGenerator;
    
    private OpenAiImageGenerator()
    {
        sharedClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", "***");
    }

    /// <summary>
    /// Метод для генерации изображения на основе текстового описания
    /// </summary>
    /// <param name="imageDescription">Текстовое описание изображения</param>
    /// <returns>URL сгенерированного изображения</returns>
    public async Task<string> CreateImage(string imageDescription)
    {
        using StringContent jsonContent = new(
            JsonSerializer.Serialize(new
            {
                prompt = imageDescription,
                n = 1,
                size = "1024x1024"
            }),
            Encoding.UTF8,
            "application/json");

        var response = await sharedClient.PostAsync("", jsonContent);
        var jsonResponse = await response.Content.ReadAsStringAsync();
        
        var responseObject = JsonConvert.DeserializeObject<ImageCreateResponse>(jsonResponse);
        var imageUrl = responseObject.Data[0].Url;
        
        return imageUrl;
    }

    /// <summary>
    /// Метод для получения единственного экземпляра класса OpenAiImageGenerator
    /// </summary>
    /// <returns>Экземпляр класса OpenAiImageGenerator</returns>
    public static OpenAiImageGenerator GetInstance()
    {
        if (_openAiImageGenerator == null)
        {
            _openAiImageGenerator = new OpenAiImageGenerator();
        }
        return _openAiImageGenerator;
    }
}