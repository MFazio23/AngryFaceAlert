using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace AngryFaceAlertApis.Filters
{
    public static class HashIt
    {
        public static string getHashSha256(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            return hash.Aggregate(string.Empty, (current, x) => current + $"{x:x2}");
        }
    }

    public class EmotionApiAuthFilter : AuthorizeAttribute
    {
        public string Key { get; set; }

        //skylineemotionapi
        public const string EMOTIONHASH = "fcff4e50321c7343b0d439a54e6e2538502f054a7c167729996cd6d12384aa84";
        public const string EMOTIONKEY = "skylineemotionapi";

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (AuthorizeRequest(actionContext))
            {
                return;
            }

            HandleUnauthorizedRequest(actionContext);
        }

        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext ctx)
        {
            ctx.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            //Code to handle unauthorized request

        }

        private bool AuthorizeRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {

            if (string.IsNullOrEmpty(actionContext.Request.Headers.Authorization.Parameter))
            {
                return false;
            }

            var hashedKey = HashIt.getHashSha256(EMOTIONKEY);

            if (hashedKey != actionContext.Request.Headers.Authorization.Parameter)
                return false;
            
            return true;

        }
    }

    public class FaceApiAuthFilter : AuthorizeAttribute
    {
        private const string FACEHASH = "6184ac07493cfbc9b62b724f1c2deedf1a089f2e2094f9a701e3611aefe6a013";
        private const string FACEKEY = "skylinefaceapi";

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (AuthorizeRequest(actionContext))
            {
                return;
            }

            HandleUnauthorizedRequest(actionContext);
        }

        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext ctx)
        {

            ctx.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

        }

        private bool AuthorizeRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {

            if (string.IsNullOrEmpty(actionContext.Request.Headers.Authorization.Parameter))
            {
                return false;
            }

            var hashedKey = HashIt.getHashSha256(FACEKEY);

            if (hashedKey != actionContext.Request.Headers.Authorization.Parameter)
                return false;

            return true;

        }
    }
}