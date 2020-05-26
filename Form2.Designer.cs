namespace testUI
{
    partial class Form2
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
            this.button1 = new System.Windows.Forms.Button();
            this.nodeNumberInput = new System.Windows.Forms.TextBox();
            this.nodeNumberLabel = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.nodePanel = new System.Windows.Forms.Panel();
            this.setEdgeCapacityButton = new System.Windows.Forms.Button();
            this.setEdgeCapacityTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.addChildNodeButton = new System.Windows.Forms.Button();
            this.nodeSettingsLabel = new System.Windows.Forms.Label();
            this.selectANodeLabel = new System.Windows.Forms.Label();
            this.addChildToTheNodeLabel = new System.Windows.Forms.Label();
            this.addChildNodeDropDown = new System.Windows.Forms.ComboBox();
            this.updateNodeNameButton = new System.Windows.Forms.Button();
            this.newNodeNameTextBox = new System.Windows.Forms.TextBox();
            this.newNodeNameLabel = new System.Windows.Forms.Label();
            this.nodeDropDown = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nodePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.Location = new System.Drawing.Point(703, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // nodeNumberInput
            // 
            this.nodeNumberInput.Location = new System.Drawing.Point(152, 9);
            this.nodeNumberInput.MaxLength = 100;
            this.nodeNumberInput.Name = "nodeNumberInput";
            this.nodeNumberInput.Size = new System.Drawing.Size(69, 20);
            this.nodeNumberInput.TabIndex = 1;
            this.nodeNumberInput.TextChanged += new System.EventHandler(this.nodeNumberInput_TextChanged);
            // 
            // nodeNumberLabel
            // 
            this.nodeNumberLabel.AutoSize = true;
            this.nodeNumberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.nodeNumberLabel.ForeColor = System.Drawing.Color.Orange;
            this.nodeNumberLabel.Location = new System.Drawing.Point(14, 12);
            this.nodeNumberLabel.Name = "nodeNumberLabel";
            this.nodeNumberLabel.Size = new System.Drawing.Size(132, 13);
            this.nodeNumberLabel.TabIndex = 2;
            this.nodeNumberLabel.Text = "Toplam Düğüm Sayısı:";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SeaShell;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(66, 35);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Düğüm Sayısı Belirle";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // nodePanel
            // 
            this.nodePanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.nodePanel.Controls.Add(this.button5);
            this.nodePanel.Controls.Add(this.label1);
            this.nodePanel.Controls.Add(this.setEdgeCapacityButton);
            this.nodePanel.Controls.Add(this.setEdgeCapacityTextBox);
            this.nodePanel.Controls.Add(this.label2);
            this.nodePanel.Controls.Add(this.button4);
            this.nodePanel.Controls.Add(this.addChildNodeButton);
            this.nodePanel.Controls.Add(this.nodeSettingsLabel);
            this.nodePanel.Controls.Add(this.selectANodeLabel);
            this.nodePanel.Controls.Add(this.addChildToTheNodeLabel);
            this.nodePanel.Controls.Add(this.addChildNodeDropDown);
            this.nodePanel.Controls.Add(this.updateNodeNameButton);
            this.nodePanel.Controls.Add(this.newNodeNameTextBox);
            this.nodePanel.Controls.Add(this.newNodeNameLabel);
            this.nodePanel.Controls.Add(this.nodeDropDown);
            this.nodePanel.Location = new System.Drawing.Point(34, 138);
            this.nodePanel.Name = "nodePanel";
            this.nodePanel.Size = new System.Drawing.Size(254, 569);
            this.nodePanel.TabIndex = 4;
            this.nodePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.nodePanel_Paint);
            // 
            // setEdgeCapacityButton
            // 
            this.setEdgeCapacityButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.setEdgeCapacityButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.setEdgeCapacityButton.Location = new System.Drawing.Point(57, 401);
            this.setEdgeCapacityButton.Name = "setEdgeCapacityButton";
            this.setEdgeCapacityButton.Size = new System.Drawing.Size(113, 23);
            this.setEdgeCapacityButton.TabIndex = 16;
            this.setEdgeCapacityButton.Text = "Limit Belirle";
            this.setEdgeCapacityButton.UseVisualStyleBackColor = false;
            this.setEdgeCapacityButton.Click += new System.EventHandler(this.button5_Click);
            // 
            // setEdgeCapacityTextBox
            // 
            this.setEdgeCapacityTextBox.Location = new System.Drawing.Point(54, 363);
            this.setEdgeCapacityTextBox.Name = "setEdgeCapacityTextBox";
            this.setEdgeCapacityTextBox.Size = new System.Drawing.Size(129, 20);
            this.setEdgeCapacityTextBox.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.Orange;
            this.label2.Location = new System.Drawing.Point(2, 309);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(248, 39);
            this.label2.TabIndex = 14;
            this.label2.Text = "Seçili Ana Düğüm \r\nve \r\nÇocuk Düğüm Arasındaki Kenar Kapasitesi";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(57, 519);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(122, 23);
            this.button4.TabIndex = 13;
            this.button4.Text = "Yolları Bul";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // addChildNodeButton
            // 
            this.addChildNodeButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.addChildNodeButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.addChildNodeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SeaShell;
            this.addChildNodeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addChildNodeButton.Location = new System.Drawing.Point(66, 283);
            this.addChildNodeButton.Name = "addChildNodeButton";
            this.addChildNodeButton.Size = new System.Drawing.Size(114, 23);
            this.addChildNodeButton.TabIndex = 9;
            this.addChildNodeButton.Text = "Seçili Düğümü Ekle";
            this.addChildNodeButton.UseVisualStyleBackColor = false;
            this.addChildNodeButton.Click += new System.EventHandler(this.addChildNodeButton_Click);
            // 
            // nodeSettingsLabel
            // 
            this.nodeSettingsLabel.AutoSize = true;
            this.nodeSettingsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.nodeSettingsLabel.ForeColor = System.Drawing.Color.Crimson;
            this.nodeSettingsLabel.Location = new System.Drawing.Point(52, 9);
            this.nodeSettingsLabel.Name = "nodeSettingsLabel";
            this.nodeSettingsLabel.Size = new System.Drawing.Size(173, 25);
            this.nodeSettingsLabel.TabIndex = 8;
            this.nodeSettingsLabel.Text = "Düğüm Ayarları";
            // 
            // selectANodeLabel
            // 
            this.selectANodeLabel.AutoSize = true;
            this.selectANodeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.selectANodeLabel.ForeColor = System.Drawing.Color.Orange;
            this.selectANodeLabel.Location = new System.Drawing.Point(39, 44);
            this.selectANodeLabel.Name = "selectANodeLabel";
            this.selectANodeLabel.Size = new System.Drawing.Size(193, 16);
            this.selectANodeLabel.TabIndex = 7;
            this.selectANodeLabel.Text = "Ayarları Değişecek Düğüm";
            // 
            // addChildToTheNodeLabel
            // 
            this.addChildToTheNodeLabel.AutoSize = true;
            this.addChildToTheNodeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.addChildToTheNodeLabel.ForeColor = System.Drawing.Color.Orange;
            this.addChildToTheNodeLabel.Location = new System.Drawing.Point(54, 201);
            this.addChildToTheNodeLabel.Name = "addChildToTheNodeLabel";
            this.addChildToTheNodeLabel.Size = new System.Drawing.Size(134, 16);
            this.addChildToTheNodeLabel.TabIndex = 6;
            this.addChildToTheNodeLabel.Text = "Çocuk Düğüm Seç";
            this.addChildToTheNodeLabel.Click += new System.EventHandler(this.label2_Click);
            // 
            // addChildNodeDropDown
            // 
            this.addChildNodeDropDown.FormattingEnabled = true;
            this.addChildNodeDropDown.Location = new System.Drawing.Point(54, 244);
            this.addChildNodeDropDown.Name = "addChildNodeDropDown";
            this.addChildNodeDropDown.Size = new System.Drawing.Size(146, 21);
            this.addChildNodeDropDown.TabIndex = 5;
            // 
            // updateNodeNameButton
            // 
            this.updateNodeNameButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.updateNodeNameButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.updateNodeNameButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.updateNodeNameButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SeaShell;
            this.updateNodeNameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updateNodeNameButton.Location = new System.Drawing.Point(65, 140);
            this.updateNodeNameButton.Name = "updateNodeNameButton";
            this.updateNodeNameButton.Size = new System.Drawing.Size(114, 23);
            this.updateNodeNameButton.TabIndex = 3;
            this.updateNodeNameButton.Text = "Düğüm Adı Güncelle";
            this.updateNodeNameButton.UseVisualStyleBackColor = false;
            this.updateNodeNameButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // newNodeNameTextBox
            // 
            this.newNodeNameTextBox.Location = new System.Drawing.Point(167, 114);
            this.newNodeNameTextBox.Name = "newNodeNameTextBox";
            this.newNodeNameTextBox.Size = new System.Drawing.Size(33, 20);
            this.newNodeNameTextBox.TabIndex = 2;
            // 
            // newNodeNameLabel
            // 
            this.newNodeNameLabel.AutoSize = true;
            this.newNodeNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.newNodeNameLabel.ForeColor = System.Drawing.Color.Orange;
            this.newNodeNameLabel.Location = new System.Drawing.Point(39, 115);
            this.newNodeNameLabel.Name = "newNodeNameLabel";
            this.newNodeNameLabel.Size = new System.Drawing.Size(122, 16);
            this.newNodeNameLabel.TabIndex = 1;
            this.newNodeNameLabel.Text = "Yeni Düğüm Adı:";
            // 
            // nodeDropDown
            // 
            this.nodeDropDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nodeDropDown.FormattingEnabled = true;
            this.nodeDropDown.Location = new System.Drawing.Point(59, 63);
            this.nodeDropDown.Name = "nodeDropDown";
            this.nodeDropDown.Size = new System.Drawing.Size(129, 21);
            this.nodeDropDown.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(34, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(282, 29);
            this.label3.TabIndex = 5;
            this.label3.Text = "AKIŞ GRAFI OLUŞTUR";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.nodeNumberLabel);
            this.panel1.Controls.Add(this.nodeNumberInput);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(34, 59);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(254, 73);
            this.panel1.TabIndex = 6;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(66, 475);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(107, 27);
            this.button5.TabIndex = 7;
            this.button5.Text = "Çiz";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 446);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Graf Çiz";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 729);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nodePanel);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.nodePanel.ResumeLayout(false);
            this.nodePanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox nodeNumberInput;
        private System.Windows.Forms.Label nodeNumberLabel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel nodePanel;
        private System.Windows.Forms.ComboBox nodeDropDown;
        private System.Windows.Forms.Label newNodeNameLabel;
        private System.Windows.Forms.TextBox newNodeNameTextBox;
        private System.Windows.Forms.Button updateNodeNameButton;
        private System.Windows.Forms.Label addChildToTheNodeLabel;
        private System.Windows.Forms.ComboBox addChildNodeDropDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button addChildNodeButton;
        private System.Windows.Forms.Label nodeSettingsLabel;
        private System.Windows.Forms.Label selectANodeLabel;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button setEdgeCapacityButton;
        private System.Windows.Forms.TextBox setEdgeCapacityTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label1;
    }
}