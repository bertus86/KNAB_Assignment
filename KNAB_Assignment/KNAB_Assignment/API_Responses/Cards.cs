﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KNAB_Assignment.API_Responses
{
    class Cards
    {

        public class Cardslist
        {
            public Card[] Property1 { get; set; }
        }

        public class Card
        {
            public string id { get; set; }
            public object checkItemStates { get; set; }
            public bool closed { get; set; }
            public DateTime dateLastActivity { get; set; }
            public string desc { get; set; }
            public object descData { get; set; }
            public object dueReminder { get; set; }
            public string idBoard { get; set; }
            public string idList { get; set; }
            public object[] idMembersVoted { get; set; }
            public int idShort { get; set; }
            public object idAttachmentCover { get; set; }
            public object[] idLabels { get; set; }
            public bool manualCoverAttachment { get; set; }
            public string name { get; set; }
            public int pos { get; set; }
            public string shortLink { get; set; }
            public bool isTemplate { get; set; }
            public Badges badges { get; set; }
            public bool dueComplete { get; set; }
            public object due { get; set; }
            public string[] idChecklists { get; set; }
            public object[] idMembers { get; set; }
            public object[] labels { get; set; }
            public string shortUrl { get; set; }
            public object start { get; set; }
            public bool subscribed { get; set; }
            public string url { get; set; }
            public Cover cover { get; set; }
        }

        public class Badges
        {
            public Attachmentsbytype attachmentsByType { get; set; }
            public bool location { get; set; }
            public int votes { get; set; }
            public bool viewingMemberVoted { get; set; }
            public bool subscribed { get; set; }
            public string fogbugz { get; set; }
            public int checkItems { get; set; }
            public int checkItemsChecked { get; set; }
            public object checkItemsEarliestDue { get; set; }
            public int comments { get; set; }
            public int attachments { get; set; }
            public bool description { get; set; }
            public object due { get; set; }
            public bool dueComplete { get; set; }
            public object start { get; set; }
        }

        public class Attachmentsbytype
        {
            public Trello trello { get; set; }
        }

        public class Trello
        {
            public int board { get; set; }
            public int card { get; set; }
        }

        public class Cover
        {
            public object idAttachment { get; set; }
            public object color { get; set; }
            public object idUploadedBackground { get; set; }
            public string size { get; set; }
            public string brightness { get; set; }
        }

    }
}
