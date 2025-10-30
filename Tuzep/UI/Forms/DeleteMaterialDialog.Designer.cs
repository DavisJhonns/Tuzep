namespace Tuzep.UI.Forms
{
    partial class RemoveMaterialDialog
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
            groupBox1 = new GroupBox();
            nudQuantity = new NumericUpDown();
            btnOk = new Button();
            btnCancel = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(nudQuantity);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(96, 50);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Quantity";
            // 
            // nudQuantity
            // 
            nudQuantity.Dock = DockStyle.Fill;
            nudQuantity.Location = new Point(3, 19);
            nudQuantity.Name = "nudQuantity";
            nudQuantity.Size = new Size(90, 23);
            nudQuantity.TabIndex = 0;
            nudQuantity.TextAlign = HorizontalAlignment.Center;
            // 
            // btnOk
            // 
            btnOk.Location = new Point(114, 10);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(93, 23);
            btnOk.TabIndex = 1;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(114, 38);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(93, 23);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // RemoveMaterialDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(214, 70);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "RemoveMaterialDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "DeleteMaterialDialog";
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nudQuantity).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private NumericUpDown nudQuantity;
        private Button btnOk;
        private Button btnCancel;
    }
}