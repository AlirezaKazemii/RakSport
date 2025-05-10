namespace RakSport.Domain.Models.FAQ
{
    public class FAQEntity
    {
        public Guid Id { get; private set; }
        public string Question { get; private set; }
        public string Answer { get; private set; }
        public DateTime CreateAt { get; private set; }

    }
}   
