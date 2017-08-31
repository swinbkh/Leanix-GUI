using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LeanIX.Api;
using LeanIX.Api.Common;
using LeanIX.Api.Models;

namespace Leanix_GUI
{
    
    public partial class Form1 : Form
    {
        private string ID;      //used for global service reference
        string Token = "YzGaOVCSRavq3bCAKarHNzWnJWs8WUVZtXUa2AVB";
        string Domain = "https://app.leanix.net/UniversityofNottinghamDEMO/api/v1";


        ServicesApi api = new ServicesApi();        //New instance of the leaIX Api

        public Form1()
        {
            InitializeComponent();
            try
            {
                ApiClient client = new ApiClientBuilder()
                    .WithBasePath(Domain)       //Domain Name
                    .WithTokenProviderHost("app.leanix.net")        //Base URL
                    .WithApiToken(Token)       //Auth Token
                    .Build();

                ServicesApi api = new ServicesApi();
                List<Service> services = api.getServices(false, "");        //List all services
                foreach (Service s in services)
                {

                    listBox1.Items.Add(new ListBoxItem {        //add to listbox
                        DisplayName = s.displayName,        //keep name and ID seperate
                        Identifier = s.ID
                    });
                    Console.WriteLine(s.ID);
                }
                listBox1.DisplayMember = "DisplayName";     //display Name of service
                listBox1.ValueMember = "Identifier";        //Keep ID of selected service for later reference
                listBox1.Update();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                label1.Text = "Unable to connect.";
            }

            System.Console.ReadLine();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = ((ListBoxItem)listBox1.SelectedItem).DisplayName.ToString();
            label2.Text = ((ListBoxItem)listBox1.SelectedItem).Identifier.ToString();
            ID = ((ListBoxItem)listBox1.SelectedItem).Identifier.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ApiClient client = new ApiClientBuilder()
    .WithBasePath(Domain)
    .WithTokenProviderHost("app.leanix.net")
    .WithApiToken(Token)
    .Build();



            ServiceInfo ServiceInformation = new ServiceInfo();
                ServiceInformation.Show();
                StringBuilder formatted = new StringBuilder();

                Service service = api.getService(ID, false);

                string name = "Name: " + service.displayName + "\n";
                string description = "Description: \n\t" + service.description + "\n";
                string serviceID = "ID: " + ID + "\n";
                string providedInterfaces = "Provided Interfaces: \n";
                string consumedInterfaces = "Consumed Interfaces: \n";

                List<FactSheetHasIfaceProvider> Providers = (api.getFactSheetHasIfaceProviders(ID));


                for (int i = 0; i < Providers.Count; i++)
                {
                    string relation = Providers[i].ID;
                    providedInterfaces = providedInterfaces + ("\t" + api.getService((Providers[i]).ifaceID, false).displayName) + "\n";    //Display Name Of Provided Interface
                }

                List<FactSheetHasIfaceConsumer> Consumers = (api.getFactSheetHasIfaceConsumers(ID));


                for (int i = 0; i < Consumers.Count; i++)
                {
                    string relation = Providers[i].ID;
                    consumedInterfaces = consumedInterfaces + ("\t" + api.getService((Consumers[i]).ifaceID, false).displayName) + "\n";    //Display Name Of Provided Interface
                }
                if (providedInterfaces == "Provided Interfaces: \n")
                { providedInterfaces = providedInterfaces + "\tNone.\n"; }

                if (consumedInterfaces == "Consumed Interfaces: \n")
                { consumedInterfaces = consumedInterfaces + "\tNone."; }


                formatted.Append(description).Append(providedInterfaces).Append(consumedInterfaces);
                ServiceInformation.label5.Text = name.Remove(0, 6);
                ServiceInformation.label6.Text = ID;
                ServiceInformation.richTextBox1.Text = formatted.ToString();
                ServiceInformation.richTextBox1.ReadOnly = true;









            


        }
    }
    class ListBoxItem
    {
        public string DisplayName { get; set; }
        public string Identifier { get; set; }
    }
}
