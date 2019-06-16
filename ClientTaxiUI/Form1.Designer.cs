namespace ClientTaxiUI {
    partial class Form1 {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent() {
            this.panel1 = new System.Windows.Forms.Panel();
            this.DestinationAddressBox = new System.Windows.Forms.TextBox();
            this.StartAddressBox = new System.Windows.Forms.TextBox();
            this.PhoneBox = new System.Windows.Forms.TextBox();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.DestinationAddressLabel = new System.Windows.Forms.Label();
            this.StartAddresslabel = new System.Windows.Forms.Label();
            this.Phonelabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.DestinationAddressBox);
            this.panel1.Controls.Add(this.StartAddressBox);
            this.panel1.Controls.Add(this.PhoneBox);
            this.panel1.Controls.Add(this.NameBox);
            this.panel1.Controls.Add(this.TimeLabel);
            this.panel1.Controls.Add(this.DestinationAddressLabel);
            this.panel1.Controls.Add(this.StartAddresslabel);
            this.panel1.Controls.Add(this.Phonelabel);
            this.panel1.Controls.Add(this.NameLabel);
            this.panel1.Location = new System.Drawing.Point(4, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1215, 658);
            this.panel1.TabIndex = 0;
            // 
            // DestinationAddressBox
            // 
            this.DestinationAddressBox.Location = new System.Drawing.Point(215, 346);
            this.DestinationAddressBox.Name = "DestinationAddressBox";
            this.DestinationAddressBox.Size = new System.Drawing.Size(424, 20);
            this.DestinationAddressBox.TabIndex = 8;
            // 
            // StartAddressBox
            // 
            this.StartAddressBox.Location = new System.Drawing.Point(215, 270);
            this.StartAddressBox.Name = "StartAddressBox";
            this.StartAddressBox.Size = new System.Drawing.Size(424, 20);
            this.StartAddressBox.TabIndex = 7;
            // 
            // PhoneBox
            // 
            this.PhoneBox.Location = new System.Drawing.Point(215, 200);
            this.PhoneBox.Name = "PhoneBox";
            this.PhoneBox.Size = new System.Drawing.Size(169, 20);
            this.PhoneBox.TabIndex = 6;
            // 
            // NameBox
            // 
            this.NameBox.Location = new System.Drawing.Point(215, 108);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(185, 20);
            this.NameBox.TabIndex = 5;
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Location = new System.Drawing.Point(212, 391);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(30, 13);
            this.TimeLabel.TabIndex = 4;
            this.TimeLabel.Text = "Time";
            // 
            // DestinationAddressLabel
            // 
            this.DestinationAddressLabel.AutoSize = true;
            this.DestinationAddressLabel.Location = new System.Drawing.Point(212, 310);
            this.DestinationAddressLabel.Name = "DestinationAddressLabel";
            this.DestinationAddressLabel.Size = new System.Drawing.Size(98, 13);
            this.DestinationAddressLabel.TabIndex = 3;
            this.DestinationAddressLabel.Text = "DestinationAddress";
            // 
            // StartAddresslabel
            // 
            this.StartAddresslabel.AutoSize = true;
            this.StartAddresslabel.Location = new System.Drawing.Point(212, 235);
            this.StartAddresslabel.Name = "StartAddresslabel";
            this.StartAddresslabel.Size = new System.Drawing.Size(67, 13);
            this.StartAddresslabel.TabIndex = 2;
            this.StartAddresslabel.Text = "StartAddress";
            // 
            // Phonelabel
            // 
            this.Phonelabel.AutoSize = true;
            this.Phonelabel.Location = new System.Drawing.Point(212, 157);
            this.Phonelabel.Name = "Phonelabel";
            this.Phonelabel.Size = new System.Drawing.Size(38, 13);
            this.Phonelabel.TabIndex = 1;
            this.Phonelabel.Text = "Phone";
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(212, 79);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(35, 13);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "Name";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1222, 657);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label Phonelabel;
        private System.Windows.Forms.Label StartAddresslabel;
        private System.Windows.Forms.Label DestinationAddressLabel;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.TextBox StartAddressBox;
        private System.Windows.Forms.TextBox PhoneBox;
        private System.Windows.Forms.TextBox DestinationAddressBox;
    }
}

