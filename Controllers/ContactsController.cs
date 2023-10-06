using CritoAgency.Models;
using CritoAgency.Services;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace CritoAgency.Controllers
{
    public class ContactsController : SurfaceController
    {
        public ContactsController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
        }

        [HttpPost]
        public IActionResult Index(ContactForm contactForm)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            //Send email
            using var mail = new MailService("no-reply@always-reply.com","hjalpmighjalp@websupport.se", 671, "umbraco.Linus@se.com","********");

            //To Sender
            mail.SendAsync(contactForm.Email, "Your Request was recieved", "Hi, your request was recieved and we wont be contacting you").ConfigureAwait(false);

            //Reciever
            mail.SendAsync("orreBorre@umbraco.com",$"{contactForm.Name} sent a request", contactForm.Message).ConfigureAwait(false);



            return LocalRedirect(contactForm.RedirectUrl ?? "/");
        }
    }
}
