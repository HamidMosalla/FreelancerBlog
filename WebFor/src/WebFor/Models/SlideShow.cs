using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFor.Models
{
    public class SlideShow
    {
        public int SlideShowId { get; set; }
        public DateTime SlideShowDateCreated { get; set; }
        public int SlideShowPriority { get; set; }
        public string SlideShowTitle { get; set; }
        public string SlideShowDescription { get; set; }
        public string SlideShowPictrure { get; set; }
        public string SlideShowLink { get; set; }
    }
}
