namespace WindowsFormsApp1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.numericUpDownNewScheme = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownNewScheme2 = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.buttonNewScheme = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNewScheme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNewScheme2)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownNewScheme
            // 
            resources.ApplyResources(this.numericUpDownNewScheme, "numericUpDownNewScheme");
            this.numericUpDownNewScheme.Maximum = new decimal(new int[] {
            700,
            0,
            0,
            0});
            this.numericUpDownNewScheme.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownNewScheme.Name = "numericUpDownNewScheme";
            this.numericUpDownNewScheme.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // numericUpDownNewScheme2
            // 
            resources.ApplyResources(this.numericUpDownNewScheme2, "numericUpDownNewScheme2");
            this.numericUpDownNewScheme2.Maximum = new decimal(new int[] {
            700,
            0,
            0,
            0});
            this.numericUpDownNewScheme2.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownNewScheme2.Name = "numericUpDownNewScheme2";
            this.numericUpDownNewScheme2.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Control;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.Name = "textBox2";
            // 
            // buttonNewScheme
            // 
            resources.ApplyResources(this.buttonNewScheme, "buttonNewScheme");
            this.buttonNewScheme.Name = "buttonNewScheme";
            this.buttonNewScheme.UseVisualStyleBackColor = true;
            this.buttonNewScheme.Click += new System.EventHandler(this.buttonNewScheme_Click);
            // 
            // Form2
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonNewScheme);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.numericUpDownNewScheme2);
            this.Controls.Add(this.numericUpDownNewScheme);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNewScheme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNewScheme2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownNewScheme;
        private System.Windows.Forms.NumericUpDown numericUpDownNewScheme2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button buttonNewScheme;
    }
}