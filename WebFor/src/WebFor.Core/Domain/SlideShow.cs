using System;

namespace WebFor.Core.Domain
{
    public class SlideShow
    {
        public int SlideShowId { get; set; }
        public DateTime SlideShowDateCreated { get; set; }
        public int? SlideShowPriority { get; set; }
        public string SlideShowTitle { get; set; }
        public string SlideShowBody { get; set; }
        public string SlideShowPictrure { get; set; }
        public string SlideShowLink { get; set; }
    }
}
