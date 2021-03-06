using _0_Framework.Domain;
using System;
using System.Collections.Generic;

namespace CommentManagement.Domain.CommentAgg
{
    public class Comment 
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Website { get; private set; }
        public string Message { get; private set; }
        public bool IsConfirmed { get; private set; }
        public bool IsCanceled { get; private set; }
        public long OwnerRecordID { get; private set; }
        public int Type { get; private set; }
        public long ParentID { get; private set; }
        public Comment Parent { get; private set; }

        public Comment(string Name,string Email,string Website,string Message,
            long OwnerRecordID,int Type,long ParentID)
        {
            this.Name = Name;
            this.Email = Email;
            this.Website = Website;
            this.Message = Message;
            this.OwnerRecordID = OwnerRecordID;
            this.Type = Type;
            this.ParentID = ParentID;
            CreationDate = DateTime.Now;
        }

        public void Confirm()
        {
            IsConfirmed = true;
        }

        public void Cancel()
        {
            IsCanceled = true;
        }
    }
}
