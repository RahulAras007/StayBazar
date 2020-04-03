using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class PropertyMostpopular
    {
        public static List<CLayer.Property> SpecialOffers(int PropertyStatus, int UserStstus, int Limit)
        {
            DataLayer.PropertyMostpopular property = new DataLayer.PropertyMostpopular();
            return property.SpecialOffers(PropertyStatus, UserStstus, Limit);
        }
        public static List<CLayer.Property> MostpopularGet(int PropertyStatus, int UserStstus, int Limit)
        {
            DataLayer.PropertyMostpopular property = new DataLayer.PropertyMostpopular();
            return property.MostpopularGet(PropertyStatus, UserStstus, Limit);
        }
        public static List<CLayer.Property> MostpopularForb2bGet(int PropertyStatus, int UserStstus, int Limit)
        {
            DataLayer.PropertyMostpopular property = new DataLayer.PropertyMostpopular();
            return property.MostpopularForb2bGet(PropertyStatus, UserStstus, Limit);
        }
        public static List<CLayer.Property> NewlyCreatedList(int PropertyStatus, int UserStstus, int Limit)
        {
            DataLayer.PropertyMostpopular property = new DataLayer.PropertyMostpopular();
            return property.NewlyCreatedList(PropertyStatus, UserStstus, Limit);
        }

        public static List<CLayer.Property> DealsofDay(int PropertyStatus, int UserStstus, int Limit)
        {
            DataLayer.PropertyMostpopular property = new DataLayer.PropertyMostpopular();
            return property.DealsofDay(PropertyStatus, UserStstus, Limit);
        }
        public static List<CLayer.Property> LongStayDels(int PropertyStatus, int UserStstus, int Limit)
        {
            DataLayer.PropertyMostpopular property = new DataLayer.PropertyMostpopular();
            return property.LongStayDels(PropertyStatus, UserStstus, Limit);
        }

        public static List<CLayer.Property> InterestedPropertiesGet(long PropertyId,int PropertyStatus, int UserStstus, int Limit)
        {
            DataLayer.PropertyMostpopular property = new DataLayer.PropertyMostpopular();
            return property.InterestedPropertiesGet(PropertyId,PropertyStatus, UserStstus, Limit);
        }
        public static List<CLayer.Property> MostpopularGetWithGDS(int PropertyStatus, int UserStstus, int Limit)
        {
            DataLayer.PropertyMostpopular property = new DataLayer.PropertyMostpopular();
            return property.MostpopularGetWithGDS(PropertyStatus, UserStstus, Limit);
        }
        public static List<CLayer.Property> InterestedPropertiesGetWithGDS(long PropertyId, int PropertyStatus, int UserStstus, int Limit)
        {
            DataLayer.PropertyMostpopular property = new DataLayer.PropertyMostpopular();
            return property.InterestedPropertiesGetWithGDS(PropertyId, PropertyStatus, UserStstus, Limit);
        }
        //PreferedProperties
        public static List<CLayer.Property> PreferedProperties(string Destination, string Userid)
        {
            DataLayer.PropertyMostpopular property = new DataLayer.PropertyMostpopular();
            return property.PreferedProperties(Destination, Userid);
        }




        public static List<CLayer.Property> GetProperty(long propertyId)
        {
            //long propertyId=  feedback.PropertyId;
            DataLayer.PropertyMostpopular property = new DataLayer.PropertyMostpopular();
            return property.ProprtyGet(propertyId);
        }
    }
}
