namespace MVVMDemo
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
            this.db_SFIEntitiesView1 = new MVVMDemo.Views.db_SFIEntitiesView.db_SFIEntitiesView();
            this.SuspendLayout();
            // 
            // db_SFIEntitiesView1
            // 
            this.db_SFIEntitiesView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.db_SFIEntitiesView1.Location = new System.Drawing.Point(0, 0);
            this.db_SFIEntitiesView1.Name = "db_SFIEntitiesView1";
            this.db_SFIEntitiesView1.Size = new System.Drawing.Size(804, 474);
            this.db_SFIEntitiesView1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 474);
            this.Controls.Add(this.db_SFIEntitiesView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Views.db_SFIEntitiesView.db_SFIEntitiesView db_SFIEntitiesView1;
    }
}

