using RakSport.Domain.Models.ContentFeature;
using RakSport.Domain.Models.ContentImage;
using RakSport.Domain.Models.SportHall;

namespace RakSport.Domain.Models.ContentSection
{
    public class ContentSectionEntity //مدیریت محتوای داینامیک برای صفحات سایت
    {
        public Guid Id { get; private set; }
        public Guid? SportHallId { get; private set; }
        public PageType Page { get; private set; } //نشون میده این محتوا مربوط به کدوم صفحه از سایته
        public string Title { get; private set; } //عنوان و متن اصلی محتوا که در سایت نمایش داده میشن 
        public string Description { get; private set; } //عنوان و متن اصلی محتوا که در سایت نمایش داده میشن


        // Navigation Properties
        public SportHallEntity? SportHall { get; private set; } //ممکنه محتوا برای غیر سالن باشه
        public ICollection<ContentImageEntity> Images { get; set; } = new List<ContentImageEntity>();
        //گالری تصاویر مربوط به این بخش از محتوا.

        public ICollection<ContentFeatureEntity> Features { get; set; } = new List<ContentFeatureEntity>();
        //موارد لیستی یا نکات مهم، مثلاً امکانات سالن یا بندهای قوانین.


        public enum PageType
        {
            HomePage,
            Rules,
            AboutAs,
            HallDetail
        }
    }
}
