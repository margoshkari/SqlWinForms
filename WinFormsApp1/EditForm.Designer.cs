
namespace WinFormsApp1
{
    partial class EditForm
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
            this.label = new System.Windows.Forms.Label();
            this.valueTB = new System.Windows.Forms.TextBox();
            this.submitBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(88, 28);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(50, 15);
            this.label.TabIndex = 3;
            this.label.Text = "Column";
            // 
            // valueTB
            // 
            this.valueTB.Location = new System.Drawing.Point(88, 70);
            this.valueTB.Name = "valueTB";
            this.valueTB.Size = new System.Drawing.Size(100, 23);
            this.valueTB.TabIndex = 4;
            this.valueTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.valueTB_KeyDown);
            // 
            // submitBtn
            // 
            this.submitBtn.Location = new System.Drawing.Point(103, 99);
            this.submitBtn.Name = "submitBtn";
            this.submitBtn.Size = new System.Drawing.Size(75, 23);
            this.submitBtn.TabIndex = 5;
            this.submitBtn.Text = "Submit";
            this.submitBtn.UseVisualStyleBackColor = true;
            this.submitBtn.Click += new System.EventHandler(this.submitBtn_Click);
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 170);
            this.Controls.Add(this.submitBtn);
            this.Controls.Add(this.valueTB);
            this.Controls.Add(this.label);
            this.Name = "EditForm";
            this.Text = "EditForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TextBox valueTB;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button submitBtn;
    }
}