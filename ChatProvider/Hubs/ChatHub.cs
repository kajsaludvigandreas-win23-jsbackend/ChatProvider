using ChatProvider.Data;
using ChatProvider.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatProvider.Hubs;

public class ChatHub(DataContext context) : Hub
{
    private readonly DataContext _context = context;

    public async Task JoinChat(UserConnection uc)
    {
        await Clients.All.SendAsync("RecieveMessage", DateTime.Now.ToString("HH:mm:ss"), uc.UserName, $"{uc.UserName} has joined the chat");
    }

    public async Task JoinSpecificChatRoom(UserConnection uc)
    {
        uc.ConnectionId = Context.ConnectionId;
        await Groups.AddToGroupAsync(uc.ConnectionId, uc.ChatRoom);

        _context.Add(uc);
        await _context.SaveChangesAsync();

        await Clients.Group(uc.ChatRoom).SendAsync("RecieveMessage", DateTime.Now.ToString("HH:mm:ss"), uc.UserName, $"{uc.UserName} has joined the chat");
    }

    public async Task SendMessage(string message)
    {
        var uc = await _context.Connections.FirstOrDefaultAsync(x => x.ConnectionId == Context.ConnectionId);
        if (uc != null)
        {
            await Clients.Group(uc.ChatRoom).SendAsync("RecieveMessage", DateTime.Now.ToString("HH:mm:ss"), uc.UserName, message);
        }
    }

    public async Task StartTyping(string userName, string ChatRoom)
    {
        await Clients.Group(ChatRoom).SendAsync("UserTyping", userName);
    }

    public async Task StopTyping(string userName, string ChatRoom)
    {
        await Clients.Group(ChatRoom).SendAsync("UserStoppedTyping", userName);
    }
}
