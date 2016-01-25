namespace WebFor.Core.Domain
{
    public class SiteOrder
    {
        public int SiteOrderId { get; set; }
        public string SiteOrderFullName { get; set; }
        public string SiteOrderEmail { get; set; }
        public string SiteOrderPhone { get; set; }
        public string SiteOrderDesc { get; set; }
        public decimal? SiteOrderFinalPrice { get; set; }
        public string SiteOrderExample { get; set; }
        public string SiteOrderHowFindUs { get; set; }
        public int? SiteOrderStaticPageNumber { get; set; }
        public bool? SiteOrderIsDynamic { get; set; }
        public bool? SiteOrderDoesHaveDatabase { get; set; }
        public bool? SiteOrderIsResponsive { get; set; }
        public bool? SiteOrderDoesHaveSiteMap { get; set; }
        public bool? SiteOrderDoesHaveSeo { get; set; }
        public bool? SiteOrderDoesHaveRegistration { get; set; }
        public bool? SiteOrderDoesHaveExternalAuth { get; set; }
        public bool? SiteOrderDoesSourceCodeIncluded { get; set; }
        public bool? SiteOrderDoesSuppoerIncluded { get; set; }
        public bool? SiteOrderDoesHaveFileManager { get; set; }
        public bool? SiteOrderDoesHaveAdvancedHtmlEditor { get; set; }
        public bool? SiteOrderDoesUseAjaxLight { get; set; }
        public bool? SiteOrderDoesUseAjaxMedium { get; set; }
        public bool? SiteOrderDoesUseAjaxHeavy { get; set; }
        public bool? SiteOrderDoesHaveNewsLetter { get; set; }
        public bool? SiteOrderDoesHaveFeed { get; set; }
        public bool? SiteOrderIsCrossPlatform { get; set; }
        public bool? SiteOrderIsSinglePage { get; set; }
        public bool? SiteOrderDoesHaveSlideShow { get; set; }
        public bool? SiteOrderIsOptimizedForLightness { get; set; }
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
        public bool? SiteOrderDoesIncludeUnitTest { get; set; }
        public bool? SiteOrderDoesHaveShoppingCart { get; set; }
        public bool? SiteOrderDoesHaveShoppingCartWithoutRegister { get; set; }
        public bool? SiteOrderDoesHaveSearch { get; set; }
        public bool? SiteOrderDoesHaveSearchAjax { get; set; }
        public bool? SiteOrderDoesHaveMultipleCriteriaSearch { get; set; }
        public bool? SiteOrderDoesHaveCommenting { get; set; }
        public bool? SiteOrderDoesHaveDocumentation { get; set; }
        public bool? SiteOrderDoesHaveComplexFooter { get; set; }
        public bool? SiteOrderIsAsync { get; set; }
    }
}
