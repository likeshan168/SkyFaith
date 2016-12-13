namespace DPDTrack.Views
{
    partial class NewAgentFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewAgentFrm));
            this.newAgentView1 = new DPDTrack.Views.NewAgentView();
            this.SuspendLayout();
            // 
            // newAgentView1
            // 
            this.newAgentView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newAgentView1.Location = new System.Drawing.Point(0, 0);
            this.newAgentView1.Name = "newAgentView1";
            this.newAgentView1.Size = new System.Drawing.Size(965, 254);
            this.newAgentView1.TabIndex = 0;
            // 
            // NewAgentFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 254);
            this.Controls.Add(this.newAgentView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewAgentFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NewAgentFrm";
            this.ResumeLayout(false);

        }

        #endregion

        private NewAgentView newAgentView1;
    }
}