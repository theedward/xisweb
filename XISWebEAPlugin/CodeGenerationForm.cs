using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Reflection;
using System.Threading;

namespace XisWebEAPlugin
{
    public partial class CodeGenerationForm : Form
    {
        string platformType = null;
        const string noPath = "Select a folder...";
        private EA.Repository repository;
        private bool GENERATE = true;

        public CodeGenerationForm(EA.Repository repository)
        {
            InitializeComponent();
            this.repository = repository;
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();

            if (!string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
            {
                textBoxPath.Text = folderBrowserDialog1.SelectedPath;
                errorProvider.SetError(textBoxPath, string.Empty);
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            bool valid = true;

            if (textBoxPath.Text != noPath)
            {
                errorProvider.SetError(textBoxPath, string.Empty);
            }
            else
            {
                errorProvider.SetError(textBoxPath, "A Destination folder must be specified!");
                valid = false;
            }

            if (valid)
            {
                if (GENERATE)
                {
                    ChangeControlsUponGeneration();
                    backgroundWorker.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Code Generation disabled for now. It is being updated for the next version!",
                        "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ChangeControlsUponGeneration()
        {
            this.ClientSize = new System.Drawing.Size(399, 155);
            progressBar.Visible = true;
            buttonBrowse.Enabled = false;
            buttonGenerate.Enabled = false;
            buttonGenerate.Text = "Generating...";
        }

        private void RestoreControlsAfterGeneration()
        {
            this.ClientSize = new System.Drawing.Size(399, 135);
            progressBar.Visible = false;
            buttonBrowse.Enabled = true;
            buttonGenerate.Enabled = true;
            buttonGenerate.Text = "Generate!";
        }

        private void ExecuteCommand(string command)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo("java.exe", "-jar " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            // *** Redirect the output ***
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            Process process = Process.Start(processInfo);
            process.WaitForExit();

            // *** Read the streams ***
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            if (!string.IsNullOrEmpty(output))
            {
                MessageBox.Show("output>>" + output,
                    "",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
            }

            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show("error>>" + error,
                    "",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop,
                    MessageBoxDefaultButton.Button1);
            }
            process.Close();
        }

        #region backgroundWorker Methods

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            EA.Project project = repository.GetProjectInterface();
            EA.Package package = (EA.Package)repository.Models.GetAt(0);
            string[] res = repository.ConnectionString.Split('\\');
            string projectName = res[res.Length - 1].Split('.')[0];
            string xmiPath = textBoxPath.Text + "\\" + projectName + ".xmi";
            string umlPath = "\"" + textBoxPath.Text + "\\" + projectName + ".uml\"";
            project.ExportPackageXMI(package.PackageGUID, EA.EnumXMIType.xmiEA21, 1, -1, 1, 0, xmiPath);
            string exePath = "\"" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            xmiPath = "\"" + xmiPath + "\"";

            ExecuteCommand(exePath + "\\XMLParser.jar\" " + exePath + "\" " + xmiPath + " \"" + projectName + "\"");
            backgroundWorker.ReportProgress(50, new string[] { "Model parsing done!" });
            ExecuteCommand(exePath + "\\Html5Generator.jar\" " + exePath + "\" " + umlPath + " \"" + textBoxPath.Text + "\\src-gen\"");
            backgroundWorker.ReportProgress(100, new string[] { "Site generation done!" });

                //case "Windows Phone":
                //    ExecuteCommand(exePath + "\\XMLParser.jar\" " + exePath + "\" " + xmiPath + " \"" + projectName + "\"");
                //    backgroundWorker.ReportProgress(50, new string[] { "Model parsing done!" });
                //    ExecuteCommand(exePath + "\\WindowsPhoneGenerator.jar\" " + exePath + "\" " + umlPath + " \"" + textBoxPath.Text + "\\src-gen");
                //    backgroundWorker.ReportProgress(100, new string[] { "Windows Phone generation done!" });
                //    break;
                //case "iOS":
                //    ExecuteCommand(exePath + "\\XMLParser.jar\" " + exePath + "\" " + xmiPath + " \"" + projectName + "\"");
                //    backgroundWorker.ReportProgress(50, new string[] { "Model parsing done!" });
                //    ExecuteCommand(exePath + "\\iOSGenerator.jar\" " + exePath + "\" " + umlPath + " \"" + textBoxPath.Text + "\\src-gen\"");
                //    backgroundWorker.ReportProgress(100, new string[] { "iOS generation done!" });
                //    break;
                //case "All":
                //    ExecuteCommand(exePath + "\\XMLParser.jar\" " + exePath + "\" " + xmiPath + " \"" + projectName + "\"");
                //    backgroundWorker.ReportProgress(25, new string[] { "Model parsing done!" });
                //    ExecuteCommand(exePath + "\\AndroidGenerator.jar\" " + exePath + "\" " + umlPath + " \"" + textBoxPath.Text + "\\android\\src-gen\"");
                //    backgroundWorker.ReportProgress(50, new string[] { "Android generation done!" });
                //    ExecuteCommand(exePath + "\\WindowsPhoneGenerator.jar\" " + exePath + "\" " + umlPath + " \"" + textBoxPath.Text + "\\windowsphone\\src-gen");
                //    backgroundWorker.ReportProgress(75, new string[] { "Windows Phone generation done!" });
                //    ExecuteCommand(exePath + "\\iOSGenerator.jar\" " + exePath + "\" " + umlPath + " \"" + textBoxPath.Text + "\\ios\\src-gen\"");
                //    backgroundWorker.ReportProgress(100, new string[] { "iOS generation done!" });
                //    break;
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            this.Text = e.ProgressPercentage.ToString() + "%";

            if (e.UserState is string[] && ((string[])e.UserState).Count() > 0)
            {
                this.Text += " - " + ((string[])e.UserState)[0];
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RestoreControlsAfterGeneration();
        }

        #endregion
    }
}
