using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFor.Web.ViewModels.Email
{
    public class IdentityTemplateViewModel
    {
        public string EmailPreviewMessage { get; set; }
        public string EmailMessageHeader { get; set; }
        public string EmailMessageBody { get; set; }
        public string CallBackLink { get; set; }
        public string CallBackLinkText { get; set; }

    }
}