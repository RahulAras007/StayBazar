using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class Query
    {
        public static List<CLayer.Query> GetAll(CLayer.ObjectStatus.MsgType messageType)
        {
            DataLayer.Query query = new DataLayer.Query();
            return query.GetAll((int) messageType);
        }

        public static List<CLayer.Query> GetArchive(CLayer.ObjectStatus.MsgType messageType)
        {
            DataLayer.Query query = new DataLayer.Query();
            return query.GetArchive((int)messageType);
        }

        public static int Save(CLayer.Query querydata)
        {
            DataLayer.Query query = new DataLayer.Query();
            querydata.Validate();
            return query.Save(querydata);
        }

        public static int Savefeedback(string email, string feedback)
        {
            DataLayer.Query query = new DataLayer.Query();
            return query.Savefeedback(email, feedback);
        }



        public static int SaveQueryForm(CLayer.FormSubmitcs data)
        {
            DataLayer.Query query = new DataLayer.Query();
            return query.SaveQueryForm(data);
        }

        public static void Delete(int QueryId)
        {
            DataLayer.Query query = new DataLayer.Query();
            query.Delete(QueryId);
            return;
        }
        public static void Delete(string QueryIds)
        {
            DataLayer.Query query = new DataLayer.Query();
            query.Delete(QueryIds);
            return;
        }

        public static CLayer.Query Get(int QueryId)
        {
            DataLayer.Query query = new DataLayer.Query();
            return query.Get(QueryId);
        }

        public static void SetStatus(int QueryId, int Status)
        {
            DataLayer.Query query = new DataLayer.Query();
            query.SetStatus(QueryId, Status);
            return;
        }

        public static void SetStatus(string QueryIds, int Status)
        {
            DataLayer.Query query = new DataLayer.Query();
            query.SetStatus(QueryIds, Status);
            return;
        }
    }
}
