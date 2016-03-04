namespace Civilization
{
    partial class GameInformation
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
            this.label_Population = new System.Windows.Forms.Label();
            this.textBox_Population = new System.Windows.Forms.TextBox();
            this.label_CivNum = new System.Windows.Forms.Label();
            this.textBox_CivNum = new System.Windows.Forms.TextBox();
            this.comboBox_Civ = new System.Windows.Forms.ComboBox();
            this.groupBox_civ = new System.Windows.Forms.GroupBox();
            this.label_pop = new System.Windows.Forms.Label();
            this.textBox_Pop = new System.Windows.Forms.TextBox();
            this.groupBox_civ.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_Population
            // 
            this.label_Population.AutoSize = true;
            this.label_Population.Location = new System.Drawing.Point(13, 13);
            this.label_Population.Name = "label_Population";
            this.label_Population.Size = new System.Drawing.Size(101, 12);
            this.label_Population.TabIndex = 0;
            this.label_Population.Text = "Total Population";
            // 
            // textBox_Population
            // 
            this.textBox_Population.Location = new System.Drawing.Point(148, 10);
            this.textBox_Population.Name = "textBox_Population";
            this.textBox_Population.ReadOnly = true;
            this.textBox_Population.Size = new System.Drawing.Size(100, 21);
            this.textBox_Population.TabIndex = 1;
            // 
            // label_CivNum
            // 
            this.label_CivNum.AutoSize = true;
            this.label_CivNum.Location = new System.Drawing.Point(15, 46);
            this.label_CivNum.Name = "label_CivNum";
            this.label_CivNum.Size = new System.Drawing.Size(119, 12);
            this.label_CivNum.TabIndex = 2;
            this.label_CivNum.Text = "Civilization Number";
            // 
            // textBox_CivNum
            // 
            this.textBox_CivNum.Location = new System.Drawing.Point(148, 43);
            this.textBox_CivNum.Name = "textBox_CivNum";
            this.textBox_CivNum.ReadOnly = true;
            this.textBox_CivNum.Size = new System.Drawing.Size(100, 21);
            this.textBox_CivNum.TabIndex = 3;
            // 
            // comboBox_Civ
            // 
            this.comboBox_Civ.FormattingEnabled = true;
            this.comboBox_Civ.Location = new System.Drawing.Point(15, 79);
            this.comboBox_Civ.Name = "comboBox_Civ";
            this.comboBox_Civ.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Civ.TabIndex = 4;
            this.comboBox_Civ.SelectedIndexChanged += new System.EventHandler(this.comboBox_Civ_SelectedIndexChanged);
            // 
            // groupBox_civ
            // 
            this.groupBox_civ.Controls.Add(this.textBox_Pop);
            this.groupBox_civ.Controls.Add(this.label_pop);
            this.groupBox_civ.Location = new System.Drawing.Point(15, 106);
            this.groupBox_civ.Name = "groupBox_civ";
            this.groupBox_civ.Size = new System.Drawing.Size(233, 183);
            this.groupBox_civ.TabIndex = 5;
            this.groupBox_civ.TabStop = false;
            // 
            // label_pop
            // 
            this.label_pop.AutoSize = true;
            this.label_pop.Location = new System.Drawing.Point(7, 21);
            this.label_pop.Name = "label_pop";
            this.label_pop.Size = new System.Drawing.Size(65, 12);
            this.label_pop.TabIndex = 0;
            this.label_pop.Text = "Population";
            // 
            // textBox_Pop
            // 
            this.textBox_Pop.Location = new System.Drawing.Point(115, 21);
            this.textBox_Pop.Name = "textBox_Pop";
            this.textBox_Pop.ReadOnly = true;
            this.textBox_Pop.Size = new System.Drawing.Size(100, 21);
            this.textBox_Pop.TabIndex = 6;
            // 
            // GameInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 301);
            this.Controls.Add(this.groupBox_civ);
            this.Controls.Add(this.comboBox_Civ);
            this.Controls.Add(this.textBox_CivNum);
            this.Controls.Add(this.label_CivNum);
            this.Controls.Add(this.textBox_Population);
            this.Controls.Add(this.label_Population);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "GameInformation";
            this.Text = "GameInformation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameInformation_FormClosing);
            this.groupBox_civ.ResumeLayout(false);
            this.groupBox_civ.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Population;
        private System.Windows.Forms.TextBox textBox_Population;
        private System.Windows.Forms.Label label_CivNum;
        private System.Windows.Forms.TextBox textBox_CivNum;
        private System.Windows.Forms.ComboBox comboBox_Civ;
        private System.Windows.Forms.GroupBox groupBox_civ;
        private System.Windows.Forms.TextBox textBox_Pop;
        private System.Windows.Forms.Label label_pop;
    }
}