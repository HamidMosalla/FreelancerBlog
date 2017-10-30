namespace FreelancerBlog.Core.Domain
{
    public class SiteOrder
    {

        public int SiteOrderId { get; set; }


        //preliminary info - step1
        public string SiteOrderWebSiteType { get; set; }
        public string SiteOrderNumberOfMockUp { get; set; }
        public string SiteOrderDevelopmentComplexity { get; set; }
        public int? SiteOrderStaticPageNumber { get; set; }
        public bool SiteOrderDoesHaveDatabase { get; set; }
        public bool SiteOrderDoesHaveShoppingCart { get; set; }
        public bool SiteOrderDoesHaveShoppingCartWithoutRegister { get; set; }
        public bool SiteOrderDoesHaveBlog { get; set; }
        public bool SiteOrderDoesContentOnUs { get; set; }
        public string SiteOrderSupportType { get; set; }


        //site design info - step2
        public bool SiteOrderDoesUseTemplates { get; set; }
        public bool SiteOrderDoesUseCustomDesign { get; set; }
        public bool SiteOrderIsResponsive { get; set; }
        public bool SiteOrderIsOptimizedForMobile { get; set; }
        public bool SiteOrderIsOptimizedForAccessibility { get; set; }
        public bool SiteOrderIsOptimizedForLightness { get; set; }
        public bool SiteOrderDoesWithOkWithJavascriptDisabled { get; set; }
        public bool SiteOrderDoesHaveSeo { get; set; }
        public string SiteOrderSeoType { get; set; }
        public bool SiteOrderDoesHaveSiteMap { get; set; }


        //structural info - step3
        public bool SiteOrderIsCrossPlatform { get; set; }
        public bool SiteOrderDoesIncludeUnitTest { get; set; }
        public bool SiteOrderIsAsync { get; set; }
        public bool SiteOrderDoesConformToSolidDesign { get; set; }
        public bool SiteOrderIsSinglePage { get; set; }
        public bool SiteOrderDoesUseAjax { get; set; }
        public string SiteOrderDoesUseAjaxType { get; set; }
        public bool SiteOrderDoesHaveSsl { get; set; }
        public bool SiteOrderDoesSourceCodeIncluded { get; set; }
        public bool SiteOrderDoesHaveDocumentation { get; set; }


        //site part info - step4
        public bool SiteOrderDoesHaveRegistration { get; set; }
        public bool SiteOrderDoesHaveExternalAuth { get; set; }
        public bool SiteOrderDoesIncludeUserArea { get; set; }
        public bool SiteOrderDoesHaveUserProfile { get; set; }
        public bool SiteOrderDoesHaveAdminSection { get; set; }
        public bool SiteOrderDoesAdminManageUsers { get; set; }
        public bool SiteOrderDoesHaveFileManager { get; set; }
        public bool SiteOrderDoesHaveImageGallery { get; set; }
        public bool SiteOrderDoesHaveAdvancedHtmlEditor { get; set; }
        public bool SiteOrderDoesHaveSlideShow { get; set; }


        //auxiliary feature info - step5
        public bool SiteOrderDoesSupportTagging { get; set; }
        public bool SiteOrderDoesSupportCategory { get; set; }
        public bool SiteOrderDoesHaveCommenting { get; set; }
        public bool SiteOrderDoesHaveRatinging { get; set; }
        public bool SiteOrderDoesHaveNewsLetter { get; set; }
        public bool SiteOrderDoesHaveFeed { get; set; }
        public bool SiteOrderDoesVideoPlaying { get; set; }
        public bool SiteOrderDoesHaveForum { get; set; }
        public bool SiteOrderDoesHaveSearch { get; set; }
        public string SiteOrderSearchType { get; set; }


        //misc feature info - step6
        public bool SiteOrderDoesUseGoogleMap { get; set; }
        public bool SiteOrderDoesUseGoogleAnalytics { get; set; }
        public bool SiteOrderDoesUseSocialMedia { get; set; }
        public bool SiteOrderDoesIncludeChart { get; set; }
        public bool SiteOrderDoesIncludeDynamicChart { get; set; }
        public bool SiteOrderDoesIncludeAdvancedReport { get; set; }
        public bool SiteOrderDoesIncludeDomainAndHosting { get; set; }
        public bool SiteOrderDoesSupportIncluded { get; set; }
        public bool SiteOrderDoesHaveFaq { get; set; }
        public bool SiteOrderDoesHaveComplexFooter { get; set; }


        //check out info - step7
        public string SiteOrderFullName { get; set; }
        public string SiteOrderEmail { get; set; }
        public string SiteOrderPhone { get; set; }
        public string SiteOrderDesc { get; set; }
        public string SiteOrderTimeToDeliverMonth { get; set; }
        public string SiteOrderExample { get; set; }
        public string SiteOrderHowFindUs { get; set; }


        //final price info
        public decimal? SiteOrderFinalPrice { get; set; }
    }
}
