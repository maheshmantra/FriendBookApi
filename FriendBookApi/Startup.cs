using System;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using SimpleInjector;
using FriendBookApi.FriendBookBAL;
using FriendBookApi.FriendBookDAL;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

[assembly: OwinStartup(typeof(FriendBookApi.Startup))]

namespace FriendBookApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            ConfigureOAuth(app);

            var container = GetContainer();
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);


        }
        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }

        public Container GetContainer() {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Register your types, for instance using the scoped lifestyle:
            container.Register<IFriendBookBAL, FriendBookBusinessLayer>(Lifestyle.Scoped);
            container.Register<IFriendBookDAL, FriendBookDataAccessLayer>(Lifestyle.Scoped);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify(); // Initialise container
            return container;
        }
    }
}
