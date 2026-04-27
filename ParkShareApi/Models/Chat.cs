namespace ParkShareApi.Models;

public class Chat
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid SeekerId { get; set; }
    public User? Seeker { get; set; }
    public Guid OwnerId { get; set; }
    public User? Owner { get; set; }
    public Guid? RelatedSpaceId { get; set; }
    public ParkingSpace? RelatedSpace { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<Message> Messages { get; set; } = [];
}

public class Message
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ChatId { get; set; }
    public Chat? Chat { get; set; }
    public Guid SenderId { get; set; }
    public User? Sender { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
