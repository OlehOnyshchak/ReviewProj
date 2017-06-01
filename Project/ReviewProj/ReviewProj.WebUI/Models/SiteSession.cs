using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Globalization;
//
//using MvcBasic.Logic;


namespace ReviewProj.WebUI.Models
{
    /// <summary>
    /// Defines the site session.
    /// </summary>
    /// <remarks>
    /// Used to cache only the needed data for the current user!
    /// </remarks>
    public class SiteSession
    {
        public static int CurrentUICulture
        {
            get
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    return 1;
                else if (Thread.CurrentThread.CurrentUICulture.Name == "uk-UA")
                    return 2;
                else
                    return 0;
            }
            set
            {
                //
                // Set the thread's CurrentUICulture.
                //
                if (value == 1)
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                else if (value == 2)
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("uk-UA");
                else
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                //
                // Set the thread's CurrentCulture the same as CurrentUICulture.
                //
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
            }
        }
    }
}