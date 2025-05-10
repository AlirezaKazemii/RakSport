using RakSport.Domain.Models.ContentSection;

namespace RakSport.Domain.Models.ContentImage
{
    public class ContentImageEntity
    {
        public Guid Id { get; private set;}
        public Guid ContentSectionId { get; private set; }
        public string Url { get; private set; }

        // Navigation Properties
        public ContentSectionEntity ContentSection { get; private set; }
    }
}
