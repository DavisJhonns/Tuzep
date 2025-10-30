namespace Tuzep.UI.Components
{
    partial class MaterialPropertyUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            gbPropertyName = new GroupBox();
            SuspendLayout();
            // 
            // gbPropertyName
            // 
            gbPropertyName.Dock = DockStyle.Fill;
            gbPropertyName.Location = new Point(0, 0);
            gbPropertyName.Name = "gbPropertyName";
            gbPropertyName.Size = new Size(150, 50);
            gbPropertyName.TabIndex = 0;
            gbPropertyName.TabStop = false;
            gbPropertyName.Text = "groupBox1";
            // 
            // MaterialPropertyUC
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gbPropertyName);
            Name = "MaterialPropertyUC";
            Size = new Size(150, 50);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gbPropertyName;
    }
}
