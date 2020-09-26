using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SignarRChat.Helpers;
using SignarRChat.Models;

namespace ChatApp
{
    [HubName("chatHub")]
    public class ChatHub : Hub
    {
        static readonly List<ContactVm> ConnectedContacts = new List<ContactVm>();
        private static readonly List<MessageVm> ConnectedMessages = new List<MessageVm>();

        public void Connect(string name, int contactId)
        {
            var id = Context.ConnectionId;

            if (ConnectedContacts.Count(x => x.ConnectionId == id) != 0) return;
            var data = CommonFunctions.GetContacts().FirstOrDefault(s => s.ContactId == contactId);
            if (data == null) return;
            data.ConnectionId = id;
            ConnectedContacts.Add(data);

            var currentUser = ConnectedContacts.FirstOrDefault(u => u.ConnectionId == id);

            if (currentUser == null) return;

            // send to caller 
            Clients.Caller.onConnected(currentUser.ContactId.ToString(), currentUser.Name, ConnectedContacts,
                ConnectedMessages, currentUser.ContactId);

            // send to all except caller client           
            Clients.AllExcept(currentUser.ConnectionId).onNewUserConnected(currentUser.ContactId.ToString(),
                currentUser.Name, currentUser.ContactId);
        }

        public void SendMessageToAll(string name, string message)
        {
            // store last 100 messages in cache
            //AddMessageInCache(userName, message);

            // Broad cast message
            //Clients.All.messageReceived(userName, message);
        }

        public void SendPrivateMessage(string toContactId, string message)
        {
            try
            {
                var fromConnectionId = Context.ConnectionId;
                var strFromContactId = (ConnectedContacts.Where(u => u.ConnectionId == Context.ConnectionId)
                    .Select(u => u.ContactId).FirstOrDefault()).ToString();
                int.TryParse(strFromContactId, out var fromContactId);
                int.TryParse(toContactId, out var toUserId);
                var fromContacts = ConnectedContacts.Where(u => u.ContactId == fromContactId).ToList();
                var toContacts = ConnectedContacts.Where(x => x.ContactId == toUserId).ToList();

                if (fromContacts.Count == 0 || !toContacts.Any()) return;
                foreach (var item in toContacts)
                {
                    // send to                                                                                            //Chat Title
                    Clients.Client(item.ConnectionId).sendPrivateMessage(fromContactId.ToString(), fromContacts[0].Name,
                        fromContacts[0].Name, message);
                }

                foreach (var item in fromContacts)
                {
                    // send to caller user                                                                                //Chat Title
                    Clients.Client(item.ConnectionId).sendPrivateMessage(toUserId.ToString(), fromContacts[0].Name,
                        toContacts[0].Name, message);
                }

                // send to caller user
                //Clients.Caller.sendPrivateMessage(_toUserId.ToString(), FromUsers[0].UserName, message);
                //ChatDB.Instance.SaveChatHistory(_fromUserId, _toUserId, message);
                var messageDetail = new MessageVm()
                {
                    FromContactId = fromContactId,
                    FromName = fromContacts[0].Name,
                    ToContactId = toUserId,
                    ToName = toContacts[0].Name,
                    Message = message
                };
                AddMessageInCache(messageDetail);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void RequestLastMessage(int fromContactId, int toContactId)
        {
            var currentChatMessages = (from u in ConnectedMessages where ((u.FromContactId == fromContactId && u.ToContactId == toContactId) || (u.FromContactId == toContactId && u.ToContactId == fromContactId)) select u).ToList();
            //send to caller user
            Clients.Caller.GetLastMessages(toContactId, currentChatMessages);
        }

        public void SendUserTypingRequest(string strToContactId)
        {
            var strFromContactId = (ConnectedContacts.Where(u => u.ConnectionId == Context.ConnectionId).Select(u => u.ContactId).FirstOrDefault()).ToString();

            int.TryParse(strToContactId, out var toContactId);
            var toContacts = ConnectedContacts.Where(x => x.ContactId == toContactId).ToList();

            foreach (var item in toContacts)
            {
                // send to                                                                                            
                Clients.Client(item.ConnectionId).ReceiveTypingRequest(strFromContactId);
            }
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = ConnectedContacts.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item == null) return base.OnDisconnected(stopCalled);
            ConnectedContacts.Remove(item);
            if (ConnectedContacts.Where(u => u.ConnectionId == item.ConnectionId).Count() == 0)
            {
                var id = item.ContactId.ToString();
                Clients.All.onUserDisconnected(id, item.Name);
            }
            return base.OnDisconnected(stopCalled);
        }

        private static void AddMessageInCache(MessageVm messageDetail)
        {
            ConnectedMessages.Add(messageDetail);
            if (ConnectedMessages.Count > 100)
                ConnectedMessages.RemoveAt(0);
        }
    }
}