using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class Testimonial : ICommunication
    {
        public long TestimonialId { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public int Status { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public bool ShowInWidget { get; set; }

        public void Validate()
        {
            Description = Utils.CutString(Description, 800);
            Picture = Utils.CutString(Picture, 250);
            Title = Utils.CutString(Title, 100);
            Name = Utils.CutString(Name, 100);
            Company = Utils.CutString(Company, 100);
        }
    }
}
