namespace Tuzep.UI.Forms
{
    partial class MaterialDetailForm
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
            picExportIcon = new PictureBox();
            lblMaterialName = new Label();
            btnOk = new Button();
            flpProperties = new FlowLayoutPanel();
            btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)picExportIcon).BeginInit();
            SuspendLayout();
            // 
            // picExportIcon
            // 
            picExportIcon.BorderStyle = BorderStyle.FixedSingle;
            picExportIcon.Location = new Point(172, 12);
            picExportIcon.Name = "picExportIcon";
            picExportIcon.Size = new Size(100, 100);
            picExportIcon.SizeMode = PictureBoxSizeMode.Zoom;
            picExportIcon.TabIndex = 0;
            picExportIcon.TabStop = false;
            picExportIcon.Click += picExportIcon_Click;
            // 
            // lblMaterialName
            // 
            lblMaterialName.Font = new Font("Segoe UI", 20F);
            lblMaterialName.Location = new Point(12, 12);
            lblMaterialName.Name = "lblMaterialName";
            lblMaterialName.Size = new Size(154, 100);
            lblMaterialName.TabIndex = 1;
            lblMaterialName.Text = "Ready Mix Concrete";
            lblMaterialName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.Location = new Point(147, 305);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(125, 23);
            btnOk.TabIndex = 3;
            btnOk.Text = "Ok";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // flpProperties
            // 
            flpProperties.Location = new Point(12, 118);
            flpProperties.Name = "flpProperties";
            flpProperties.Size = new Size(260, 181);
            flpProperties.TabIndex = 4;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCancel.Location = new Point(12, 305);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(125, 23);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // MaterialDetailForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 336);
            Controls.Add(flpProperties);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(lblMaterialName);
            Controls.Add(picExportIcon);
            Name = "MaterialDetailForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "MaterialDetailForm";
            Load += MaterialDetailForm_Load;
            ((System.ComponentModel.ISupportInitialize)picExportIcon).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox picExportIcon;
        private Label lblMaterialName;
        private Button btnOk;
        private FlowLayoutPanel flpProperties;
        private Button btnCancel;
    }
}