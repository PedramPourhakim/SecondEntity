using _0_Framework.Domain;
using ShopManagement.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.CommentAgg
{
    public class Comment :EntityBase
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Message { get; private set; }
        public bool IsConfirmed { get; private set; }
        public bool IsCanceled { get; private set; }
        public long ProductId { get; private set; }
        public Product Product { get; set; }
        public Comment(string Name,string Email,
            string Message,long ProductId)
        {
            this.Name = Name;
            this.Email = Email;
            this.Message = Message;
            this.ProductId = ProductId;
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
