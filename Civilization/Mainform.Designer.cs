namespace Civilization
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.StartButton = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.button_Configure = new System.Windows.Forms.Button();
            this.button_Pause = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button_Reset = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.button_Load = new System.Windows.Forms.Button();
            this.comboBox_Language = new System.Windows.Forms.ComboBox();
            this.hScrollBar_SpeedControl = new System.Windows.Forms.HScrollBar();
            this.label_SpeedControl = new System.Windows.Forms.Label();
            this.button_GameInfo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(13, 21);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(99, 23);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(118, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(802, 438);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            this.pictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseClick);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // button_Configure
            // 
            this.button_Configure.Location = new System.Drawing.Point(13, 66);
            this.button_Configure.Name = "button_Configure";
            this.button_Configure.Size = new System.Drawing.Size(99, 23);
            this.button_Configure.TabIndex = 2;
            this.button_Configure.Text = "Configure";
            this.button_Configure.UseVisualStyleBackColor = true;
            this.button_Configure.Click += new System.EventHandler(this.button_Configure_Click);
            // 
            // button_Pause
            // 
            this.button_Pause.Location = new System.Drawing.Point(13, 114);
            this.button_Pause.Name = "button_Pause";
            this.button_Pause.Size = new System.Drawing.Size(99, 23);
            this.button_Pause.TabIndex = 3;
            this.button_Pause.Text = "Pause";
            this.button_Pause.UseVisualStyleBackColor = true;
            this.button_Pause.Click += new System.EventHandler(this.button_Pause_Click);
            // 
            // button_Reset
            // 
            this.button_Reset.Location = new System.Drawing.Point(13, 159);
            this.button_Reset.Name = "button_Reset";
            this.button_Reset.Size = new System.Drawing.Size(99, 23);
            this.button_Reset.TabIndex = 4;
            this.button_Reset.Text = "Reset";
            this.button_Reset.UseVisualStyleBackColor = true;
            this.button_Reset.Click += new System.EventHandler(this.button_Reset_Click);
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(13, 204);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(99, 23);
            this.button_Save.TabIndex = 5;
            this.button_Save.Text = "Save";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // button_Load
            // 
            this.button_Load.Location = new System.Drawing.Point(13, 250);
            this.button_Load.Name = "button_Load";
            this.button_Load.Size = new System.Drawing.Size(99, 23);
            this.button_Load.TabIndex = 6;
            this.button_Load.Text = "Load";
            this.button_Load.UseVisualStyleBackColor = true;
            this.button_Load.Click += new System.EventHandler(this.button_Load_Click);
            // 
            // comboBox_Language
            // 
            this.comboBox_Language.FormattingEnabled = true;
            this.comboBox_Language.Items.AddRange(new object[] {
            "中文",
            "English"});
            this.comboBox_Language.Location = new System.Drawing.Point(13, 345);
            this.comboBox_Language.Name = "comboBox_Language";
            this.comboBox_Language.Size = new System.Drawing.Size(99, 20);
            this.comboBox_Language.TabIndex = 7;
            this.comboBox_Language.SelectedIndexChanged += new System.EventHandler(this.comboBox_Language_SelectedIndexChanged);
            // 
            // hScrollBar_SpeedControl
            // 
            this.hScrollBar_SpeedControl.Location = new System.Drawing.Point(13, 310);
            this.hScrollBar_SpeedControl.Name = "hScrollBar_SpeedControl";
            this.hScrollBar_SpeedControl.Size = new System.Drawing.Size(99, 17);
            this.hScrollBar_SpeedControl.SmallChange = 10;
            this.hScrollBar_SpeedControl.TabIndex = 8;
            this.hScrollBar_SpeedControl.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_SpeedControl_Scroll);
            // 
            // label_SpeedControl
            // 
            this.label_SpeedControl.AutoSize = true;
            this.label_SpeedControl.Location = new System.Drawing.Point(12, 288);
            this.label_SpeedControl.Name = "label_SpeedControl";
            this.label_SpeedControl.Size = new System.Drawing.Size(83, 12);
            this.label_SpeedControl.TabIndex = 9;
            this.label_SpeedControl.Text = "Speed Control";
            // 
            // button_GameInfo
            // 
            this.button_GameInfo.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_GameInfo.Location = new System.Drawing.Point(13, 380);
            this.button_GameInfo.Name = "button_GameInfo";
            this.button_GameInfo.Size = new System.Drawing.Size(99, 23);
            this.button_GameInfo.TabIndex = 10;
            this.button_GameInfo.Text = "Game Information";
            this.button_GameInfo.UseVisualStyleBackColor = true;
            this.button_GameInfo.Click += new System.EventHandler(this.button_GameInfo_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 462);
            this.Controls.Add(this.button_GameInfo);
            this.Controls.Add(this.label_SpeedControl);
            this.Controls.Add(this.hScrollBar_SpeedControl);
            this.Controls.Add(this.comboBox_Language);
            this.Controls.Add(this.button_Load);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.button_Reset);
            this.Controls.Add(this.button_Pause);
            this.Controls.Add(this.button_Configure);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.StartButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainForm";
            this.Text = "Civilization";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.SizeChanged += new System.EventHandler(this.MainForm_Resize);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button button_Configure;
        private System.Windows.Forms.Button button_Pause;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button_Reset;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button button_Load;
        private System.Windows.Forms.ComboBox comboBox_Language;
        private System.Windows.Forms.HScrollBar hScrollBar_SpeedControl;
        private System.Windows.Forms.Label label_SpeedControl;
        private System.Windows.Forms.Button button_GameInfo;
    }
}

