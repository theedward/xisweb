namespace XisWebEAPlugin
{
    partial class ModelGenerationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labelPatterns = new System.Windows.Forms.Label();
            this.comboBoxPatterns = new System.Windows.Forms.ComboBox();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // labelPatterns
            // 
            this.labelPatterns.AutoSize = true;
            this.labelPatterns.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelPatterns.Location = new System.Drawing.Point(13, 13);
            this.labelPatterns.Name = "labelPatterns";
            this.labelPatterns.Size = new System.Drawing.Size(152, 13);
            this.labelPatterns.TabIndex = 0;
            this.labelPatterns.Text = "Home generation Pattern:";
            // 
            // comboBoxPatterns
            // 
            this.comboBoxPatterns.FormattingEnabled = true;
            this.comboBoxPatterns.Items.AddRange(new object[] {
            "List Menu"});
            //"Tab Menu"});
            this.comboBoxPatterns.SelectedItem = this.comboBoxPatterns.Items[0];
            this.comboBoxPatterns.Location = new System.Drawing.Point(167, 10);
            this.comboBoxPatterns.Name = "comboBoxPatterns";
            this.comboBoxPatterns.Size = new System.Drawing.Size(121, 21);
            this.comboBoxPatterns.TabIndex = 1;
            this.comboBoxPatterns.SelectedIndexChanged += new System.EventHandler(this.comboBoxPatterns_SelectedIndexChanged);
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(119, 56);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(75, 23);
            this.buttonGenerate.TabIndex = 0;
            this.buttonGenerate.Text = "Generate!";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // ModelGenerationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 90);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.comboBoxPatterns);
            this.Controls.Add(this.labelPatterns);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModelGenerationForm";
            this.Text = "Generate Models...";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPatterns;
        private System.Windows.Forms.ComboBox comboBoxPatterns;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}