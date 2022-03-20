using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Application
{
    public class OperationResult
    {
        public bool Movafagh { get; set; }
        public string Message { get; set; }
        public OperationResult()
        {
            Movafagh = false;
        }
        public OperationResult Succeeded(string Message="عملیات با موفقیت انجام شد")
        {
            Movafagh = true;
            this.Message = Message;
            return this;
        }
        public OperationResult Failed(string Message)
        {
            Movafagh = false;
            this.Message = Message;
            return this;
        }
    }
}
