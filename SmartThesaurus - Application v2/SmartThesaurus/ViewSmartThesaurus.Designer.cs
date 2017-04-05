﻿using System.Drawing;
using System.Windows.Forms;

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
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.TbCMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkbTemp = new System.Windows.Forms.CheckBox();
            this.checkbEtml = new System.Windows.Forms.CheckBox();
            this.listViewResultEtml = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDirectory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnlSearchBar = new System.Windows.Forms.Panel();
            this.pblSearchBtn = new System.Windows.Forms.Panel();
            this.btnSearchEtml = new System.Windows.Forms.Button();
            this.txbInputEtml = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnLogin = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.listViewResultEducanet = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSearchEducanet = new System.Windows.Forms.Button();
            this.txbInputEducanet = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listViewResultTemp = new System.Windows.Forms.ListView();
            this.colHeadName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHeadSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHeadModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHeadDirectory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnSearchTemp = new System.Windows.Forms.Button();
            this.txbInputTemp = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblBackColor = new System.Windows.Forms.Label();
            this.btnHide = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cmbActualisation = new System.Windows.Forms.ComboBox();
            this.pnlMain.SuspendLayout();
            this.TbCMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.pnlSearchBar.SuspendLayout();
            this.pblSearchBtn.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F);
            this.lblTitle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblTitle.Location = new System.Drawing.Point(11, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(284, 39);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Smart Thésaurus";
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.formSearch_MouseDown);
            this.lblTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.formSearch_MouseMove);
            this.lblTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.formSearch_MouseUp);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.TbCMain);
            this.pnlMain.Controls.Add(this.lblSearch);
            this.pnlMain.Location = new System.Drawing.Point(11, 54);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(778, 535);
            this.pnlMain.TabIndex = 4;
            // 
            // TbCMain
            // 
            this.TbCMain.Controls.Add(this.tabPage1);
            this.TbCMain.Controls.Add(this.tabPage2);
            this.TbCMain.Controls.Add(this.tabPage3);
            this.TbCMain.Location = new System.Drawing.Point(18, 100);
            this.TbCMain.Name = "TbCMain";
            this.TbCMain.SelectedIndex = 0;
            this.TbCMain.Size = new System.Drawing.Size(741, 420);
            this.TbCMain.TabIndex = 18;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.checkbTemp);
            this.tabPage1.Controls.Add(this.checkbEtml);
            this.tabPage1.Controls.Add(this.listViewResultEtml);
            this.tabPage1.Controls.Add(this.pnlSearchBar);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(733, 394);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ETML";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(35, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 42);
            this.label2.TabIndex = 26;
            this.label2.Text = " ";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(250)))), ((int)(((byte)(65)))));
            this.label1.Location = new System.Drawing.Point(32, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 48);
            this.label1.TabIndex = 25;
            this.label1.Text = " ";
            // 
            // checkbTemp
            // 
            this.checkbTemp.AutoSize = true;
            this.checkbTemp.BackColor = System.Drawing.Color.Transparent;
            this.checkbTemp.Location = new System.Drawing.Point(42, 36);
            this.checkbTemp.Name = "checkbTemp";
            this.checkbTemp.Size = new System.Drawing.Size(53, 17);
            this.checkbTemp.TabIndex = 25;
            this.checkbTemp.Text = "Temp";
            this.checkbTemp.UseVisualStyleBackColor = false;
            // 
            // checkbEtml
            // 
            this.checkbEtml.AutoSize = true;
            this.checkbEtml.BackColor = System.Drawing.Color.Transparent;
            this.checkbEtml.Location = new System.Drawing.Point(42, 14);
            this.checkbEtml.Name = "checkbEtml";
            this.checkbEtml.Size = new System.Drawing.Size(55, 17);
            this.checkbEtml.TabIndex = 24;
            this.checkbEtml.Text = "ETML";
            this.checkbEtml.UseVisualStyleBackColor = false;
            // 
            // listViewResultEtml
            // 
            this.listViewResultEtml.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colDirectory,
            this.colSize});
            this.listViewResultEtml.GridLines = true;
            this.listViewResultEtml.Location = new System.Drawing.Point(9, 67);
            this.listViewResultEtml.Name = "listViewResultEtml";
            this.listViewResultEtml.Size = new System.Drawing.Size(715, 318);
            this.listViewResultEtml.TabIndex = 23;
            this.listViewResultEtml.UseCompatibleStateImageBehavior = false;
            this.listViewResultEtml.View = System.Windows.Forms.View.Details;
            this.listViewResultEtml.DoubleClick += new System.EventHandler(this.listViewResultEtml_DoubleClick);
            // 
            // colName
            // 
            this.colName.Text = "Nom";
            this.colName.Width = 153;
            // 
            // colDirectory
            // 
            this.colDirectory.Text = "Chemin";
            // 
            // colSize
            // 
            this.colSize.Text = "Taille";
            // 
            // pnlSearchBar
            // 
            this.pnlSearchBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(250)))), ((int)(((byte)(65)))));
            this.pnlSearchBar.Controls.Add(this.pblSearchBtn);
            this.pnlSearchBar.Controls.Add(this.txbInputEtml);
            this.pnlSearchBar.Location = new System.Drawing.Point(125, 13);
            this.pnlSearchBar.Name = "pnlSearchBar";
            this.pnlSearchBar.Size = new System.Drawing.Size(406, 40);
            this.pnlSearchBar.TabIndex = 18;
            // 
            // pblSearchBtn
            // 
            this.pblSearchBtn.BackColor = System.Drawing.SystemColors.Window;
            this.pblSearchBtn.Controls.Add(this.btnSearchEtml);
            this.pblSearchBtn.Location = new System.Drawing.Point(367, 4);
            this.pblSearchBtn.Name = "pblSearchBtn";
            this.pblSearchBtn.Size = new System.Drawing.Size(36, 32);
            this.pblSearchBtn.TabIndex = 10;
            // 
            // btnSearchEtml
            // 
            this.btnSearchEtml.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(250)))), ((int)(((byte)(65)))));
            this.btnSearchEtml.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchEtml.BackgroundImage")));
            this.btnSearchEtml.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(250)))), ((int)(((byte)(65)))));
            this.btnSearchEtml.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchEtml.Location = new System.Drawing.Point(5, 3);
            this.btnSearchEtml.Name = "btnSearchEtml";
            this.btnSearchEtml.Size = new System.Drawing.Size(27, 26);
            this.btnSearchEtml.TabIndex = 1;
            this.btnSearchEtml.UseVisualStyleBackColor = false;
            this.btnSearchEtml.Click += new System.EventHandler(this.btnSearchEtml_Click);
            // 
            // txbInputEtml
            // 
            this.txbInputEtml.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbInputEtml.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbInputEtml.Location = new System.Drawing.Point(3, 4);
            this.txbInputEtml.Multiline = true;
            this.txbInputEtml.Name = "txbInputEtml";
            this.txbInputEtml.Size = new System.Drawing.Size(366, 32);
            this.txbInputEtml.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnLogin);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.listViewResultEducanet);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(733, 394);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Educanet2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(250)))), ((int)(((byte)(65)))));
            this.btnLogin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(250)))), ((int)(((byte)(65)))));
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.SystemColors.GrayText;
            this.btnLogin.Location = new System.Drawing.Point(19, 17);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(106, 33);
            this.btnLogin.TabIndex = 24;
            this.btnLogin.Text = "Se Connecter";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(250)))), ((int)(((byte)(65)))));
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(250)))), ((int)(((byte)(65)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(561, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 33);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // listViewResultEducanet
            // 
            this.listViewResultEducanet.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.listViewResultEducanet.GridLines = true;
            this.listViewResultEducanet.Location = new System.Drawing.Point(9, 67);
            this.listViewResultEducanet.Name = "listViewResultEducanet";
            this.listViewResultEducanet.Size = new System.Drawing.Size(715, 318);
            this.listViewResultEducanet.TabIndex = 23;
            this.listViewResultEducanet.UseCompatibleStateImageBehavior = false;
            this.listViewResultEducanet.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Nom";
            this.columnHeader5.Width = 356;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Taille";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Dernière fois modifié";
            this.columnHeader7.Width = 139;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Chemin";
            this.columnHeader8.Width = 153;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(250)))), ((int)(((byte)(65)))));
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.txbInputEducanet);
            this.panel1.Location = new System.Drawing.Point(136, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(395, 40);
            this.panel1.TabIndex = 21;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Controls.Add(this.btnSearchEducanet);
            this.panel2.Location = new System.Drawing.Point(355, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(36, 32);
            this.panel2.TabIndex = 11;
            // 
            // btnSearchEducanet
            // 
            this.btnSearchEducanet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(250)))), ((int)(((byte)(65)))));
            this.btnSearchEducanet.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchEducanet.BackgroundImage")));
            this.btnSearchEducanet.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(250)))), ((int)(((byte)(65)))));
            this.btnSearchEducanet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchEducanet.Location = new System.Drawing.Point(5, 3);
            this.btnSearchEducanet.Name = "btnSearchEducanet";
            this.btnSearchEducanet.Size = new System.Drawing.Size(27, 26);
            this.btnSearchEducanet.TabIndex = 1;
            this.btnSearchEducanet.UseVisualStyleBackColor = false;
            // 
            // txbInputEducanet
            // 
            this.txbInputEducanet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbInputEducanet.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbInputEducanet.Location = new System.Drawing.Point(4, 4);
            this.txbInputEducanet.Multiline = true;
            this.txbInputEducanet.Name = "txbInputEducanet";
            this.txbInputEducanet.Size = new System.Drawing.Size(354, 32);
            this.txbInputEducanet.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.listViewResultTemp);
            this.tabPage3.Controls.Add(this.panel3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(733, 394);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Détails";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // listViewResultTemp
            // 
            this.listViewResultTemp.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colHeadName,
            this.colHeadSize,
            this.colHeadModified,
            this.colHeadDirectory});
            this.listViewResultTemp.GridLines = true;
            this.listViewResultTemp.Location = new System.Drawing.Point(9, 67);
            this.listViewResultTemp.Name = "listViewResultTemp";
            this.listViewResultTemp.Size = new System.Drawing.Size(715, 318);
            this.listViewResultTemp.TabIndex = 22;
            this.listViewResultTemp.UseCompatibleStateImageBehavior = false;
            this.listViewResultTemp.View = System.Windows.Forms.View.Details;
            this.listViewResultTemp.ItemActivate += new System.EventHandler(this.listViewResultTemp_ItemActivate);
            // 
            // colHeadName
            // 
            this.colHeadName.Text = "Nom";
            this.colHeadName.Width = 363;
            // 
            // colHeadSize
            // 
            this.colHeadSize.Text = "Taille";
            // 
            // colHeadModified
            // 
            this.colHeadModified.Text = "Dernière fois modifié";
            this.colHeadModified.Width = 139;
            // 
            // colHeadDirectory
            // 
            this.colHeadDirectory.Text = "Chemin";
            this.colHeadDirectory.Width = 153;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(250)))), ((int)(((byte)(65)))));
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.txbInputTemp);
            this.panel3.Location = new System.Drawing.Point(49, 13);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(482, 40);
            this.panel3.TabIndex = 21;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Window;
            this.panel4.Controls.Add(this.btnSearchTemp);
            this.panel4.Location = new System.Drawing.Point(443, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(36, 32);
            this.panel4.TabIndex = 10;
            // 
            // btnSearchTemp
            // 
            this.btnSearchTemp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(250)))), ((int)(((byte)(65)))));
            this.btnSearchTemp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchTemp.BackgroundImage")));
            this.btnSearchTemp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(250)))), ((int)(((byte)(65)))));
            this.btnSearchTemp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchTemp.Location = new System.Drawing.Point(5, 3);
            this.btnSearchTemp.Name = "btnSearchTemp";
            this.btnSearchTemp.Size = new System.Drawing.Size(27, 26);
            this.btnSearchTemp.TabIndex = 1;
            this.btnSearchTemp.UseVisualStyleBackColor = false;
            this.btnSearchTemp.Click += new System.EventHandler(this.btnSearchTemp_Click);
            // 
            // txbInputTemp
            // 
            this.txbInputTemp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbInputTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbInputTemp.Location = new System.Drawing.Point(3, 4);
            this.txbInputTemp.Multiline = true;
            this.txbInputTemp.Name = "txbInputTemp";
            this.txbInputTemp.Size = new System.Drawing.Size(442, 32);
            this.txbInputTemp.TabIndex = 1;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(230)))), ((int)(((byte)(60)))));
            this.lblSearch.Location = new System.Drawing.Point(289, 35);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(227, 46);
            this.lblSearch.TabIndex = 14;
            this.lblSearch.Text = "Rechercher";
            // 
            // lblBackColor
            // 
            this.lblBackColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(250)))), ((int)(((byte)(65)))));
            this.lblBackColor.Location = new System.Drawing.Point(573, 192);
            this.lblBackColor.Name = "lblBackColor";
            this.lblBackColor.Size = new System.Drawing.Size(155, 34);
            this.lblBackColor.TabIndex = 24;
            this.lblBackColor.Text = " ";
            // 
            // btnHide
            // 
            this.btnHide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(250)))), ((int)(((byte)(65)))));
            this.btnHide.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnHide.BackgroundImage")));
            this.btnHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHide.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHide.ForeColor = System.Drawing.Color.White;
            this.btnHide.Location = new System.Drawing.Point(740, 0);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(31, 22);
            this.btnHide.TabIndex = 18;
            this.btnHide.UseVisualStyleBackColor = false;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(250)))), ((int)(((byte)(65)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(770, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(31, 22);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cmbActualisation
            // 
            this.cmbActualisation.DropDownHeight = 300;
            this.cmbActualisation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActualisation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbActualisation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbActualisation.ForeColor = System.Drawing.Color.Black;
            this.cmbActualisation.FormattingEnabled = true;
            this.cmbActualisation.IntegralHeight = false;
            this.cmbActualisation.Location = new System.Drawing.Point(576, 195);
            this.cmbActualisation.Name = "cmbActualisation";
            this.cmbActualisation.Size = new System.Drawing.Size(150, 28);
            this.cmbActualisation.TabIndex = 23;
            this.cmbActualisation.SelectedIndexChanged += new System.EventHandler(this.cmbActualisation_SelectedIndexChanged);
            // 
            // formSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(250)))), ((int)(((byte)(65)))));
            this.ClientSize = new System.Drawing.Size(801, 600);
            this.Controls.Add(this.lblBackColor);
            this.Controls.Add(this.btnHide);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.cmbActualisation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "formSearch";
            this.Padding = new System.Windows.Forms.Padding(8, 30, 8, 8);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Smart Thésaurus";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.formSearch_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.formSearch_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.formSearch_MouseUp);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.TbCMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.pnlSearchBar.ResumeLayout(false);
            this.pnlSearchBar.PerformLayout();
            this.pblSearchBtn.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cmbActualisation;
        private Label lblBackColor;
        private TabControl TbCMain;
        private TabPage tabPage1;
        private Label label2;
        private Label label1;
        private CheckBox checkbTemp;
        private CheckBox checkbEtml;
        private ListView listViewResultEtml;
        private ColumnHeader colName;
        private ColumnHeader colDirectory;
        private ColumnHeader colSize;
        private Panel pnlSearchBar;
        private Panel pblSearchBtn;
        private Button btnSearchEtml;
        private TextBox txbInputEtml;
        private TabPage tabPage2;
        private Button btnLogin;
        private Button button1;
        private ListView listViewResultEducanet;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private Panel panel1;
        private Panel panel2;
        private Button btnSearchEducanet;
        private TextBox txbInputEducanet;
        private TabPage tabPage3;
        private ListView listViewResultTemp;
        private ColumnHeader colHeadName;
        private ColumnHeader colHeadSize;
        private ColumnHeader colHeadModified;
        private ColumnHeader colHeadDirectory;
        private Panel panel3;
        private Panel panel4;
        private Button btnSearchTemp;
        private TextBox txbInputTemp;
    }
}
