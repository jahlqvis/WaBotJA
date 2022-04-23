using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WaBot.Helpers;

public class WaApi
{
    private string APIUrl = "";
    private string token = "";

    public WaApi(string aPIUrl, string token)
    {
        APIUrl = aPIUrl;
        this.token = token;
    }

    public async Task<string> SendRequest(string method, string data)
    {
        string url = $"{APIUrl}{method}?token={token}";

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(url);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("", content);
            return await result.Content.ReadAsStringAsync();
        }
    }

    public async Task<string> SendMessage(string chatId, string text)
    {
        var data = new Dictionary<string, string>()
        {
            {"chatId",chatId },
            { "body", text }
        };
        return await SendRequest("sendMessage",  JsonSerializer.Serialize(data));
    }

    public async Task<string> SendOgg(string chatId)
    {
        string ogg = "https://firebasestorage.googleapis.com/v0/b/chat-api-com.appspot.com/o/audio_2019-02-02_00-50-42.ogg?alt=media&token=a563a0f7-116b-4606-9d7d-172426ede6d1";
        var data = new Dictionary<string, string>
        {
            {"audio", ogg },
            {"chatId", chatId }
        };

        return await SendRequest("sendAudio", JsonSerializer.Serialize(data));
    }

    public async Task<string> SendGeo(string chatId)
    {
        var data = new Dictionary<string, string>()
        {
            { "lat", "55.756693" },
            { "lng", "37.621578" },
            { "address", "Your address" },
            { "chatId", chatId}
        };
        return await SendRequest("sendLocation", JsonSerializer.Serialize(data));
    }

    public async Task<string> CreateGroup(string author)
    {
        var phone = author.Replace("@c.us", "");
        var data = new Dictionary<string, string>()
        {
            { "groupName", "Group C#"},
            { "phones", phone },
            { "messageText", "This is your group." }
        };
        return await SendRequest("group", JsonSerializer.Serialize(data));
    }

    public async Task<string> SendFile(string chatId, string format)
    {
        var availableFormat = new Dictionary<string, string>()
        {
            {"doc", Base64String.Doc },
            {"gif",Base64String.Gif },

            { "jpg",Base64String.Jpg },
            { "png", Base64String.Png },
            { "pdf", Base64String.Pdf },
            { "mp4",Base64String.Mp4 },
            { "mp3", Base64String.Mp3}
        };

        if (availableFormat.ContainsKey(format))
        {
            var data = new Dictionary<string, string>(){
                { "chatId", chatId },
                { "body", availableFormat[format] },
                { "filename", "yourfile" },
                { "caption", $"My file!" }
            };

            return await SendRequest("sendFile", JsonSerializer.Serialize(data));
        }

        return await SendMessage(chatId, "No file with this format");
    }




}