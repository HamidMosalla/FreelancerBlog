using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Core.Services.SiteOrderServices;
using WebFor.Infrastructure.Services.SiteOrderServices;

namespace WebFor.Infrastructure.Services.SiteOrderServices
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
                new PriceSpec {EnName = "SiteOrderWebSiteType", FaName = "نوع وب سایت", Price = 456},
                new PriceSpec {EnName = "SiteOrderNumberOfMockUp", FaName = "تعداد طرح های پیش ساخت", Price = 1231},
                new PriceSpec {EnName = "SiteOrderDevelopmentComplexity", FaName = "پیچیدگی ساخت", Price = 456},
                new PriceSpec {EnName = "SiteOrderStaticPageNumber", FaName = "تعداد صفحات استاتیک", Price = 45},
                new PriceSpec {EnName = "SiteOrderDoesHaveDatabase", FaName = "داشتن بانک اطلاعاتی", Price = 5},
                new PriceSpec {EnName = "SiteOrderDoesHaveShoppingCart", FaName = "داشتن سبد خرید", Price = 456},
                new PriceSpec {EnName = "SiteOrderDoesHaveShoppingCartWithoutRegister", FaName = "سبد خرید بدون ثبت نام", Price = 123},
                new PriceSpec {EnName = "SiteOrderDoesHaveBlog", FaName = "داشتن بلاگ", Price = 123},
                new PriceSpec {EnName = "SiteOrderDoesContentOnUs", FaName = "تولید محتوا", Price = 456},
                new PriceSpec {EnName = "SiteOrderSupportType", FaName = "نوع پشتیبانی", Price = 4444},

                new PriceSpec {EnName = "SiteOrderDoesUseTemplates", FaName = "استفاده از قالب آماده", Price = 45612},
                new PriceSpec {EnName = "SiteOrderDoesUseCustomDesign", FaName = "استفاده از قالب دست ساز", Price = 1231456},
                new PriceSpec {EnName = "SiteOrderIsResponsive", FaName = "دیزاین واکنش گرا", Price = 456456},
                new PriceSpec {EnName = "SiteOrderIsOptimizedForMobile", FaName = "بهینه سازی برای موبایل", Price = 123},
                new PriceSpec {EnName = "SiteOrderIsOptimizedForAccessibility", FaName = "بهینه سازی دسترسی", Price = 456},
                new PriceSpec {EnName = "SiteOrderIsOptimizedForLightness", FaName = "بهینه سازی برای سبکی", Price = 1231},
                new PriceSpec {EnName = "SiteOrderDoesWithOkWithJavascriptDisabled", FaName = "کار کردن بدون جاوا اسکریپت", Price = 45645},
                new PriceSpec {EnName = "SiteOrderDoesHaveSeo", FaName = "بهینه سازی برای موتورهای جستجو", Price = 4564},
                new PriceSpec {EnName = "SiteOrderSeoType", FaName = "نوع بهینه سازی", Price = 456456},
                new PriceSpec {EnName = "SiteOrderDoesHaveSiteMap", FaName = "نقشه سایت", Price = 45645645},

                new PriceSpec {EnName = "SiteOrderIsCrossPlatform", FaName = "کراسپلتفرم", Price = 456456},
                new PriceSpec {EnName = "SiteOrderDoesIncludeUnitTest", FaName = "یونیت تست", Price = 45644},
                new PriceSpec {EnName = "SiteOrderIsAsync", FaName = "غیر همزمان بودن توابع", Price = 23},
                new PriceSpec {EnName = "SiteOrderDoesConformToSolidDesign", FaName = "پرنسیب سالید دیزان", Price = 111},
                new PriceSpec {EnName = "SiteOrderIsSinglePage", FaName = "وب سایت تک صفحه ای", Price = 112300},
                new PriceSpec {EnName = "SiteOrderDoesUseAjax", FaName = "اسفاده از آژاکس", Price = 102210},
                new PriceSpec {EnName = "SiteOrderDoesUseAjaxType", FaName = "شدت استفاده از آژاکس", Price = 1050},
                new PriceSpec {EnName = "SiteOrderDoesHaveSsl", FaName = "استفاده از اس اس ال", Price = 1500},
                new PriceSpec {EnName = "SiteOrderDoesSourceCodeIncluded", FaName = "تحویل با سورس کد", Price = 145600},
                new PriceSpec {EnName = "SiteOrderDoesHaveDocumentation", FaName = "مستند سازی", Price = 456},

                new PriceSpec {EnName = "SiteOrderDoesHaveRegistration", FaName = "قابلیت ثبت نام", Price = 444},
                new PriceSpec {EnName = "SiteOrderDoesHaveExternalAuth", FaName = "ورود بدون ثبت نام با اوآث", Price = 1400},
                new PriceSpec {EnName = "SiteOrderDoesIncludeUserArea", FaName = "بخش کاربری", Price = 1600},
                new PriceSpec {EnName = "SiteOrderDoesHaveUserProfile", FaName = "بخش پروفایل کاربری", Price = 1040},
                new PriceSpec {EnName = "SiteOrderDoesHaveAdminSection", FaName = "داشتن بخش مدیریت", Price = 1500},
                new PriceSpec {EnName = "SiteOrderDoesAdminManageUsers", FaName = "بخش مدیریت کاربران", Price = 100},
                new PriceSpec {EnName = "SiteOrderDoesHaveFileManager", FaName = "مدیریت فایل", Price = 100},
                new PriceSpec {EnName = "SiteOrderDoesHaveImageGallery", FaName = "گالری عکس", Price = 1500},
                new PriceSpec {EnName = "SiteOrderDoesHaveAdvancedHtmlEditor", FaName = "ادیتور اچ تی ام ال پیشرفته", Price = 15600},
                new PriceSpec {EnName = "SiteOrderDoesHaveSlideShow", FaName = "سلاید شو", Price = 1050},

                new PriceSpec {EnName = "SiteOrderDoesSupportTagging", FaName = "قابلیت تگ گذاری", Price = 10640},
                new PriceSpec {EnName = "SiteOrderDoesSupportCategory", FaName = "قابلیت دسته بندی", Price = 1050},
                new PriceSpec {EnName = "SiteOrderDoesHaveCommenting", FaName = "قابلیت کامنت گذاری", Price = 10780},
                new PriceSpec {EnName = "SiteOrderDoesHaveRatinging", FaName = "قابلیت امتیاز دهی", Price = 10037},
                new PriceSpec {EnName = "SiteOrderDoesHaveNewsLetter", FaName = "قابلیت خبرنامه", Price = 1050},
                new PriceSpec {EnName = "SiteOrderDoesHaveFeed", FaName = "قابلیت فید خوان", Price = 1050},
                new PriceSpec {EnName = "SiteOrderDoesVideoPlaying", FaName = "قابلیت پخش ویدئو", Price = 1400},
                new PriceSpec {EnName = "SiteOrderDoesHaveForum", FaName = "قابلیت فوروم", Price = 14600},
                new PriceSpec {EnName = "SiteOrderDoesHaveSearch", FaName = "قابلیت جستجو", Price = 145600},
                new PriceSpec {EnName = "SiteOrderSearchType", FaName = "نوع جستجو", Price = 1040},

                new PriceSpec {EnName = "SiteOrderDoesUseGoogleMap", FaName = "استفاده از گوگل مپ", Price = 1456700},
                new PriceSpec {EnName = "SiteOrderDoesUseGoogleAnalytics", FaName = "استفاده از گوگل آنالیتیک", Price = 107640},
                new PriceSpec {EnName = "SiteOrderDoesUseSocialMedia", FaName = "راه اندازی سوشال مدیا", Price = 100},
                new PriceSpec {EnName = "SiteOrderDoesIncludeChart", FaName = "استفاده از چارت", Price = 10440},
                new PriceSpec {EnName = "SiteOrderDoesIncludeDynamicChart", FaName = "استفاده از چارت دینامیک", Price = 145600},
                new PriceSpec {EnName = "SiteOrderDoesIncludeAdvancedReport", FaName = "قابلیت گزارش گیری پیشرفته", Price = 1070},
                new PriceSpec {EnName = "SiteOrderDoesIncludeDomainAndHosting", FaName = "اضافه کردن هزینه هاست و دامین", Price = 11456},
                new PriceSpec {EnName = "SiteOrderDoesSupportIncluded", FaName = "ساپورت بعد از تحویل", Price = 188900},
                new PriceSpec {EnName = "SiteOrderDoesHaveFaq", FaName = "بخش سوالات تکراری", Price = 104564440},
                new PriceSpec {EnName = "SiteOrderDoesHaveComplexFooter", FaName = "فوتر پیشرفته", Price = 104564450}
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
