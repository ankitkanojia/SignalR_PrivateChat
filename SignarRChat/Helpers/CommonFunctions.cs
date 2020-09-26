using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignarRChat.Models;

namespace SignarRChat.Helpers
{
    public static class CommonFunctions
    {
        //todo: get data from the database
        public static List<ContactVm> GetContacts()
        {
            var response = new List<ContactVm>
            {
                new ContactVm
                {
                    ContactId = 1,
                    Description = "Lorem ipsum dummy text",
                    ImagePath = "user1.png",
                    Name = "Suchit",
                    IsGroup = false
                },
                new ContactVm
                {
                    ContactId = 2,
                    Description = "Lorem ipsum dummy text",
                    ImagePath = "user2.png",
                    Name = "Ankit",
                    IsGroup = false
                },
                new ContactVm
                {
                    ContactId = 3,
                    Description = "Lorem ipsum dummy text",
                    ImagePath = "user3.png",
                    Name = "Naresh",
                    IsGroup = false
                },
                new ContactVm
                {
                    ContactId = 4,
                    Description = "Lorem ipsum dummy text",
                    ImagePath = "user4.png",
                    Name = "Hitesh",
                    IsGroup = false
                },
                new ContactVm
                {
                    ContactId = 5,
                    Description = "Lorem ipsum dummy text",
                    ImagePath = "user6.png",
                    Name = "Roma",
                    IsGroup = false
                }
            };







            return response;
        }

    }
}