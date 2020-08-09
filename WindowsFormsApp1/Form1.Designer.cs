namespace WindowsFormsApp1
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.textBoxMarked = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBoxEdition = new System.Windows.Forms.GroupBox();
            this.buttonBlockRemove = new WindowsFormsApp1.Form1.ChooseButton();
            this.buttonBlockDecision = new WindowsFormsApp1.Form1.ChooseButton();
            this.buttonBlockBind = new WindowsFormsApp1.Form1.ChooseButton();
            this.buttonBlockOperation = new WindowsFormsApp1.Form1.ChooseButton();
            this.buttonBlockStop = new WindowsFormsApp1.Form1.ChooseButton();
            this.buttonBlockStart = new WindowsFormsApp1.Form1.ChooseButton();
            this.groupBoxLanguage = new System.Windows.Forms.GroupBox();
            this.buttonLanguage1 = new System.Windows.Forms.Button();
            this.buttonLanguage2 = new System.Windows.Forms.Button();
            this.groupBoxFile = new System.Windows.Forms.GroupBox();
            this.buttonScheme3 = new System.Windows.Forms.Button();
            this.buttonScheme2 = new System.Windows.Forms.Button();
            this.buttonScheme1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.groupBoxEdition.SuspendLayout();
            this.groupBoxLanguage.SuspendLayout();
            this.groupBoxFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            resources.ApplyResources(this.splitContainer.Panel1, "splitContainer.Panel1");
            this.splitContainer.Panel1.Controls.Add(this.vScrollBar1);
            this.splitContainer.Panel1.Controls.Add(this.hScrollBar1);
            this.splitContainer.Panel1.Controls.Add(this.pictureBox);
            // 
            // splitContainer.Panel2
            // 
            resources.ApplyResources(this.splitContainer.Panel2, "splitContainer.Panel2");
            this.splitContainer.Panel2.Controls.Add(this.textBoxMarked);
            this.splitContainer.Panel2.Controls.Add(this.textBox1);
            this.splitContainer.Panel2.Controls.Add(this.groupBoxEdition);
            this.splitContainer.Panel2.Controls.Add(this.groupBoxLanguage);
            this.splitContainer.Panel2.Controls.Add(this.groupBoxFile);
            // 
            // vScrollBar1
            // 
            resources.ApplyResources(this.vScrollBar1, "vScrollBar1");
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            this.vScrollBar1.ValueChanged += new System.EventHandler(this.vScrollBar1_ValueChanged_1);
            // 
            // hScrollBar1
            // 
            resources.ApplyResources(this.hScrollBar1, "hScrollBar1");
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // pictureBox
            // 
            resources.ApplyResources(this.pictureBox, "pictureBox");
            this.pictureBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // textBoxMarked
            // 
            resources.ApplyResources(this.textBoxMarked, "textBoxMarked");
            this.textBoxMarked.Name = "textBoxMarked";
            this.textBoxMarked.TextChanged += new System.EventHandler(this.textBoxMarked_TextChanged);
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Name = "textBox1";
            // 
            // groupBoxEdition
            // 
            resources.ApplyResources(this.groupBoxEdition, "groupBoxEdition");
            this.groupBoxEdition.Controls.Add(this.buttonBlockRemove);
            this.groupBoxEdition.Controls.Add(this.buttonBlockDecision);
            this.groupBoxEdition.Controls.Add(this.buttonBlockBind);
            this.groupBoxEdition.Controls.Add(this.buttonBlockOperation);
            this.groupBoxEdition.Controls.Add(this.buttonBlockStop);
            this.groupBoxEdition.Controls.Add(this.buttonBlockStart);
            this.groupBoxEdition.Name = "groupBoxEdition";
            this.groupBoxEdition.TabStop = false;
            // 
            // buttonBlockRemove
            // 
            resources.ApplyResources(this.buttonBlockRemove, "buttonBlockRemove");
            this.buttonBlockRemove.Name = "buttonBlockRemove";
            this.buttonBlockRemove.UseVisualStyleBackColor = true;
            this.buttonBlockRemove.Click += new System.EventHandler(this.buttonBlockRemove_Click);
            this.buttonBlockRemove.MouseEnter += new System.EventHandler(this.buttonBlockRemove_MouseEnter);
            this.buttonBlockRemove.MouseLeave += new System.EventHandler(this.buttonBlockRemove_MouseLeave);
            // 
            // buttonBlockDecision
            // 
            resources.ApplyResources(this.buttonBlockDecision, "buttonBlockDecision");
            this.buttonBlockDecision.Name = "buttonBlockDecision";
            this.buttonBlockDecision.UseVisualStyleBackColor = true;
            this.buttonBlockDecision.Click += new System.EventHandler(this.buttonBlockDecision_Click);
            this.buttonBlockDecision.MouseEnter += new System.EventHandler(this.buttonBlockDecision_MouseEnter);
            this.buttonBlockDecision.MouseLeave += new System.EventHandler(this.buttonBlockDecision_MouseLeave);
            // 
            // buttonBlockBind
            // 
            resources.ApplyResources(this.buttonBlockBind, "buttonBlockBind");
            this.buttonBlockBind.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonBlockBind.Name = "buttonBlockBind";
            this.buttonBlockBind.UseVisualStyleBackColor = false;
            this.buttonBlockBind.Click += new System.EventHandler(this.buttonBlockBind_Click);
            this.buttonBlockBind.MouseEnter += new System.EventHandler(this.buttonBlockBind_MouseEnter);
            this.buttonBlockBind.MouseLeave += new System.EventHandler(this.buttonBlockBind_MouseLeave);
            // 
            // buttonBlockOperation
            // 
            resources.ApplyResources(this.buttonBlockOperation, "buttonBlockOperation");
            this.buttonBlockOperation.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonBlockOperation.Name = "buttonBlockOperation";
            this.buttonBlockOperation.UseVisualStyleBackColor = false;
            this.buttonBlockOperation.Click += new System.EventHandler(this.buttonBlockOperation_Click);
            this.buttonBlockOperation.MouseEnter += new System.EventHandler(this.buttonBlockOperation_MouseEnter);
            this.buttonBlockOperation.MouseLeave += new System.EventHandler(this.buttonBlockOperation_MouseLeave);
            // 
            // buttonBlockStop
            // 
            resources.ApplyResources(this.buttonBlockStop, "buttonBlockStop");
            this.buttonBlockStop.Name = "buttonBlockStop";
            this.buttonBlockStop.UseVisualStyleBackColor = true;
            this.buttonBlockStop.Click += new System.EventHandler(this.buttonBlockStop_Click);
            this.buttonBlockStop.MouseEnter += new System.EventHandler(this.buttonBlockStop_MouseEnter);
            this.buttonBlockStop.MouseLeave += new System.EventHandler(this.buttonBlockStop_MouseLeave);
            // 
            // buttonBlockStart
            // 
            resources.ApplyResources(this.buttonBlockStart, "buttonBlockStart");
            this.buttonBlockStart.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonBlockStart.Name = "buttonBlockStart";
            this.buttonBlockStart.UseVisualStyleBackColor = false;
            this.buttonBlockStart.Click += new System.EventHandler(this.buttonBlockStart_Click);
            this.buttonBlockStart.MouseEnter += new System.EventHandler(this.buttonBlockStart_MouseEnter);
            this.buttonBlockStart.MouseLeave += new System.EventHandler(this.buttonBlockStart_MouseLeave);
            // 
            // groupBoxLanguage
            // 
            resources.ApplyResources(this.groupBoxLanguage, "groupBoxLanguage");
            this.groupBoxLanguage.Controls.Add(this.buttonLanguage1);
            this.groupBoxLanguage.Controls.Add(this.buttonLanguage2);
            this.groupBoxLanguage.Name = "groupBoxLanguage";
            this.groupBoxLanguage.TabStop = false;
            // 
            // buttonLanguage1
            // 
            resources.ApplyResources(this.buttonLanguage1, "buttonLanguage1");
            this.buttonLanguage1.Name = "buttonLanguage1";
            this.buttonLanguage1.UseVisualStyleBackColor = true;
            this.buttonLanguage1.Click += new System.EventHandler(this.buttonLanguage1_Click);
            // 
            // buttonLanguage2
            // 
            resources.ApplyResources(this.buttonLanguage2, "buttonLanguage2");
            this.buttonLanguage2.Name = "buttonLanguage2";
            this.buttonLanguage2.UseVisualStyleBackColor = true;
            this.buttonLanguage2.Click += new System.EventHandler(this.buttonLanguage2_Click);
            // 
            // groupBoxFile
            // 
            resources.ApplyResources(this.groupBoxFile, "groupBoxFile");
            this.groupBoxFile.Controls.Add(this.buttonScheme3);
            this.groupBoxFile.Controls.Add(this.buttonScheme2);
            this.groupBoxFile.Controls.Add(this.buttonScheme1);
            this.groupBoxFile.Name = "groupBoxFile";
            this.groupBoxFile.TabStop = false;
            // 
            // buttonScheme3
            // 
            resources.ApplyResources(this.buttonScheme3, "buttonScheme3");
            this.buttonScheme3.Name = "buttonScheme3";
            this.buttonScheme3.UseVisualStyleBackColor = true;
            this.buttonScheme3.Click += new System.EventHandler(this.buttonScheme3_Click);
            // 
            // buttonScheme2
            // 
            resources.ApplyResources(this.buttonScheme2, "buttonScheme2");
            this.buttonScheme2.Name = "buttonScheme2";
            this.buttonScheme2.UseVisualStyleBackColor = true;
            this.buttonScheme2.Click += new System.EventHandler(this.buttonScheme2_Click);
            // 
            // buttonScheme1
            // 
            resources.ApplyResources(this.buttonScheme1, "buttonScheme1");
            this.buttonScheme1.Name = "buttonScheme1";
            this.buttonScheme1.UseVisualStyleBackColor = true;
            this.buttonScheme1.Click += new System.EventHandler(this.buttonScheme1_Click);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Name = "Form1";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.groupBoxEdition.ResumeLayout(false);
            this.groupBoxLanguage.ResumeLayout(false);
            this.groupBoxFile.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.GroupBox groupBoxFile;
        private System.Windows.Forms.GroupBox groupBoxEdition;
        private System.Windows.Forms.GroupBox groupBoxLanguage;
        private System.Windows.Forms.Button buttonLanguage1;
        private System.Windows.Forms.Button buttonLanguage2;
        private System.Windows.Forms.Button buttonScheme3;
        private System.Windows.Forms.Button buttonScheme2;
        private System.Windows.Forms.Button buttonScheme1;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar1;

        private ChooseButton buttonBlockStart;
        private ChooseButton buttonBlockStop;
        private ChooseButton buttonBlockBind;
        private ChooseButton buttonBlockOperation;
        private ChooseButton buttonBlockRemove;
        private ChooseButton buttonBlockDecision;
        private System.Windows.Forms.TextBox textBoxMarked;
        private System.Windows.Forms.TextBox textBox1;
    }
}

