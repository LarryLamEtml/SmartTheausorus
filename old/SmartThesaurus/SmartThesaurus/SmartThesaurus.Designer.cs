namespace SmartThesaurus
{
    partial class formSearch
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formSearch));
            this.txbInput = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnQuit = new System.Windows.Forms.Button();
            this.cbIndex = new System.Windows.Forms.ComboBox();
            this.pbTitle = new System.Windows.Forms.PictureBox();
            this.lblSearch = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).BeginInit();
            this.pbTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // txbInput
            // 
            this.txbInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbInput.Location = new System.Drawing.Point(350, 366);
            this.txbInput.Name = "txbInput";
            this.txbInput.Size = new System.Drawing.Size(274, 31);
            this.txbInput.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(583, 366);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(41, 30);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DimGray;
            this.lblTitle.Location = new System.Drawing.Point(12, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(393, 55);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Smart Thésaurus";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(700, 527);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(97, 42);
            this.btnQuit.TabIndex = 4;
            this.btnQuit.Text = "Quitter";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // cbIndex
            // 
            this.cbIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIndex.FormattingEnabled = true;
            this.cbIndex.Location = new System.Drawing.Point(234, 365);
            this.cbIndex.Name = "cbIndex";
            this.cbIndex.Size = new System.Drawing.Size(110, 32);
            this.cbIndex.TabIndex = 5;
            this.cbIndex.SelectedIndexChanged += new System.EventHandler(this.cbIndex_SelectedIndexChanged);
            // 
            // pbTitle
            // 
            this.pbTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(250)))), ((int)(((byte)(65)))));
            this.pbTitle.Controls.Add(this.lblTitle);
            this.pbTitle.Location = new System.Drawing.Point(-6, 0);
            this.pbTitle.Name = "pbTitle";
            this.pbTitle.Size = new System.Drawing.Size(827, 95);
            this.pbTitle.TabIndex = 6;
            this.pbTitle.TabStop = false;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(230)))), ((int)(((byte)(60)))));
            this.lblSearch.Location = new System.Drawing.Point(235, 211);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(324, 46);
            this.lblSearch.TabIndex = 7;
            this.lblSearch.Text = "Smart Thésaurus";
            // 
            // formSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 600);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.pbTitle);
            this.Controls.Add(this.cbIndex);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txbInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Smart Thésaurus";
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).EndInit();
            this.pbTitle.ResumeLayout(false);
            this.pbTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();


        }

        #endregion
        private System.Windows.Forms.TextBox txbInput;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.ComboBox cbIndex;
        private System.Windows.Forms.PictureBox pbTitle;
        private System.Windows.Forms.Label lblSearch;
    }
}

