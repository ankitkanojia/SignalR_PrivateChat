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
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }

        public override Task OnConnected()
        {
            var UserName = Context.QueryString["UserName"];
            var UserId = Context.ConnectionId;
            StaticValues.ConnectionDetails.Add(new ChatVm {
                UserId = UserId,
                UserName = UserName
            });
            return base.OnConnected();
        }
    }
}