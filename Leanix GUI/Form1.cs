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
        private string ID;

        public Form1()
        {
            InitializeComponent();
            try
            {
                ApiClient client = new ApiClientBuilder()
                    .WithBasePath("https://app.leanix.net/UniversityofNottinghamDEMO/api/v1")
                    .WithTokenProviderHost("app.leanix.net")
                    .WithApiToken("YzGaOVCSRavq3bCAKarHNzWnJWs8WUVZtXUa2AVB")
                    .Build();

                ServicesApi api = new ServicesApi();
                List<Service> services = api.getServices(false, "");
                foreach (Service s in services)
                {

                    listBox1.Items.Add(new ListBoxItem {
                        DisplayName = s.displayName,
                        Identifier = s.ID
                    });
                    Console.WriteLine(s.ID);
                }
                listBox1.DisplayMember = "DisplayName";
                listBox1.ValueMember = "Identifier";
                listBox1.Update();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
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
            ServiceInfo ServiceInformation = new ServiceInfo();
            ServiceInformation.Show();
            ServiceInformation.label5.Text = ID;
            ServiceInformation.label6.Text = api
        }
    }
    class ListBoxItem
    {
        public string DisplayName { get; set; }
        public string Identifier { get; set; }
    }
}
