namespace SmartThesaurus
{
    partial class ManualDateDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnSendManualDate = new System.Windows.Forms.Button();
            this.dtp = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date : ";
            // 
            // btnSendManualDate
            // 
            this.btnSendManualDate.Location = new System.Drawing.Point(244, 21);
            this.btnSendManualDate.Name = "btnSendManualDate";
            this.btnSendManualDate.Size = new System.Drawing.Size(48, 23);
            this.btnSendManualDate.TabIndex = 4;
            this.btnSendManualDate.Text = "Valider";
            this.btnSendManualDate.UseVisualStyleBackColor = true;
            this.btnSendManualDate.Click += new System.EventHandler(this.btnSendManualDate_Click);
            // 
            // dtp
            // 
            this.dtp.Location = new System.Drawing.Point(58, 24);
            this.dtp.Name = "dtp";
            this.dtp.Size = new System.Drawing.Size(180, 20);
            this.dtp.TabIndex = 5;
            this.dtp.Value = new System.DateTime(2017, 3, 1, 9, 28, 26, 0);
            // 
            // ManualDateDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 65);
            this.Controls.Add(this.dtp);
            this.Controls.Add(this.btnSendManualDate);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManualDateDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Actualisation manuelle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSendManualDate;
        private System.Windows.Forms.DateTimePicker dtp;
    }
}