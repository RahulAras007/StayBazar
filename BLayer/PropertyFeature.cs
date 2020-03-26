using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BLayer
{
    public class PropertyFeature
    {
        public static List<CLayer.PropertyFeature> GetAll()
        {
            DataLayer.PropertyFeature propertyfeature = new DataLayer.PropertyFeature();
            return propertyfeature.GetAll();
        }

        public static List<CLayer.PropertyFeature> GetAllSearch()
        {
            DataLayer.PropertyFeature propertyfeature = new DataLayer.PropertyFeature();
            return propertyfeature.GetAllSearch();
        }
        public static int Save(CLayer.PropertyFeature propertyfeaturedata)
        {
            DataLayer.PropertyFeature propertyfeature = new DataLayer.PropertyFeature();
            propertyfeaturedata.Validate();
            return propertyfeature.Save(propertyfeaturedata);
        }
        public static int SavePropertyFeature(CLayer.PropertyFeature data)
        {
            DataLayer.PropertyFeature propertyfeature = new DataLayer.PropertyFeature();
            return propertyfeature.SavePropertyFeature(data);
        }
        public static void Delete(int PropertyFeatureId)
        {
            DataLayer.PropertyFeature propertyfeature = new DataLayer.PropertyFeature();
            propertyfeature.Delete(PropertyFeatureId);
        }
        public static void DeleteFeatureOnProperty(long propertyid)
        {
            DataLayer.PropertyFeature propertyfeature = new DataLayer.PropertyFeature();
            propertyfeature.DeleteFeatureOnProperty(propertyid);
        }

        public static CLayer.PropertyFeature Get(int PropertyFeatureId)
        {
            DataLayer.PropertyFeature propertyfeature = new DataLayer.PropertyFeature();
            return propertyfeature.Get(PropertyFeatureId);
        }

        public static List<CLayer.PropertyFeature> GetAllWithSelectedForProperty(long propertyId)
        {
            DataLayer.PropertyFeature propertyfeature = new DataLayer.PropertyFeature();
            return propertyfeature.GetAllWithSelectedForProperty(propertyId);
        }
        public static List<CLayer.PropertyFeature> GetFeaturesForProperty(long propertyId)
        {
            DataLayer.PropertyFeature propertyfeature = new DataLayer.PropertyFeature();
            return propertyfeature.GetFeaturesForProperty(propertyId);
        }
        public static DataTable SaveGDSFeatures(List<string> codes)
        {
            DataLayer.PropertyFeature propertyfeature = new DataLayer.PropertyFeature();
            return propertyfeature.SaveGDSFeatures(codes);
        }
        public static DataTable SaveGDSFeatureProperties(List<string> codes)
        {
            DataLayer.PropertyFeature propertyfeature = new DataLayer.PropertyFeature();
            return propertyfeature.SaveGDSFeatureProperties(codes);
        }
    }
}
