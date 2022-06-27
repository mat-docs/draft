using System;
using System.Windows.Forms;

using MAT.Atlas.Automation.Client.Services;

namespace LoadSSN
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var application = new ApplicationServiceClient();
                    var sets = WorkbookServiceClient.Call(client => client.GetSets());
                    if (sets.Length == 0)
                    {
                        throw new Exception("Ensure ATLAS is running...");
                    }

                    application.LoadFileSessions(sets[0].Id, openFileDialog.FileNames);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }
    }
}