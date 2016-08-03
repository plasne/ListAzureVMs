using System;
using System.Windows.Forms;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net;
using System.Xml;
using Microsoft.Azure.Management.Compute;
using Microsoft.Azure.Management.Compute.Models;
using Microsoft.Azure.Management.Network;
using Microsoft.Azure.Management.Network.Models;
using Microsoft.Rest.Azure;
using System.Collections.Generic;

namespace ListAzureVMs
{
    public partial class Form1 : Form
    {

        private AuthenticationResult auth;

        public Form1()
        {
            InitializeComponent();
        }

        private async void asmLogin_Click(object sender, EventArgs e)
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

        private void asmQuery_Click(object sender, EventArgs e)
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

        private async void armLogin_Click(object sender, EventArgs e)
        {
            try
            {
                AuthenticationContext context = new AuthenticationContext("https://login.windows.net/peterlasne.onmicrosoft.com");
                ClientCredential credential = new ClientCredential("d6d95a58-78b3-412a-aa6c-3691cf328294", "?????");
                auth = await context.AcquireTokenAsync("https://management.core.windows.net/", credential);
                toolStripStatusLabel1.Text = "Successfully logged in.";
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Failed login: " + ex.Message;
            }
        }

        private class VM
        {
            public string name;
            public string privateIp;
            public string publicIp;
        }

        private VM Rebox(VirtualMachine vm, Dictionary<string, NetworkInterface> nicLookup, Dictionary<string, PublicIPAddress> publicIpLookup)
        {
            VM o = new VM() { name = vm.Name };
            if (vm.NetworkProfile.NetworkInterfaces.Count > 0)
            {
                string nicId = vm.NetworkProfile.NetworkInterfaces[0].Id;
                if (nicLookup.ContainsKey(nicId))
                {
                    NetworkInterface nic = nicLookup[nicId];
                    o.privateIp = nic.IpConfigurations[0].PrivateIPAddress;
                    string pipId = nic.IpConfigurations[0].PublicIPAddress.Id;
                    if (publicIpLookup.ContainsKey(pipId))
                    {
                        PublicIPAddress pip = publicIpLookup[pipId];
                        o.publicIp = pip.IpAddress;
                    }
                }
            }
            return o;
        }

        private void armQuery_Click(object sender, EventArgs e)
        {
            listResults.Items.Clear();

            // convert the access token into a credential
            Microsoft.Rest.TokenCredentials credential = new Microsoft.Rest.TokenCredentials(auth.AccessToken);

            // create a network client
            NetworkManagementClient networkClient = new NetworkManagementClient(credential) { SubscriptionId = "fb0d9e18-712a-426f-ae67-1eac274bf9f1" };

            // read a list of all public IPs
            IPage<PublicIPAddress> publicIps = networkClient.PublicIPAddresses.ListAll();
            Dictionary<string, PublicIPAddress> publicIpLookup = new Dictionary<string, PublicIPAddress>();
            foreach (PublicIPAddress publicIp in publicIps)
            {
                publicIpLookup.Add(publicIp.Id, publicIp);
            }
            while (publicIps.NextPageLink != null)
            {
                publicIps = networkClient.PublicIPAddresses.ListNext(publicIps.NextPageLink);
                foreach (PublicIPAddress publicIp in publicIps)
                {
                    publicIpLookup.Add(publicIp.Id, publicIp);
                }
            }

            // read a list of all NICs
            IPage<NetworkInterface> nics = networkClient.NetworkInterfaces.ListAll();
            Dictionary<string, NetworkInterface> nicLookup = new Dictionary<string, NetworkInterface>();
            foreach (NetworkInterface nic in nics)
            {
                nicLookup.Add(nic.Id, nic);
            }
            while (nics.NextPageLink != null)
            {
                nics = networkClient.NetworkInterfaces.ListNext(nics.NextPageLink);
                foreach (NetworkInterface nic in nics)
                {
                    nicLookup.Add(nic.Id, nic);
                }
            }

            // read a list of all VMs
            ComputeManagementClient computeClient = new ComputeManagementClient(credential) { SubscriptionId = "fb0d9e18-712a-426f-ae67-1eac274bf9f1" };
            IPage<VirtualMachine> vms = computeClient.VirtualMachines.ListAll();
            foreach (VirtualMachine vm in vms)
            {
                VM o = Rebox(vm, nicLookup, publicIpLookup);
                if (o != null) listResults.Items.Add(o.name + ", " + o.privateIp + ", " + o.publicIp);
            }
            while (vms.NextPageLink != null)
            {
                vms = computeClient.VirtualMachines.ListNext(vms.NextPageLink);
                foreach (VirtualMachine vm in vms)
                {
                    VM o = Rebox(vm, nicLookup, publicIpLookup);
                    if (o != null) listResults.Items.Add(o.name + ", " + o.privateIp + ", " + o.publicIp);
                }
            }

        }

    }
}
