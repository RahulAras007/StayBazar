using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class Testimonial
    {
        public static List<CLayer.Testimonial> GetAll(int status)
        {
            DataLayer.Testimonial testimonial = new DataLayer.Testimonial();
            return testimonial.GetAll(status);
        }

        public static List<CLayer.Testimonial> GetForWidget()
        {
            DataLayer.Testimonial testimonial = new DataLayer.Testimonial();
            return testimonial.GetForWidget();
        }

        public static int Save(CLayer.Testimonial testimonialdata)
        {
            DataLayer.Testimonial testimonial = new DataLayer.Testimonial();
            testimonialdata.Validate();
            return testimonial.Save(testimonialdata);
        }

        public static void Delete(int TestimonialId)
        {
            DataLayer.Testimonial testimonial = new DataLayer.Testimonial();
            testimonial.Delete(TestimonialId);
        }

        public static void Delete(string TestimonialIds)
        {
            DataLayer.Testimonial testimonial = new DataLayer.Testimonial();
            testimonial.Delete(TestimonialIds);
            return;
        }

        public static CLayer.Testimonial Get(int TestimonialId)
        {
            DataLayer.Testimonial testimonial = new DataLayer.Testimonial();
            return testimonial.Get(TestimonialId);
        }

        public static void SetToShowInWidget(int TestimonialId, bool Status)
        {
            DataLayer.Testimonial testimonial = new DataLayer.Testimonial();
            testimonial.SetToShowInWidget(TestimonialId, Status);
            return;
        }

        public static void SetStatus(int TestimonialId, int Status)
        {
            DataLayer.Testimonial testimonial = new DataLayer.Testimonial();
            testimonial.SetStatus(TestimonialId, Status);
            return;
        }

        public static void SetStatus(string TestimonialIds, int Status)
        {
            DataLayer.Testimonial testimonial = new DataLayer.Testimonial();
            testimonial.SetStatus(TestimonialIds, Status);
            return;
        }
    }
}
