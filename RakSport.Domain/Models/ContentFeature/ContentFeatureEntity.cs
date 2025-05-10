using RakSport.Domain.Models.ContentSection;

namespace RakSport.Domain.Models.ContentFeature
{
    public class ContentFeatureEntity
    {
        public Guid Id { get; private set; }
        public Guid ContentSectionId { get; private set; }
        public string Text { get; private set; }

        // Navigation Properties
        public ContentSectionEntity ContentSection { get; private set; }
    }
}
