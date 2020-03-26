using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLayer
{
    public class Query : ICommunication
    {
        public long QueryId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public CLayer.ObjectStatus.MsgType MessageType { get; set; }


        public void Validate()
        {
            Name = Utils.CutString(Name, 100);
            Email = Utils.CutString(Email, 100);
            Phone = Utils.CutString(Phone, 15);
            Subject = Utils.CutString(Subject, 200);
            Body = Utils.CutString(Body, 1000);
        }
    }
}
