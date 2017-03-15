namespace SmartThesaurus
{
    partial class EducanetLogin
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
            this.txbEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txbPassword = new System.Windows.Forms.TextBox();
            this.lblConnexion = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txbEmail
            // 
            this.txbEmail.Location = new System.Drawing.Point(105, 72);
            this.txbEmail.Name = "txbEmail";
            this.txbEmail.Size = new System.Drawing.Size(183, 20);
            this.txbEmail.TabIndex = 0;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(63, 75);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(32, 13);
            this.lblEmail.TabIndex = 1;
            this.lblEmail.Text = "Email";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(22, 113);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(71, 13);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "Mot de passe";
            // 
            // txbPassword
            // 
            this.txbPassword.Location = new System.Drawing.Point(104, 111);
            this.txbPassword.Name = "txbPassword";
            this.txbPassword.PasswordChar = '*';
            this.txbPassword.Size = new System.Drawing.Size(183, 20);
            this.txbPassword.TabIndex = 2;
            // 
            // lblConnexion
            // 
            this.lblConnexion.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnexion.Location = new System.Drawing.Point(129, 23);
            this.lblConnexion.Name = "lblConnexion";
            this.lblConnexion.Size = new System.Drawing.Size(116, 23);
            this.lblConnexion.TabIndex = 4;
            this.lblConnexion.Text = "Connexion";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(262, 149);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(81, 29);
            this.btnConnect.TabIndex = 5;
            this.btnConnect.Text = "Se connecter";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // EducanetLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 190);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lblConnexion);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txbPassword);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txbEmail);
            this.Name = "EducanetLogin";
            this.Text = "EducanetLogin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txbPassword;
        private System.Windows.Forms.Label lblConnexion;
        private System.Windows.Forms.Button btnConnect;
    }
}