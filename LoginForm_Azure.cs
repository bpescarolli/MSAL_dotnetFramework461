
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;

namespace MSAL_dotnetFramework461
{
    public partial class LoginForm_Azure : Form
    {
        private static bool loggeIn = false;
        private static readonly string[] scopes = { "user.read" };
        private string AccessToken = null;
        private string UserName = null;

        public LoginForm_Azure()
        {
            InitializeComponent();
        }

        private async void logginBtn_Click(object sender, EventArgs e)
        {
            if (!loggeIn)
            {
                //Windows Auth
                WindowsIdentity current = WindowsIdentity.GetCurrent();
                WindowsPrincipal windowsPrincipal = new WindowsPrincipal(current);

                var authResult = await Login();
                if (authResult != null)
                {
                    UserName = authResult.Account.Username;
                    AccessToken = authResult.AccessToken;
                    loggeIn = true;
                    if (verifyAccessPermission())
                    {
                        MessageBox.Show("Sucess!");
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Usuário sem permissão para utilização do software.", "Recusado!");
                    }
                }
            }
            else
            {
                await Logout();
                loggeIn = false;
            }
        }

        private async Task<AuthenticationResult> Login()
        {
            AuthenticationResult authResult = null;
            var accounts = await Program.PublicClientApp.GetAccountsAsync();
            var firstAccount = accounts.FirstOrDefault();

            try
            {
                authResult = await Program.PublicClientApp.AcquireTokenSilent(scopes, firstAccount)
                    .ExecuteAsync();
            }
            catch (MsalUiRequiredException ex)
            {
                // A MsalUiRequiredException happened on AcquireTokenSilent.
                // This indicates you need to call AcquireTokenInteractive to acquire a token
                System.Diagnostics.Debug.WriteLine($"MsalUiRequiredException: {ex.Message}");

                try
                {
                    authResult = await Program.PublicClientApp.AcquireTokenInteractive(scopes)
                        .WithAccount(accounts.FirstOrDefault())
                        .WithPrompt(Prompt.SelectAccount)
                        .ExecuteAsync();
                }
                catch (MsalException msalex)
                {
                    //label1.Text = $"Error Acquiring Token:{System.Environment.NewLine}{msalex}";
                    MessageBox.Show(msalex.Message);
                }
            }
            catch (Exception ex)
            {
                //label1.Text = $"Error Acquiring Token Silently:{System.Environment.NewLine}{ex}";
                MessageBox.Show(ex.Message);
            }
            return authResult;
        }

        private async Task Logout()
        {

            var accounts = await Program.PublicClientApp.GetAccountsAsync();
            if (accounts.Any())
            {
                try
                {
                    await Program.PublicClientApp.RemoveAsync(accounts.FirstOrDefault());

                }
                catch (MsalException ex)
                {
                    throw new Exception($"Error signing-out user: {ex.Message}");
                }
            }
        }
        private bool verifyAccessPermission()
        {
            bool access = false;
            string result = HTTPRequest("https://graph.microsoft.com/v1.0/me/memberOf", AccessToken);
            Production appInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<Production>(Encoding.UTF8.GetString(MSAL_dotnetFramework461.Resources.appinfo));
            foreach (string groupID in appInfo.AllowedGroupsID)
            {
                if (result.Contains("\"id\":\"" + groupID + "\"")) //ID DO GRUPO EDITÁVEL EM RESOURCES/APPINFO.JSON
                {
                    access = true;
                }
            }

            return access;
        }

        private string getUserName()
        {
            string result = HTTPRequest("https://graph.microsoft.com/v1.0/me/", AccessToken);
            UserData userData = Newtonsoft.Json.JsonConvert.DeserializeObject<UserData>(result);
            return userData.displayName;
        }

        private string HTTPRequest(string address, string token)
        {
            try
            {
                Uri url = new Uri(address);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.PreAuthenticate = true;
                req.Headers.Add("Authorization", "Bearer " + token);
                req.Accept = "application/json";
                req.Method = "GET";

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                Stream dataStream = res.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadLine();

                return responseFromServer;
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro", e.Message);
                return null;
            }

        }

        private void microsoftIconPictureBox_Click(object sender, EventArgs e)
        {
            logginBtn_Click(null, null);
        }

        private string HostName()
        {
            string HostName = null;

            HostName = Dns.GetHostName().ToString();

            if(string.IsNullOrEmpty(HostName))
            {
                HostName = "Dispositivo Desconhecido";
            } else if (HostName.Contains("::"))
            {
                HostName = HostName.Replace("::", " ");

            } 
            else if (HostName.Contains("/"))
            {
                HostName = HostName.Replace("/", " ");

            }
            else if (HostName.Contains("\\"))
            {
                HostName = HostName.Replace("\\", " ");
            }

            return HostName;
        }

    }

    public class UserData
    {
        public string displayName { get; set; }
        public string givenName { get; set; }
        public string surname { get; set; }
        public string userPrincipalName { get; set; }
        public string id { get; set; }
    }

}
