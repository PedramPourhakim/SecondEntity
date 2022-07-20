using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts
{
    public  class PaymentMethod
    {
        public int Id { get;private set; }
        public string Name { get;private set; }
        public string Description { get;private set; }
        private PaymentMethod(int Id,string Name,
            string Description)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
        }
        public static List<PaymentMethod> GetList()
        {
            return new List<PaymentMethod>
            {
                new PaymentMethod(1,"پرداخت آنلاین",
                "وصل شدن به درگاه پرداخت اینترنتی و پرداخت مبلغ سفارش در لحظه."),
                new PaymentMethod(2,"پرداخت نقدی","در این مدل ابتدا سفارش ثبت شده و در زمان تحویل کالا پرداخت صورت خواهد گرفت.")
            };
        }
        public static  PaymentMethod GetBy(int id)
        {
            return GetList().FirstOrDefault(x => x.Id == id);
        }

    }
}
