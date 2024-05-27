using System.ComponentModel.DataAnnotations;

namespace ChatProvider.Models;

public class UserConnection
{
    [Key]
    public int Id { get; set; }

    public string UserName { get; set; } =null!;

    public string ChatRoom { get; set; } =null!;

    public string ConnectionId { get; set; } =null!;
}
