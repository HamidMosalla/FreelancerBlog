using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FreelancerBlog.Core.Services.SiteOrderServices;

namespace FreelancerBlog.Services.SiteOrderServices
{
    public class PriceSpec
    {
        public string EnName { get; set; }
        public string FaName { get; set; }
        public decimal Price { get; set; }
        public object Value { get; set; }
    }

    public class PriceSpecCollectionFactory : IPriceSpecCollectionFactory<PriceSpec, object>
    {
        public List<PriceSpec> BuildPriceSpecCollection(object viewModel)
        {
            var listOfSiteOrder = new List<PriceSpec>
            {
                new PriceSpec {EnName = "SiteOrderWebSiteType", FaName = "نوع وب سایت", Price = 1},
                new PriceSpec {EnName = "SiteOrderNumberOfMockUp", FaName = "تعداد طرح های پیش ساخت", Price = 50000},
                new PriceSpec {EnName = "SiteOrderDevelopmentComplexity", FaName = "پیچیدگی ساخت", Price = 1},
                new PriceSpec {EnName = "SiteOrderStaticPageNumber", FaName = "تعداد صفحات استاتیک", Price = 80000},
                new PriceSpec {EnName = "SiteOrderDoesHaveDatabase", FaName = "داشتن بانک اطلاعاتی", Price = 300000},
                new PriceSpec {EnName = "SiteOrderDoesHaveShoppingCart", FaName = "داشتن سبد خرید", Price = 900000},
                new PriceSpec {EnName = "SiteOrderDoesHaveShoppingCartWithoutRegister", FaName = "سبد خرید بدون ثبت نام", Price = 400000},
                new PriceSpec {EnName = "SiteOrderDoesHaveBlog", FaName = "داشتن بلاگ", Price = 1000000},
                new PriceSpec {EnName = "SiteOrderDoesContentOnUs", FaName = "تولید محتوا", Price = 500000},
                new PriceSpec {EnName = "SiteOrderSupportType", FaName = "نوع پشتیبانی", Price = 1},

                new PriceSpec {EnName = "SiteOrderDoesUseTemplates", FaName = "استفاده از قالب آماده", Price = 100000},
                new PriceSpec {EnName = "SiteOrderDoesUseCustomDesign", FaName = "استفاده از قالب دست ساز", Price = 800000},
                new PriceSpec {EnName = "SiteOrderIsResponsive", FaName = "دیزاین واکنش گرا", Price = 500000},
                new PriceSpec {EnName = "SiteOrderIsOptimizedForMobile", FaName = "بهینه سازی برای موبایل", Price = 600000},
                new PriceSpec {EnName = "SiteOrderIsOptimizedForAccessibility", FaName = "بهینه سازی دسترسی", Price = 500000},
                new PriceSpec {EnName = "SiteOrderIsOptimizedForLightness", FaName = "بهینه سازی برای سبکی", Price = 600000},
                new PriceSpec {EnName = "SiteOrderDoesWithOkWithJavascriptDisabled", FaName = "کار کردن بدون جاوا اسکریپت", Price = 800000},
                new PriceSpec {EnName = "SiteOrderDoesHaveSeo", FaName = "بهینه سازی برای موتورهای جستجو", Price = 400000},
                new PriceSpec {EnName = "SiteOrderSeoType", FaName = "نوع بهینه سازی", Price = 1},
                new PriceSpec {EnName = "SiteOrderDoesHaveSiteMap", FaName = "نقشه سایت", Price = 200000},

                new PriceSpec {EnName = "SiteOrderIsCrossPlatform", FaName = "کراسپلتفرم", Price = 1200000},
                new PriceSpec {EnName = "SiteOrderDoesIncludeUnitTest", FaName = "یونیت تست", Price = 1500000},
                new PriceSpec {EnName = "SiteOrderIsAsync", FaName = "غیر همزمان بودن توابع", Price = 500000},
                new PriceSpec {EnName = "SiteOrderDoesConformToSolidDesign", FaName = "پرنسیب سالید دیزان", Price = 800000},
                new PriceSpec {EnName = "SiteOrderIsSinglePage", FaName = "وب سایت تک صفحه ای", Price = 4000000},
                new PriceSpec {EnName = "SiteOrderDoesUseAjax", FaName = "اسفاده از آژاکس", Price = 400000},
                new PriceSpec {EnName = "SiteOrderDoesUseAjaxType", FaName = "شدت استفاده از آژاکس", Price = 1},
                new PriceSpec {EnName = "SiteOrderDoesHaveSsl", FaName = "استفاده از اس اس ال", Price = 200000},
                new PriceSpec {EnName = "SiteOrderDoesSourceCodeIncluded", FaName = "تحویل با سورس کد", Price = 1},
                new PriceSpec {EnName = "SiteOrderDoesHaveDocumentation", FaName = "مستند سازی", Price = 600000},

                new PriceSpec {EnName = "SiteOrderDoesHaveRegistration", FaName = "قابلیت ثبت نام", Price = 200000},
                new PriceSpec {EnName = "SiteOrderDoesHaveExternalAuth", FaName = "ورود بدون ثبت نام با اوآث", Price = 400000},
                new PriceSpec {EnName = "SiteOrderDoesIncludeUserArea", FaName = "بخش کاربری", Price = 600000},
                new PriceSpec {EnName = "SiteOrderDoesHaveUserProfile", FaName = "بخش پروفایل کاربری", Price = 370000},
                new PriceSpec {EnName = "SiteOrderDoesHaveAdminSection", FaName = "داشتن بخش مدیریت", Price = 600000},
                new PriceSpec {EnName = "SiteOrderDoesAdminManageUsers", FaName = "بخش مدیریت کاربران", Price = 400000},
                new PriceSpec {EnName = "SiteOrderDoesHaveFileManager", FaName = "مدیریت فایل", Price = 600000},
                new PriceSpec {EnName = "SiteOrderDoesHaveImageGallery", FaName = "گالری عکس", Price = 380000},
                new PriceSpec {EnName = "SiteOrderDoesHaveAdvancedHtmlEditor", FaName = "ادیتور اچ تی ام ال پیشرفته", Price = 100000},
                new PriceSpec {EnName = "SiteOrderDoesHaveSlideShow", FaName = "سلاید شو", Price = 250000},

                new PriceSpec {EnName = "SiteOrderDoesSupportTagging", FaName = "قابلیت تگ گذاری", Price = 500000},
                new PriceSpec {EnName = "SiteOrderDoesSupportCategory", FaName = "قابلیت دسته بندی", Price = 390000},
                new PriceSpec {EnName = "SiteOrderDoesHaveCommenting", FaName = "قابلیت کامنت گذاری", Price = 400000},
                new PriceSpec {EnName = "SiteOrderDoesHaveRatinging", FaName = "قابلیت امتیاز دهی", Price = 200000},
                new PriceSpec {EnName = "SiteOrderDoesHaveNewsLetter", FaName = "قابلیت خبرنامه", Price = 460000},
                new PriceSpec {EnName = "SiteOrderDoesHaveFeed", FaName = "قابلیت فید خوان", Price = 280000},
                new PriceSpec {EnName = "SiteOrderDoesVideoPlaying", FaName = "قابلیت پخش ویدئو", Price = 380000},
                new PriceSpec {EnName = "SiteOrderDoesHaveForum", FaName = "قابلیت فوروم", Price = 300000},
                new PriceSpec {EnName = "SiteOrderDoesHaveSearch", FaName = "قابلیت جستجو", Price = 370000},
                new PriceSpec {EnName = "SiteOrderSearchType", FaName = "نوع جستجو", Price = 1},

                new PriceSpec {EnName = "SiteOrderDoesUseGoogleMap", FaName = "استفاده از گوگل مپ", Price = 100000},
                new PriceSpec {EnName = "SiteOrderDoesUseGoogleAnalytics", FaName = "استفاده از گوگل آنالیتیک", Price = 700000},
                new PriceSpec {EnName = "SiteOrderDoesUseSocialMedia", FaName = "راه اندازی سوشال مدیا", Price = 300000},
                new PriceSpec {EnName = "SiteOrderDoesIncludeChart", FaName = "استفاده از چارت", Price = 250000},
                new PriceSpec {EnName = "SiteOrderDoesIncludeDynamicChart", FaName = "استفاده از چارت دینامیک", Price = 490000},
                new PriceSpec {EnName = "SiteOrderDoesIncludeAdvancedReport", FaName = "قابلیت گزارش گیری پیشرفته", Price = 600000},
                new PriceSpec {EnName = "SiteOrderDoesIncludeDomainAndHosting", FaName = "اضافه کردن هزینه هاست و دامین", Price = 200000},
                new PriceSpec {EnName = "SiteOrderDoesSupportIncluded", FaName = "ساپورت بعد از تحویل", Price = 250000},
                new PriceSpec {EnName = "SiteOrderDoesHaveFaq", FaName = "بخش سوالات تکراری", Price = 180000},
                new PriceSpec {EnName = "SiteOrderDoesHaveComplexFooter", FaName = "فوتر پیشرفته", Price = 250000}
            };


            var types = viewModel.GetType()
                .GetProperties()
                .Select(v => new { Name = v.Name.ToString(), Value = v.GetValue(viewModel) })
                // .Where(v => { if (v.Value == null || v.Value.ToString() == "0" || v.Value.ToString() == "False") return false;  return true; }
                .Where(v => v.Value != null && v.Value.ToString() != "0" && v.Value.ToString() != "False")
                .ToDictionary(v => v.Name, v => v.Value);


            return listOfSiteOrder.Join(types, l => l.EnName, t => t.Key, (l, t) => new PriceSpec { EnName = l.EnName, FaName = l.FaName, Price = l.Price, Value = t.Value }).ToList();
        }
    }
}
