using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XisWebEAPlugin
{
    public partial class ModelGenerationForm : Form
    {
        private string patternType = "List Menu";
        private EA.Repository repository;
        private EA.Package navigationPackage;
        private EA.Package interactionPackage;
        private List<EA.Element> useCases;

        public ModelGenerationForm(EA.Repository repository, EA.Package navigationPackage, EA.Package interactionPackage,
            List<EA.Element> useCases)
        {
            InitializeComponent();
            this.repository = repository;
            this.navigationPackage = navigationPackage;
            this.interactionPackage = interactionPackage;
            this.useCases = useCases;
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            bool valid = true;

            if (!string.IsNullOrEmpty(patternType))
            {
                errorProvider.SetError(comboBoxPatterns, string.Empty);
            }
            else
            {
                errorProvider.SetError(comboBoxPatterns, "A Pattern must be specified!");
                valid = false;
            }

            if (valid)
            {
                M2MTransformer.ProcessUseCase(repository, navigationPackage, interactionPackage, useCases, patternType);
                Close();
                MessageBox.Show("Models successfully generated!\r\nCheck the contents of «InteractionSpace View» and «NavigationSpace View».",
                    "XIS-Web Plugin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void comboBoxPatterns_SelectedIndexChanged(object sender, EventArgs e)
        {
            patternType = comboBoxPatterns.SelectedItem as String;
            errorProvider.SetError(comboBoxPatterns, string.Empty);
        }
    }
}
