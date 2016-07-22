using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net;
using System.Xml;

namespace ListAzureVMs
{
    public partial class Form1 : Form
    {

        private AuthenticationResult auth;

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                AuthenticationContext context = new AuthenticationContext("https://login.windows.net/peterlasne.onmicrosoft.com");
                UserPasswordCredential credential = new UserPasswordCredential("service@peterlasne.onmicrosoft.com", "???????");
                auth = await context.AcquireTokenAsync("https://management.core.windows.net/", "1beaa8ef-a7aa-40b9-958f-5b61e6feef36", credential);
                toolStripStatusLabel1.Text = "Successfully logged in.";
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Failed login: " + ex.Message;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            listResults.Items.Clear();

            WebClient client = new WebClient();
            client.Headers.Add("Authorization", "bearer " + auth.AccessToken);
            client.Headers.Add("x-ms-version", "2015-04-01");
            string services = client.DownloadString("https://management.core.windows.net/fb0d9e18-712a-426f-ae67-1eac274bf9f1/services/hostedservices");

            XmlDocument servicesXml = new XmlDocument();
            XmlNamespaceManager servicesNsm = new XmlNamespaceManager(servicesXml.NameTable);
            servicesNsm.AddNamespace("a", "http://schemas.microsoft.com/windowsazure");
            servicesXml.LoadXml(services);

            foreach (XmlNode service in servicesXml.DocumentElement.SelectNodes("a:HostedService", servicesNsm))
            {

                // query to get the cloud service properties
                string serviceName = service.SelectSingleNode("a:ServiceName", servicesNsm).InnerText;
                string properties = client.DownloadString("https://management.core.windows.net/fb0d9e18-712a-426f-ae67-1eac274bf9f1/services/hostedservices/" + serviceName + "?embed-detail=true");
                XmlDocument propertiesXml = new XmlDocument();
                XmlNamespaceManager propertiesNsm = new XmlNamespaceManager(propertiesXml.NameTable);
                propertiesNsm.AddNamespace("a", "http://schemas.microsoft.com/windowsazure");
                propertiesXml.LoadXml(properties);

                // get the instances
                foreach (XmlNode deployment in propertiesXml.DocumentElement.SelectNodes("a:Deployments/a:Deployment/a:RoleInstanceList/a:RoleInstance", propertiesNsm))
                {
                    listResults.Items.Add(deployment.SelectSingleNode("a:InstanceName", propertiesNsm).InnerText);
                }

            }

            toolStripStatusLabel1.Text = listResults.Items.Count + " classic VMs found.";

        }
    }
}
