using Microsoft.AspNetCore.SignalR;

namespace TheStartupBuddyV3.Hubs
{
    public class ChatHub : Hub
    {
        public Task JoinGroup(string group)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, group);
        }
        public Task SendNotificationToGroup(string group, string message)
        {
            return Clients.Group(group).SendAsync("NotificationReceived", message);
        }

        public Task SendUpdateTeamMember(string group, string message)
        {
            return Clients.Group(group).SendAsync("TeamMemberUpdate", message);
        }

        public Task SendNetworking(string group, string message)
        {
            return Clients.Group(group).SendAsync("NetworkingUpdate", message);
        }
        // public async Task SendMessage(string message)
        // {
        //     await Clients.All.SendAsync("MessageReceived", message);
        // }
    }
}