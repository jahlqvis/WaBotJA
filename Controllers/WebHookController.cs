using Microsoft.AspNetCore.Mvc;
using WaBot.Helpers;
using WaBot.Helpers.Json;

namespace WaBot.Controllers;

/// <summary>
/// Controller for processing requests coming from chat-api.com
/// </summary>
[ApiController]
[Route("/")]
public class WebHookController : ControllerBase
{
    /// <summary>
    /// A static object that represents the API for a given controller.
    /// </summary>
    private static readonly WaApi api = new WaApi("https://api.chat-api.com/instance426950/", "8lbrswhfjguzv6bf");
    private static readonly string welcomeMessage = "Bot's menu: \n" +
                                                        "1. chatid - Get chatid\n" +
                                                        "2. file doc/gif,jpg,png,pdf,mp3,mp4 - Get a file in the desired format\n" +
                                                        "3. ogg - Get a voice message\n" +
                                                        "4. geo - Get the geolocation\n" +
                                                        "5. group - Create a group with a bot";
    /// <summary>
    /// Handler of post requests received from chat-api
    /// </summary>
    /// <param name="data">Serialized json object</param>
    /// <returns></returns>    
    [HttpPost]
    public async Task<string> Post(Answer data)
    {
        foreach (var message in data.messages)
        {
            if (message.fromMe)
                continue;

            switch (message.body.Split()[0].ToLower())
            {
                case "chatid":
                    return await api.SendMessage(message.chatId, $"Your ID: {message.chatId}");
                case "file":
                    var texts = message.body.Split();
                    if (texts.Length > 1)
                        return await api.SendFile(message.chatId, texts[1]);
                    break;
                case "ogg":
                    return await api.SendOgg(message.chatId);
                case "geo":
                    return await api.SendGeo(message.chatId);
                case "group":
                    return await api.CreateGroup(message.author);
                default:
                    return await api.SendMessage(message.chatId, welcomeMessage);
            }
        }
        return "";
    }
}
