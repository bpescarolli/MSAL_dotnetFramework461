namespace MSAL_dotnetFramework461
{
    partial class LoginForm_Azure
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
            this.label1 = new System.Windows.Forms.Label();
            this.logginBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(351, 50);
            this.label1.TabIndex = 1;
            this.label1.Text = "MSAL dotnet fr461";
            // 
            // logginBtn
            // 
            this.logginBtn.Location = new System.Drawing.Point(95, 110);
            this.logginBtn.Name = "logginBtn";
            this.logginBtn.Size = new System.Drawing.Size(170, 38);
            this.logginBtn.TabIndex = 2;
            this.logginBtn.Text = "Sign in with Microsoft";
            this.logginBtn.UseVisualStyleBackColor = true;
            this.logginBtn.Click += new System.EventHandler(this.logginBtn_Click);
            // 
            // LoginForm_Azure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(350, 197);
            this.Controls.Add(this.logginBtn);
            this.Controls.Add(this.label1);
            this.Name = "LoginForm_Azure";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MSAL test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button logginBtn;
    }
}