namespace WaBot.Helpers.Json; 

public class Message
{
    public string id { get; set; } = null!;
    public string body { get; set; } = null!;
    public string type { get; set; } = null!;
    public string senderName { get; set; } = null!;
    public bool fromMe { get; set; }
    public string author { get; set; } = null!;
    public long time { get; set; } 
    public string chatId { get; set; } = null!;
    public long messageNumber { get; set; }
}
public class Answer
{
    public string instanceId { get; set; } = null!;
    public Message[] messages { get; set; } = null!;
}



