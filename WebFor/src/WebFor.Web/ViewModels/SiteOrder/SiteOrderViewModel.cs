using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFor.Web.ViewModels.SiteOrder
{
    public class SiteOrderViewModel
    {
        public int SiteOrderId { get; set; }
        public string SiteOrderFullName { get; set; }
        public string SiteOrderEmail { get; set; }
        public string SiteOrderPhone { get; set; }
        public string SiteOrderDesc { get; set; }
        public DateTime SiteOrderDeadLineDate { get; set; }
        public string SiteOrderExample { get; set; }
        public string SiteOrderHowFindUs { get; set; }




        public string SiteOrderWebSiteType { get; set; }
        public int? SiteOrderNumberOfMockUp { get; set; }
        public string SiteOrderDevelopmentComplexity { get; set; }
        public int? SiteOrderStaticPageNumber { get; set; }
        public bool? SiteOrderDoesHaveDatabase { get; set; }


        public bool? SiteOrderDoesUseTemplates { get; set; }
        public bool? SiteOrderDoesUseCustomDesign { get; set; }
        public bool? SiteOrderIsResponsive { get; set; }
        public bool? SiteOrderIsOptimizedForMobile { get; set; }
        public bool? SiteOrderIsOptimizedForAccessibility { get; set; }
        public bool? SiteOrderIsOptimizedForLightness { get; set; }
        public bool? SiteOrderDoesWithOkWithJavascriptDisabled { get; set; }
        public bool? SiteOrderIsCrossPlatform { get; set; }
        public bool? SiteOrderDoesIncludeUnitTest { get; set; }
        public bool? SiteOrderIsAsync { get; set; }


        public bool? SiteOrderDoesHaveImageGallery { get; set; }

        public bool? SiteOrderDoesContentOnUs { get; set; }



        public bool? SiteOrderDoesHaveSeo { get; set; }
        public string SiteOrderSeoType { get; set; }
        public bool? SiteOrderDoesHaveSiteMap { get; set; }





        public bool? SiteOrderDoesUseGoogleMap { get; set; }
        public bool? SiteOrderDoesUseGoogleAnalytics { get; set; }
        public bool? SiteOrderDoesUseSocialMedia { get; set; }


        public bool? SiteOrderDoesHaveRegistration { get; set; }
        public bool? SiteOrderDoesHaveExternalAuth { get; set; }


        public bool? SiteOrderDoesSourceCodeIncluded { get; set; }
        public bool? SiteOrderDoesSupportIncluded { get; set; }
        public string SiteOrderSupportType { get; set; }


        public bool? SiteOrderDoesHaveFileManager { get; set; }
        public bool? SiteOrderDoesHaveAdvancedHtmlEditor { get; set; }


        public bool? SiteOrderDoesUseAjax { get; set; }
        public string SiteOrderDoesUseAjaxType { get; set; }


        public bool? SiteOrderDoesHaveNewsLetter { get; set; }
        public bool? SiteOrderDoesHaveFeed { get; set; }
        public bool? SiteOrderDoesVideoPlaying { get; set; }
        public bool? SiteOrderDoesHaveForum { get; set; }
        public bool? SiteOrderDoesHaveFaq { get; set; }






        public bool? SiteOrderIsSinglePage { get; set; }
        public bool? SiteOrderDoesHaveSlideShow { get; set; }

        public bool? SiteOrderDoesSupportTagging { get; set; }
        public bool? SiteOrderDoesSupportCategory { get; set; }


        public bool? SiteOrderDoesHaveSsl { get; set; }
        public bool? SiteOrderDoesIncludeDomainAndHosting { get; set; }


        public bool? SiteOrderDoesIncludeUserArea { get; set; }
        public bool? SiteOrderDoesHaveUserProfile { get; set; }
        public bool? SiteOrderDoesHaveAdminSection { get; set; }
        public bool? SiteOrderDoesAdminManageUsers { get; set; }


        public bool? SiteOrderDoesIncludeChart { get; set; }
        public bool? SiteOrderDoesIncludeDynamicChart { get; set; }
        public bool? SiteOrderDoesIncludeAdvancedReport { get; set; }



        public bool? SiteOrderDoesHaveShoppingCart { get; set; }
        public bool? SiteOrderDoesHaveShoppingCartWithoutRegister { get; set; }


        public bool? SiteOrderDoesHaveSearch { get; set; }
        public string SiteOrderSearchType { get; set; }


        public bool? SiteOrderDoesHaveCommenting { get; set; }
        public bool? SiteOrderDoesHaveRatinging { get; set; }


        public bool? SiteOrderDoesHaveDocumentation { get; set; }
        public bool? SiteOrderDoesHaveComplexFooter { get; set; }





        public decimal? SiteOrderFinalPrice { get; set; }
    }
}
