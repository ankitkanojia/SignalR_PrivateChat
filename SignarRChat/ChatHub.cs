using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignarRChat.Helpers;
using SignarRChat.Models;

namespace SignarRChat
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message, string userImageUrl)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message, userImageUrl);
        }

        public override Task OnConnected()
        {
            var UserName = Context.QueryString["UserName"];
            var ProfileImage = Context.QueryString["ProfileImage"];
            var UserId = Context.ConnectionId;
            StaticValues.ConnectionDetails.Add(new ChatVm {
                UserId = UserId,
                UserName = UserName,
                ProfileImage = ProfileImage
            });
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var UserId = Context.ConnectionId;
            var foundUser = StaticValues.ConnectionDetails.FirstOrDefault(m => m.UserId.ToLower().Trim() == UserId.ToLower().Trim());
            if (stopCalled)
            {
                // We know that Stop() was called on the client,
                // and the connection shut down gracefully.
            }
            else
            {
                // This server hasn't heard from the client in the last ~35 seconds.
                // If SignalR is behind a load balancer with scaleout configured, 
                // the client may still be connected to another SignalR server.
            }

            if(foundUser != null)
            {
                StaticValues.ConnectionDetails.Remove(foundUser);
            }
            return base.OnDisconnected(stopCalled);
        }
    }
}