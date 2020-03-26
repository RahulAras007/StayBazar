using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Facebook;
using Owin;
using System.Configuration;

namespace StayBazar
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);


            //For googleplus integration
            //app.UseGoogleAuthentication (
            //    clientId: "320475075164-8mfueb58obfi6djdp2fmghuds8d18bbj.apps.googleusercontent.com",
            //    clientSecret: "1mpLRt829Utmb-816GgL3GFP");


            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");
            //app.UseGoogleAuthentication();
            var facebookAuthOptions = new FacebookAuthenticationOptions();
            facebookAuthOptions.AppId = ConfigurationManager.AppSettings.Get("AppId");
            facebookAuthOptions.AppSecret = ConfigurationManager.AppSettings.Get("AppSecret");
            facebookAuthOptions.Scope.Add("email");
            app.UseFacebookAuthentication(facebookAuthOptions);


            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = ConfigurationManager.AppSettings.Get("ClientId"),
                ClientSecret = ConfigurationManager.AppSettings.Get("ClientSecret")
            });
        }
    }
}