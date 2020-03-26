using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Testimonial : DataLink
    {
        public List<CLayer.Testimonial> GetAll(int status)
        {
            List<CLayer.Testimonial> testimonial = new List<CLayer.Testimonial>();

            DataTable dt = Connection.GetSQLTable("SELECT * FROM testimonial WHERE `Status`=" + status.ToString());
            foreach (DataRow dr in dt.Rows)
            {
                testimonial.Add(new CLayer.Testimonial()
                {
                    TestimonialId = Connection.ToLong(dr["TestimonialId"]),
                    Description = Connection.ToString(dr["Description"]),
                    Picture = Connection.ToString(dr["Picture"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    Title = Connection.ToString(dr["Title"]),
                    Name = Connection.ToString(dr["Name"]),
                    Company = Connection.ToString(dr["Company"]),
                    ShowInWidget = Connection.ToBoolean(dr["ShowInWidget"])
                });
            }

            return testimonial;
        }

        public List<CLayer.Testimonial> GetForWidget()
        {
            List<CLayer.Testimonial> testimonial = new List<CLayer.Testimonial>();
            DataTable dt = Connection.GetTable("testimonial_GetForWidget", null);
            foreach (DataRow dr in dt.Rows)
            {
                testimonial.Add(new CLayer.Testimonial()
                {
                    TestimonialId = Connection.ToLong(dr["TestimonialId"]),
                    Description = Connection.ToString(dr["Description"]),
                    Picture = Connection.ToString(dr["Picture"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    Title = Connection.ToString(dr["Title"]),
                    Name = Connection.ToString(dr["Name"]),
                    Company = Connection.ToString(dr["Company"])
                });
            }

            return testimonial;
        }

        public int Save(CLayer.Testimonial testimonial)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pTestimonialId", DataPlug.DataType._BigInt, testimonial.TestimonialId));
            param.Add(Connection.GetParameter("pDescription", DataPlug.DataType._Varchar, testimonial.Description));
            param.Add(Connection.GetParameter("pPicture", DataPlug.DataType._Varchar, testimonial.Picture));
            param.Add(Connection.GetParameter("pTitle", DataPlug.DataType._Varchar, testimonial.Title));
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, testimonial.Name));
            param.Add(Connection.GetParameter("pCompany", DataPlug.DataType._Varchar, testimonial.Company));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, testimonial.Status));
            param.Add(Connection.GetParameter("pShowInWidget", DataPlug.DataType._Bool, testimonial.ShowInWidget));
            int result = Connection.ExecuteQuery("testimonial_Save", param);
            return result;
        }

        public void Delete(int TestimonialId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pTestimonialId", DataPlug.DataType._BigInt, TestimonialId));
            Connection.ExecuteQuery("testimonial_Delete", param);
            return;
        }

        public CLayer.Testimonial Get(int TestimonialId)
        {
            CLayer.Testimonial testimonial = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pTestimonialId", DataPlug.DataType._BigInt, TestimonialId));
            DataTable dt = Connection.GetTable("testimonial_Get", param);
            if (dt.Rows.Count > 0)
            {
                testimonial = new CLayer.Testimonial();
                testimonial.TestimonialId = Connection.ToLong(dt.Rows[0]["TestimonialId"]);
                testimonial.Description = Connection.ToString(dt.Rows[0]["Description"]);
                testimonial.Picture = Connection.ToString(dt.Rows[0]["Picture"]);
                testimonial.Status = Connection.ToInteger(dt.Rows[0]["Status"]);
                testimonial.Title = Connection.ToString(dt.Rows[0]["Title"]);
                testimonial.Name = Connection.ToString(dt.Rows[0]["Name"]);
                testimonial.Company = Connection.ToString(dt.Rows[0]["Company"]);
                testimonial.ShowInWidget = Connection.ToBoolean(dt.Rows[0]["ShowInWidget"]);
            }
            return testimonial;
        }

        public void SetToShowInWidget(int TestimonialId, bool Status)
        {
            if (Status)
                Connection.ExecuteSqlQuery("UPDATE testimonial SET ShowInWidget = 1 WHERE TestimonialId=" + TestimonialId.ToString());
            else
                Connection.ExecuteSqlQuery("UPDATE testimonial SET ShowInWidget = 0 WHERE TestimonialId=" + TestimonialId.ToString());
            return;
        }

        public void Delete(string TestimonialIds)
        {
            Connection.ExecuteSqlQuery("DELETE FROM testimonial WHERE TestimonialId IN(" + TestimonialIds + ")");
            return;
        }

        public void SetStatus(int TestimonialId, int Status)
        {
            Connection.ExecuteSqlQuery("update testimonial set `Status`=" + Status.ToString() + " WHERE TestimonialId=" + TestimonialId.ToString());
            return;
        }

        public void SetStatus(string TestimonialIds, int Status)
        {
            Connection.ExecuteSqlQuery("update testimonial set `Status`=" + Status.ToString() + " WHERE TestimonialId IN(" + TestimonialIds + ")");
            return;
        }
    }
}
