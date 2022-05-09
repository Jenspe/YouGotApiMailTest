using System;
using RestSharp;
using RestSharp.Authenticators;

namespace ApiEmailTest
{
    class EmailService
    {
        private const string APIKey = "3d6982c50a31df508d4c038fc9d92d56-fe066263-fb31d0bd";
        private const string BaseUri = "https://api.mailgun.net/v3";
        private const string Domain = "sandbox6c7ad4ea76d24d959b5d03d6866a6c75.mailgun.org";
        private const string SenderAddress = "YouGot@Mail.com";
        private const string SenderDisplayName = "YouGotMail";
        private const string Tag = "sampleTag";

        public static IRestResponse SendEmail(UserEmailOptions userEmailOptions)
        {

            RestClient client = new RestClient
            {
                BaseUrl = new Uri(BaseUri),
                Authenticator = new HttpBasicAuthenticator("api", APIKey)
            };

            RestRequest request = new RestRequest();
            request.AddParameter("domain", Domain, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", $"{SenderDisplayName} <{SenderAddress}>");

            foreach (var toEmail in userEmailOptions.ToEmails)
            {
                request.AddParameter("to", toEmail);
            }

            request.AddParameter("subject", userEmailOptions.Subject);
            request.AddParameter("html", userEmailOptions.Body);
            request.AddParameter("o:tag", Tag);
            request.Method = Method.POST;
            return client.Execute(request);
        }

    }
}

