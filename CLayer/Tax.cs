using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class Tax : ICommunication
    {

        public int TaxId { get; set; }
        public int TaxTitleId { get; set; }     
        public string Title { get; set; }     
        public decimal Rate { get; set; }

        public int? CountryId { get; set; }
        public string Country { get; set; }

        public int? StateId { get; set; }
        public string State { get; set; }

        public int? CityId { get; set; }
        public string City { get; set; }
        
        public string Notes { get; set; }
        public int Status { get; set; }
        public DateTime StartDate { get; set; }    
        public DateTime EndDate { get; set; }  
        public int OnDate { get; set; }       
        public bool OnTotalAmount { get; set; }
        public bool Unlimited { get; set; } 
        public decimal PriceUpto { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
      
        public bool IsStandard { get; set; }
        public string Description { get; set; }

        public long AccommodationId { get; set; }
        public long PropertyId { get; set; }
        public int StayCategoryId { get; set; }
        public string Ids { get; set; }
        public string PropertyTitle { get; set; }
        public long BookingId { get; set; }
        public decimal TotalTaxAmount { get; set; }

        public decimal TotalCGSTTaxAmount { get; set; }
        public decimal TotalSGSTTaxAmount { get; set; }
        public decimal TotalIGSTTaxAmount { get; set; }
        public string  CGSTTitle { get; set; }
        public string SGSTTitle { get; set; }
        public string IGSTTitle { get; set; }
        public string BookingCode { get; set; }
        public Double TaxRate { get; set; }

        public Double CGSTTaxRate { get; set; }
        public Double SGSTTaxRate { get; set; }
        public Double IGSTTaxRate { get; set; }

        public int BookingType { get; set; }
        public Decimal GDSAmount { get; set; }


        public void Validate()
        {
            Title = Utils.CutString(Title, 25);
        }
    }
}
