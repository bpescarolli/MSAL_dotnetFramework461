using Microsoft.Identity.Client;
using System.Text;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace MSAL_dotnetFramework461
{ 
    internal static class Program
    {
        public static Production appInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<Production>(Encoding.UTF8.GetString(MSAL_dotnetFramework461.Resources.appinfo));
        public static string ClientId = appInfo.ClientID;
        public static string Tenant = appInfo.TenantID;
        private static IPublicClientApplication clientApp;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [System.STAThread]
        static void Main()
        {
            InitializeAuth();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm_Azure());
        }

        public static IPublicClientApplication PublicClientApp { get { return clientApp; } }

        private static void InitializeAuth()
        {

            clientApp = PublicClientApplicationBuilder.Create(ClientId)
                   .WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient")
                   //.WithDefaultRedirectUri()
                   .WithAuthority(AzureCloudInstance.AzurePublic, Tenant)
                   .Build();
            //TokenCacheHelper.EnableSerialization(clientApp.UserTokenCache);
        }

    }
}
