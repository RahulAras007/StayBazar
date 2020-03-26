using System;


namespace CLayer
{
    public class AccommodationType : ICommunication
    {
        public int AccommodationTypeId { get; set; }
        public string Title { get; set; }
        public bool CanDelete { get; set; }


        public void Validate()
        {
            Title = Utils.CutString(Title, 50);
        }
    }
}
