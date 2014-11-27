namespace GBAIntroManager
{
    partial class MainScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainScreen));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLoadROM = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.btnReadme = new System.Windows.Forms.ToolStripMenuItem();
            this.labelLoadedROM = new System.Windows.Forms.Label();
            this.fileInfoBox = new System.Windows.Forms.GroupBox();
            this.boxPositionSettings = new System.Windows.Forms.GroupBox();
            this.buttonXYPosUnlock = new System.Windows.Forms.Button();
            this.buttonStartPosReset = new System.Windows.Forms.Button();
            this.labelYPos = new System.Windows.Forms.Label();
            this.textBoxYPos = new System.Windows.Forms.TextBox();
            this.labelXPos = new System.Windows.Forms.Label();
            this.textBoxXPos = new System.Windows.Forms.TextBox();
            this.labelMap = new System.Windows.Forms.Label();
            this.textBoxMap = new System.Windows.Forms.TextBox();
            this.labelBank = new System.Windows.Forms.Label();
            this.textBoxMapBank = new System.Windows.Forms.TextBox();
            this.boxVersionSpecific = new System.Windows.Forms.GroupBox();
            this.comboBoxCry = new System.Windows.Forms.ComboBox();
            this.buttonResetCry = new System.Windows.Forms.Button();
            this.buttonTruckRemove = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonResetIntroPKMN = new System.Windows.Forms.Button();
            this.boxStartWith = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonResetStartItems = new System.Windows.Forms.Button();
            this.textBoxMoney = new System.Windows.Forms.TextBox();
            this.labelPCItemAmt = new System.Windows.Forms.Label();
            this.textBoxPCItemAmt = new System.Windows.Forms.TextBox();
            this.labelPCItemID = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxTitleMusic = new System.Windows.Forms.TextBox();
            this.textBoxProfMusic = new System.Windows.Forms.TextBox();
            this.buttonResetMusic = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxSecsOnTitle = new System.Windows.Forms.TextBox();
            this.checkBoxSkipGender = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxIntroPKMN = new System.Windows.Forms.ComboBox();
            this.comboBoxPCItemID = new System.Windows.Forms.ComboBox();
            this.buttonResetSecsOnTitle = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.fileInfoBox.SuspendLayout();
            this.boxPositionSettings.SuspendLayout();
            this.boxVersionSpecific.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.boxStartWith.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(515, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLoadROM,
            this.btnSave,
            this.toolStripSeparator1,
            this.btnExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // btnLoadROM
            // 
            this.btnLoadROM.Name = "btnLoadROM";
            this.btnLoadROM.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.btnLoadROM.Size = new System.Drawing.Size(173, 22);
            this.btnLoadROM.Text = "Load ROM";
            this.btnLoadROM.Click += new System.EventHandler(this.btnLoadROM_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Name = "btnSave";
            this.btnSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.btnSave.Size = new System.Drawing.Size(173, 22);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
            // 
            // btnExit
            // 
            this.btnExit.Name = "btnExit";
            this.btnExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.btnExit.Size = new System.Drawing.Size(173, 22);
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAbout,
            this.btnReadme});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // btnAbout
            // 
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(136, 22);
            this.btnAbout.Text = "About";
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnReadme
            // 
            this.btnReadme.Name = "btnReadme";
            this.btnReadme.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.btnReadme.Size = new System.Drawing.Size(136, 22);
            this.btnReadme.Text = "Readme";
            this.btnReadme.Click += new System.EventHandler(this.btnReadme_Click);
            // 
            // labelLoadedROM
            // 
            this.labelLoadedROM.AutoSize = true;
            this.labelLoadedROM.Location = new System.Drawing.Point(6, 16);
            this.labelLoadedROM.Name = "labelLoadedROM";
            this.labelLoadedROM.Size = new System.Drawing.Size(103, 13);
            this.labelLoadedROM.TabIndex = 1;
            this.labelLoadedROM.Text = "Loaded ROM: None";
            // 
            // fileInfoBox
            // 
            this.fileInfoBox.Controls.Add(this.labelLoadedROM);
            this.fileInfoBox.Location = new System.Drawing.Point(12, 28);
            this.fileInfoBox.Name = "fileInfoBox";
            this.fileInfoBox.Size = new System.Drawing.Size(490, 39);
            this.fileInfoBox.TabIndex = 2;
            this.fileInfoBox.TabStop = false;
            this.fileInfoBox.Text = "File Info";
            // 
            // boxPositionSettings
            // 
            this.boxPositionSettings.Controls.Add(this.buttonXYPosUnlock);
            this.boxPositionSettings.Controls.Add(this.buttonStartPosReset);
            this.boxPositionSettings.Controls.Add(this.labelYPos);
            this.boxPositionSettings.Controls.Add(this.textBoxYPos);
            this.boxPositionSettings.Controls.Add(this.labelXPos);
            this.boxPositionSettings.Controls.Add(this.textBoxXPos);
            this.boxPositionSettings.Controls.Add(this.labelMap);
            this.boxPositionSettings.Controls.Add(this.textBoxMap);
            this.boxPositionSettings.Controls.Add(this.labelBank);
            this.boxPositionSettings.Controls.Add(this.textBoxMapBank);
            this.boxPositionSettings.Location = new System.Drawing.Point(12, 73);
            this.boxPositionSettings.Name = "boxPositionSettings";
            this.boxPositionSettings.Size = new System.Drawing.Size(151, 102);
            this.boxPositionSettings.TabIndex = 3;
            this.boxPositionSettings.TabStop = false;
            this.boxPositionSettings.Text = "Start Position";
            // 
            // buttonXYPosUnlock
            // 
            this.buttonXYPosUnlock.Location = new System.Drawing.Point(79, 34);
            this.buttonXYPosUnlock.Name = "buttonXYPosUnlock";
            this.buttonXYPosUnlock.Size = new System.Drawing.Size(63, 20);
            this.buttonXYPosUnlock.TabIndex = 9;
            this.buttonXYPosUnlock.Text = "Unlock";
            this.buttonXYPosUnlock.UseVisualStyleBackColor = true;
            this.buttonXYPosUnlock.Visible = false;
            this.buttonXYPosUnlock.Click += new System.EventHandler(this.buttonXYPosUnlock_Click);
            // 
            // buttonStartPosReset
            // 
            this.buttonStartPosReset.Enabled = false;
            this.buttonStartPosReset.Location = new System.Drawing.Point(8, 61);
            this.buttonStartPosReset.Name = "buttonStartPosReset";
            this.buttonStartPosReset.Size = new System.Drawing.Size(134, 32);
            this.buttonStartPosReset.TabIndex = 8;
            this.buttonStartPosReset.Text = "Reset to Default";
            this.buttonStartPosReset.UseVisualStyleBackColor = true;
            this.buttonStartPosReset.Click += new System.EventHandler(this.buttonStartPosReset_Click);
            // 
            // labelYPos
            // 
            this.labelYPos.AutoSize = true;
            this.labelYPos.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelYPos.Location = new System.Drawing.Point(110, 18);
            this.labelYPos.Name = "labelYPos";
            this.labelYPos.Size = new System.Drawing.Size(14, 13);
            this.labelYPos.TabIndex = 7;
            this.labelYPos.Text = "Y";
            // 
            // textBoxYPos
            // 
            this.textBoxYPos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxYPos.Enabled = false;
            this.textBoxYPos.Location = new System.Drawing.Point(113, 34);
            this.textBoxYPos.MaxLength = 2;
            this.textBoxYPos.Name = "textBoxYPos";
            this.textBoxYPos.Size = new System.Drawing.Size(29, 20);
            this.textBoxYPos.TabIndex = 6;
            this.textBoxYPos.TextChanged += new System.EventHandler(this.textBoxYPos_TextChanged);
            // 
            // labelXPos
            // 
            this.labelXPos.AutoSize = true;
            this.labelXPos.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelXPos.Location = new System.Drawing.Point(78, 18);
            this.labelXPos.Name = "labelXPos";
            this.labelXPos.Size = new System.Drawing.Size(14, 13);
            this.labelXPos.TabIndex = 5;
            this.labelXPos.Text = "X";
            // 
            // textBoxXPos
            // 
            this.textBoxXPos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxXPos.Enabled = false;
            this.textBoxXPos.Location = new System.Drawing.Point(78, 34);
            this.textBoxXPos.MaxLength = 2;
            this.textBoxXPos.Name = "textBoxXPos";
            this.textBoxXPos.Size = new System.Drawing.Size(29, 20);
            this.textBoxXPos.TabIndex = 4;
            this.textBoxXPos.TextChanged += new System.EventHandler(this.textBoxXPos_TextChanged);
            // 
            // labelMap
            // 
            this.labelMap.AutoSize = true;
            this.labelMap.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelMap.Location = new System.Drawing.Point(40, 18);
            this.labelMap.Name = "labelMap";
            this.labelMap.Size = new System.Drawing.Size(28, 13);
            this.labelMap.TabIndex = 3;
            this.labelMap.Text = "Map";
            // 
            // textBoxMap
            // 
            this.textBoxMap.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxMap.Enabled = false;
            this.textBoxMap.Location = new System.Drawing.Point(43, 34);
            this.textBoxMap.MaxLength = 2;
            this.textBoxMap.Name = "textBoxMap";
            this.textBoxMap.Size = new System.Drawing.Size(29, 20);
            this.textBoxMap.TabIndex = 2;
            this.textBoxMap.TextChanged += new System.EventHandler(this.textBoxMap_TextChanged);
            // 
            // labelBank
            // 
            this.labelBank.AutoSize = true;
            this.labelBank.Location = new System.Drawing.Point(5, 18);
            this.labelBank.Name = "labelBank";
            this.labelBank.Size = new System.Drawing.Size(32, 13);
            this.labelBank.TabIndex = 1;
            this.labelBank.Text = "Bank";
            // 
            // textBoxMapBank
            // 
            this.textBoxMapBank.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxMapBank.Enabled = false;
            this.textBoxMapBank.Location = new System.Drawing.Point(8, 34);
            this.textBoxMapBank.MaxLength = 2;
            this.textBoxMapBank.Name = "textBoxMapBank";
            this.textBoxMapBank.Size = new System.Drawing.Size(29, 20);
            this.textBoxMapBank.TabIndex = 0;
            this.textBoxMapBank.TextChanged += new System.EventHandler(this.textBoxMapBank_TextChanged);
            // 
            // boxVersionSpecific
            // 
            this.boxVersionSpecific.Controls.Add(this.comboBoxCry);
            this.boxVersionSpecific.Controls.Add(this.buttonResetCry);
            this.boxVersionSpecific.Controls.Add(this.buttonTruckRemove);
            this.boxVersionSpecific.Location = new System.Drawing.Point(12, 181);
            this.boxVersionSpecific.Name = "boxVersionSpecific";
            this.boxVersionSpecific.Size = new System.Drawing.Size(151, 77);
            this.boxVersionSpecific.TabIndex = 10;
            this.boxVersionSpecific.TabStop = false;
            this.boxVersionSpecific.Text = "Titlescreen Pokémon Cry";
            // 
            // comboBoxCry
            // 
            this.comboBoxCry.Enabled = false;
            this.comboBoxCry.FormattingEnabled = true;
            this.comboBoxCry.Location = new System.Drawing.Point(9, 20);
            this.comboBoxCry.Name = "comboBoxCry";
            this.comboBoxCry.Size = new System.Drawing.Size(133, 21);
            this.comboBoxCry.TabIndex = 11;
            // 
            // buttonResetCry
            // 
            this.buttonResetCry.Enabled = false;
            this.buttonResetCry.Location = new System.Drawing.Point(8, 46);
            this.buttonResetCry.Name = "buttonResetCry";
            this.buttonResetCry.Size = new System.Drawing.Size(134, 23);
            this.buttonResetCry.TabIndex = 8;
            this.buttonResetCry.Text = "Reset to Default";
            this.buttonResetCry.UseVisualStyleBackColor = true;
            this.buttonResetCry.Click += new System.EventHandler(this.buttonResetCry_Click);
            // 
            // buttonTruckRemove
            // 
            this.buttonTruckRemove.Location = new System.Drawing.Point(8, 19);
            this.buttonTruckRemove.Name = "buttonTruckRemove";
            this.buttonTruckRemove.Size = new System.Drawing.Size(134, 50);
            this.buttonTruckRemove.TabIndex = 9;
            this.buttonTruckRemove.Text = "Remove the Truck";
            this.buttonTruckRemove.UseVisualStyleBackColor = true;
            this.buttonTruckRemove.Visible = false;
            this.buttonTruckRemove.Click += new System.EventHandler(this.buttonTruckRemove_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxIntroPKMN);
            this.groupBox1.Controls.Add(this.buttonResetIntroPKMN);
            this.groupBox1.Location = new System.Drawing.Point(169, 181);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(137, 77);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Professor Intro Pokémon";
            // 
            // buttonResetIntroPKMN
            // 
            this.buttonResetIntroPKMN.Enabled = false;
            this.buttonResetIntroPKMN.Location = new System.Drawing.Point(8, 46);
            this.buttonResetIntroPKMN.Name = "buttonResetIntroPKMN";
            this.buttonResetIntroPKMN.Size = new System.Drawing.Size(123, 23);
            this.buttonResetIntroPKMN.TabIndex = 8;
            this.buttonResetIntroPKMN.Text = "Reset to Default";
            this.buttonResetIntroPKMN.UseVisualStyleBackColor = true;
            this.buttonResetIntroPKMN.Click += new System.EventHandler(this.buttonResetIntroPKMN_Click);
            // 
            // boxStartWith
            // 
            this.boxStartWith.Controls.Add(this.comboBoxPCItemID);
            this.boxStartWith.Controls.Add(this.label4);
            this.boxStartWith.Controls.Add(this.buttonResetStartItems);
            this.boxStartWith.Controls.Add(this.textBoxMoney);
            this.boxStartWith.Controls.Add(this.labelPCItemAmt);
            this.boxStartWith.Controls.Add(this.textBoxPCItemAmt);
            this.boxStartWith.Controls.Add(this.labelPCItemID);
            this.boxStartWith.Location = new System.Drawing.Point(312, 73);
            this.boxStartWith.Name = "boxStartWith";
            this.boxStartWith.Size = new System.Drawing.Size(190, 102);
            this.boxStartWith.TabIndex = 10;
            this.boxStartWith.TabStop = false;
            this.boxStartWith.Text = "Starting Items";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(5, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Money";
            // 
            // buttonResetStartItems
            // 
            this.buttonResetStartItems.Enabled = false;
            this.buttonResetStartItems.Location = new System.Drawing.Point(136, 44);
            this.buttonResetStartItems.Name = "buttonResetStartItems";
            this.buttonResetStartItems.Size = new System.Drawing.Size(48, 46);
            this.buttonResetStartItems.TabIndex = 8;
            this.buttonResetStartItems.Text = "Reset";
            this.buttonResetStartItems.UseVisualStyleBackColor = true;
            this.buttonResetStartItems.Click += new System.EventHandler(this.buttonResetStartItems_Click);
            // 
            // textBoxMoney
            // 
            this.textBoxMoney.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxMoney.Enabled = false;
            this.textBoxMoney.Location = new System.Drawing.Point(66, 70);
            this.textBoxMoney.MaxLength = 6;
            this.textBoxMoney.Name = "textBoxMoney";
            this.textBoxMoney.Size = new System.Drawing.Size(64, 20);
            this.textBoxMoney.TabIndex = 4;
            this.textBoxMoney.TextChanged += new System.EventHandler(this.textBoxMoney_TextChanged);
            // 
            // labelPCItemAmt
            // 
            this.labelPCItemAmt.AutoSize = true;
            this.labelPCItemAmt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelPCItemAmt.Location = new System.Drawing.Point(6, 47);
            this.labelPCItemAmt.Name = "labelPCItemAmt";
            this.labelPCItemAmt.Size = new System.Drawing.Size(43, 13);
            this.labelPCItemAmt.TabIndex = 3;
            this.labelPCItemAmt.Text = "Amount";
            // 
            // textBoxPCItemAmt
            // 
            this.textBoxPCItemAmt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxPCItemAmt.Enabled = false;
            this.textBoxPCItemAmt.Location = new System.Drawing.Point(66, 44);
            this.textBoxPCItemAmt.MaxLength = 2;
            this.textBoxPCItemAmt.Name = "textBoxPCItemAmt";
            this.textBoxPCItemAmt.Size = new System.Drawing.Size(64, 20);
            this.textBoxPCItemAmt.TabIndex = 2;
            this.textBoxPCItemAmt.TextChanged += new System.EventHandler(this.textBoxPCItemAmt_TextChanged);
            // 
            // labelPCItemID
            // 
            this.labelPCItemID.AutoSize = true;
            this.labelPCItemID.Location = new System.Drawing.Point(6, 20);
            this.labelPCItemID.Name = "labelPCItemID";
            this.labelPCItemID.Size = new System.Drawing.Size(55, 13);
            this.labelPCItemID.TabIndex = 1;
            this.labelPCItemID.Text = "Item in PC";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxTitleMusic);
            this.groupBox3.Controls.Add(this.textBoxProfMusic);
            this.groupBox3.Controls.Add(this.buttonResetMusic);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(169, 73);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(137, 102);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Music";
            // 
            // textBoxTitleMusic
            // 
            this.textBoxTitleMusic.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxTitleMusic.Enabled = false;
            this.textBoxTitleMusic.Location = new System.Drawing.Point(70, 15);
            this.textBoxTitleMusic.MaxLength = 3;
            this.textBoxTitleMusic.Name = "textBoxTitleMusic";
            this.textBoxTitleMusic.Size = new System.Drawing.Size(61, 20);
            this.textBoxTitleMusic.TabIndex = 11;
            this.textBoxTitleMusic.TextChanged += new System.EventHandler(this.textBoxTitleMusic_TextChanged);
            // 
            // textBoxProfMusic
            // 
            this.textBoxProfMusic.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxProfMusic.Enabled = false;
            this.textBoxProfMusic.Location = new System.Drawing.Point(70, 44);
            this.textBoxProfMusic.MaxLength = 3;
            this.textBoxProfMusic.Name = "textBoxProfMusic";
            this.textBoxProfMusic.Size = new System.Drawing.Size(61, 20);
            this.textBoxProfMusic.TabIndex = 12;
            this.textBoxProfMusic.TextChanged += new System.EventHandler(this.textBoxProfMusic_TextChanged);
            // 
            // buttonResetMusic
            // 
            this.buttonResetMusic.Enabled = false;
            this.buttonResetMusic.Location = new System.Drawing.Point(8, 70);
            this.buttonResetMusic.Name = "buttonResetMusic";
            this.buttonResetMusic.Size = new System.Drawing.Size(123, 23);
            this.buttonResetMusic.TabIndex = 8;
            this.buttonResetMusic.Text = "Reset to Default";
            this.buttonResetMusic.UseVisualStyleBackColor = true;
            this.buttonResetMusic.Click += new System.EventHandler(this.buttonResetMusic_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(5, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Prof. Intro";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Titlescreen";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Secs on Title";
            // 
            // textBoxSecsOnTitle
            // 
            this.textBoxSecsOnTitle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxSecsOnTitle.Enabled = false;
            this.textBoxSecsOnTitle.Location = new System.Drawing.Point(80, 19);
            this.textBoxSecsOnTitle.MaxLength = 2;
            this.textBoxSecsOnTitle.Name = "textBoxSecsOnTitle";
            this.textBoxSecsOnTitle.Size = new System.Drawing.Size(50, 20);
            this.textBoxSecsOnTitle.TabIndex = 10;
            // 
            // checkBoxSkipGender
            // 
            this.checkBoxSkipGender.AutoSize = true;
            this.checkBoxSkipGender.Enabled = false;
            this.checkBoxSkipGender.Location = new System.Drawing.Point(34, 50);
            this.checkBoxSkipGender.Name = "checkBoxSkipGender";
            this.checkBoxSkipGender.Size = new System.Drawing.Size(121, 17);
            this.checkBoxSkipGender.TabIndex = 13;
            this.checkBoxSkipGender.Text = "Skip Gender Choice";
            this.checkBoxSkipGender.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonResetSecsOnTitle);
            this.groupBox2.Controls.Add(this.checkBoxSkipGender);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBoxSecsOnTitle);
            this.groupBox2.Location = new System.Drawing.Point(312, 181);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(190, 77);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Other";
            // 
            // comboBoxIntroPKMN
            // 
            this.comboBoxIntroPKMN.Enabled = false;
            this.comboBoxIntroPKMN.FormattingEnabled = true;
            this.comboBoxIntroPKMN.Location = new System.Drawing.Point(9, 19);
            this.comboBoxIntroPKMN.Name = "comboBoxIntroPKMN";
            this.comboBoxIntroPKMN.Size = new System.Drawing.Size(122, 21);
            this.comboBoxIntroPKMN.TabIndex = 12;
            // 
            // comboBoxPCItemID
            // 
            this.comboBoxPCItemID.Enabled = false;
            this.comboBoxPCItemID.FormattingEnabled = true;
            this.comboBoxPCItemID.Location = new System.Drawing.Point(66, 17);
            this.comboBoxPCItemID.Name = "comboBoxPCItemID";
            this.comboBoxPCItemID.Size = new System.Drawing.Size(118, 21);
            this.comboBoxPCItemID.TabIndex = 13;
            this.comboBoxPCItemID.SelectedIndexChanged += new System.EventHandler(this.comboBoxPCItemID_SelectedIndexChanged);
            // 
            // buttonResetSecsOnTitle
            // 
            this.buttonResetSecsOnTitle.Enabled = false;
            this.buttonResetSecsOnTitle.Location = new System.Drawing.Point(136, 17);
            this.buttonResetSecsOnTitle.Name = "buttonResetSecsOnTitle";
            this.buttonResetSecsOnTitle.Size = new System.Drawing.Size(48, 23);
            this.buttonResetSecsOnTitle.TabIndex = 14;
            this.buttonResetSecsOnTitle.Text = "Reset";
            this.buttonResetSecsOnTitle.UseVisualStyleBackColor = true;
            this.buttonResetSecsOnTitle.Click += new System.EventHandler(this.buttonResetSecsOnTitle_Click);
            // 
            // MainScreen
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 265);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.boxStartWith);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.boxVersionSpecific);
            this.Controls.Add(this.boxPositionSettings);
            this.Controls.Add(this.fileInfoBox);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainScreen";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "GBA Intro Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.fileInfoBox.ResumeLayout(false);
            this.fileInfoBox.PerformLayout();
            this.boxPositionSettings.ResumeLayout(false);
            this.boxPositionSettings.PerformLayout();
            this.boxVersionSpecific.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.boxStartWith.ResumeLayout(false);
            this.boxStartWith.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnLoadROM;
        private System.Windows.Forms.ToolStripMenuItem btnSave;
        private System.Windows.Forms.ToolStripMenuItem btnExit;
        private System.Windows.Forms.Label labelLoadedROM;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnAbout;
        private System.Windows.Forms.GroupBox fileInfoBox;
        private System.Windows.Forms.GroupBox boxPositionSettings;
        private System.Windows.Forms.TextBox textBoxMapBank;
        private System.Windows.Forms.Label labelMap;
        private System.Windows.Forms.TextBox textBoxMap;
        private System.Windows.Forms.Label labelBank;
        private System.Windows.Forms.Label labelYPos;
        private System.Windows.Forms.TextBox textBoxYPos;
        private System.Windows.Forms.Label labelXPos;
        private System.Windows.Forms.TextBox textBoxXPos;
        private System.Windows.Forms.Button buttonStartPosReset;
        private System.Windows.Forms.Button buttonXYPosUnlock;
        private System.Windows.Forms.ToolStripMenuItem btnReadme;
        private System.Windows.Forms.GroupBox boxVersionSpecific;
        private System.Windows.Forms.Button buttonResetCry;
        private System.Windows.Forms.Button buttonTruckRemove;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonResetIntroPKMN;
        private System.Windows.Forms.GroupBox boxStartWith;
        private System.Windows.Forms.Button buttonResetStartItems;
        private System.Windows.Forms.Label labelPCItemAmt;
        private System.Windows.Forms.TextBox textBoxPCItemAmt;
        private System.Windows.Forms.Label labelPCItemID;
        private System.Windows.Forms.TextBox textBoxMoney;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxTitleMusic;
        private System.Windows.Forms.TextBox textBoxProfMusic;
        private System.Windows.Forms.Button buttonResetMusic;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxSecsOnTitle;
        private System.Windows.Forms.CheckBox checkBoxSkipGender;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBoxCry;
        private System.Windows.Forms.ComboBox comboBoxIntroPKMN;
        private System.Windows.Forms.ComboBox comboBoxPCItemID;
        private System.Windows.Forms.Button buttonResetSecsOnTitle;
    }
}

