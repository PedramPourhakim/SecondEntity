using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Application
{
    public class OperationResult
    {
        public bool IsSucceeded { get; set; }
        public string Message { get; set; }
        public OperationResult()
        {
            IsSucceeded = false;
        }
        public OperationResult Succeeded(string Message="عملیات با موفقیت انجام شد")
        {
            IsSucceeded = true;
            this.Message = Message;
            return this;
        }
        public OperationResult Failed(string Message)
        {
            IsSucceeded = false;
            this.Message = Message;
            return this;
        }
    }
}
