using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ReviewProj.WebUI.Models;

namespace ReviewProj.WebUI.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Gets the current site session.
        /// </summary>
        public SiteSession CurrentSiteSession
        {
            get
            {
                SiteSession shopSession = (SiteSession)this.Session["SiteSession"];
                return shopSession;
            }
        }

        //protected override bool DisableAsyncSupport
        //{
        //    get { return true; }
        //}

        /// <summary>
        /// Manage the internationalization before to invokes the action in the current controller context.
        /// </summary>
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            int culture = 0;
            if (this.Session == null || this.Session["CurrentUICulture"] == null)
            {
                int.TryParse(System.Configuration.ConfigurationManager.AppSettings["Culture"], out culture);
                this.Session["CurrentUICulture"] = culture;
            }
            else
            {
                culture = (int)this.Session["CurrentUICulture"];
            }
            //
            SiteSession.CurrentUICulture = culture;
            //
            // Invokes the action in the current controller context.
            //
            //base.ExecuteCore();
        }
    }
}