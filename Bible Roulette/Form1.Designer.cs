
namespace Bible_Roulette
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.spinBtn = new System.Windows.Forms.Button();
            this.NumberPanel = new System.Windows.Forms.Panel();
            this.Verse2 = new System.Windows.Forms.RichTextBox();
            this.Verse3 = new System.Windows.Forms.RichTextBox();
            this.Chapter3 = new System.Windows.Forms.RichTextBox();
            this.Verse1 = new System.Windows.Forms.RichTextBox();
            this.Chapter1 = new System.Windows.Forms.RichTextBox();
            this.Chapter2 = new System.Windows.Forms.RichTextBox();
            this.Book2 = new System.Windows.Forms.RichTextBox();
            this.Book1 = new System.Windows.Forms.RichTextBox();
            this.NumberPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // spinBtn
            // 
            this.spinBtn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.spinBtn.Location = new System.Drawing.Point(143, 151);
            this.spinBtn.Name = "spinBtn";
            this.spinBtn.Size = new System.Drawing.Size(112, 34);
            this.spinBtn.TabIndex = 0;
            this.spinBtn.Text = "SPIN!";
            this.spinBtn.UseVisualStyleBackColor = false;
            this.spinBtn.Click += new System.EventHandler(this.spinBtn_Click);
            // 
            // NumberPanel
            // 
            this.NumberPanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.NumberPanel.Controls.Add(this.Verse2);
            this.NumberPanel.Controls.Add(this.Verse3);
            this.NumberPanel.Controls.Add(this.Chapter3);
            this.NumberPanel.Controls.Add(this.Verse1);
            this.NumberPanel.Controls.Add(this.Chapter1);
            this.NumberPanel.Controls.Add(this.Chapter2);
            this.NumberPanel.Controls.Add(this.Book2);
            this.NumberPanel.Controls.Add(this.Book1);
            this.NumberPanel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumberPanel.Location = new System.Drawing.Point(12, 12);
            this.NumberPanel.Name = "NumberPanel";
            this.NumberPanel.Size = new System.Drawing.Size(374, 117);
            this.NumberPanel.TabIndex = 1;
            // 
            // Verse2
            // 
            this.Verse2.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Verse2.Location = new System.Drawing.Point(281, 21);
            this.Verse2.Name = "Verse2";
            this.Verse2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.Verse2.Size = new System.Drawing.Size(40, 65);
            this.Verse2.TabIndex = 7;
            this.Verse2.Text = "";
            // 
            // Verse3
            // 
            this.Verse3.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Verse3.Location = new System.Drawing.Point(327, 21);
            this.Verse3.Name = "Verse3";
            this.Verse3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.Verse3.Size = new System.Drawing.Size(38, 65);
            this.Verse3.TabIndex = 6;
            this.Verse3.Text = "";
            // 
            // Chapter3
            // 
            this.Chapter3.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Chapter3.Location = new System.Drawing.Point(191, 21);
            this.Chapter3.Name = "Chapter3";
            this.Chapter3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.Chapter3.Size = new System.Drawing.Size(40, 65);
            this.Chapter3.TabIndex = 5;
            this.Chapter3.Text = "";
            // 
            // Verse1
            // 
            this.Verse1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Verse1.Location = new System.Drawing.Point(237, 21);
            this.Verse1.Name = "Verse1";
            this.Verse1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.Verse1.Size = new System.Drawing.Size(38, 65);
            this.Verse1.TabIndex = 4;
            this.Verse1.Text = "";
            // 
            // Chapter1
            // 
            this.Chapter1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Chapter1.Location = new System.Drawing.Point(101, 21);
            this.Chapter1.Name = "Chapter1";
            this.Chapter1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.Chapter1.Size = new System.Drawing.Size(40, 65);
            this.Chapter1.TabIndex = 3;
            this.Chapter1.Text = "";
            // 
            // Chapter2
            // 
            this.Chapter2.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Chapter2.Location = new System.Drawing.Point(147, 21);
            this.Chapter2.Name = "Chapter2";
            this.Chapter2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.Chapter2.Size = new System.Drawing.Size(38, 65);
            this.Chapter2.TabIndex = 2;
            this.Chapter2.Text = "";
            // 
            // Book2
            // 
            this.Book2.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Book2.Location = new System.Drawing.Point(55, 21);
            this.Book2.Name = "Book2";
            this.Book2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.Book2.Size = new System.Drawing.Size(40, 65);
            this.Book2.TabIndex = 1;
            this.Book2.Text = "";
            // 
            // Book1
            // 
            this.Book1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Book1.Location = new System.Drawing.Point(11, 21);
            this.Book1.Name = "Book1";
            this.Book1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.Book1.Size = new System.Drawing.Size(38, 65);
            this.Book1.TabIndex = 0;
            this.Book1.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(398, 197);
            this.Controls.Add(this.NumberPanel);
            this.Controls.Add(this.spinBtn);
            this.Name = "MainForm";
            this.Text = "Bible Roulette";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.NumberPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button spinBtn;
        private System.Windows.Forms.Panel NumberPanel;
        private System.Windows.Forms.RichTextBox Chapter3;
        private System.Windows.Forms.RichTextBox Verse1;
        private System.Windows.Forms.RichTextBox Chapter1;
        private System.Windows.Forms.RichTextBox Chapter2;
        private System.Windows.Forms.RichTextBox Book2;
        private System.Windows.Forms.RichTextBox Book1;
        private System.Windows.Forms.RichTextBox Verse2;
        private System.Windows.Forms.RichTextBox Verse3;
    }
}

