namespace Civilization
{
    partial class ConfigureForm
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
            this.checkBox_Human = new System.Windows.Forms.CheckBox();
            this.checkBox_Grass = new System.Windows.Forms.CheckBox();
            this.checkBox_Water = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBox_Human
            // 
            this.checkBox_Human.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_Human.AutoSize = true;
            this.checkBox_Human.Location = new System.Drawing.Point(12, 12);
            this.checkBox_Human.Name = "checkBox_Human";
            this.checkBox_Human.Size = new System.Drawing.Size(45, 22);
            this.checkBox_Human.TabIndex = 1;
            this.checkBox_Human.Text = "Human";
            this.checkBox_Human.UseVisualStyleBackColor = true;
            this.checkBox_Human.CheckedChanged += new System.EventHandler(this.checkBox_Human_CheckedChanged);
            // 
            // checkBox_Grass
            // 
            this.checkBox_Grass.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_Grass.AutoSize = true;
            this.checkBox_Grass.Location = new System.Drawing.Point(76, 12);
            this.checkBox_Grass.Name = "checkBox_Grass";
            this.checkBox_Grass.Size = new System.Drawing.Size(45, 22);
            this.checkBox_Grass.TabIndex = 2;
            this.checkBox_Grass.Text = "Grass";
            this.checkBox_Grass.UseVisualStyleBackColor = true;
            this.checkBox_Grass.CheckedChanged += new System.EventHandler(this.checkBox_Grass_CheckedChanged);
            // 
            // checkBox_Water
            // 
            this.checkBox_Water.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_Water.AutoSize = true;
            this.checkBox_Water.Location = new System.Drawing.Point(142, 12);
            this.checkBox_Water.Name = "checkBox_Water";
            this.checkBox_Water.Size = new System.Drawing.Size(45, 22);
            this.checkBox_Water.TabIndex = 3;
            this.checkBox_Water.Text = "Water";
            this.checkBox_Water.UseVisualStyleBackColor = true;
            this.checkBox_Water.CheckedChanged += new System.EventHandler(this.checkBox_Water_CheckedChanged);
            // 
            // ConfigureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 262);
            this.Controls.Add(this.checkBox_Water);
            this.Controls.Add(this.checkBox_Grass);
            this.Controls.Add(this.checkBox_Human);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ConfigureForm";
            this.Text = "ConfigureForm";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigureForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_Human;
        private System.Windows.Forms.CheckBox checkBox_Grass;
        private System.Windows.Forms.CheckBox checkBox_Water;

    }
}