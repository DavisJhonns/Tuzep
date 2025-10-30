namespace Tuzep.UI.Forms
{
    partial class AddMaterialSelectorDialog
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
            cmbSpecifiedMaterial = new ComboBox();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            cmbWarehouse = new ComboBox();
            groupBox3 = new GroupBox();
            nudQuantity = new NumericUpDown();
            btnCancel = new Button();
            btnOk = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).BeginInit();
            SuspendLayout();
            // 
            // cmbSpecifiedMaterial
            // 
            cmbSpecifiedMaterial.Dock = DockStyle.Fill;
            cmbSpecifiedMaterial.FormattingEnabled = true;
            cmbSpecifiedMaterial.Items.AddRange(new object[] { "ReadyMixConcrete" });
            cmbSpecifiedMaterial.Location = new Point(3, 19);
            cmbSpecifiedMaterial.Name = "cmbSpecifiedMaterial";
            cmbSpecifiedMaterial.Size = new Size(129, 23);
            cmbSpecifiedMaterial.TabIndex = 0;
            cmbSpecifiedMaterial.Text = "ReadyMixConcrete";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cmbSpecifiedMaterial);
            groupBox1.Location = new Point(12, 1);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(135, 45);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Material";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(cmbWarehouse);
            groupBox2.Location = new Point(12, 52);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(135, 45);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Warehouse";
            // 
            // cmbWarehouse
            // 
            cmbWarehouse.Dock = DockStyle.Fill;
            cmbWarehouse.FormattingEnabled = true;
            cmbWarehouse.Items.AddRange(new object[] { "ReadyMixConcrete" });
            cmbWarehouse.Location = new Point(3, 19);
            cmbWarehouse.Name = "cmbWarehouse";
            cmbWarehouse.Size = new Size(129, 23);
            cmbWarehouse.TabIndex = 1;
            cmbWarehouse.Text = "Warehouse 1";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(nudQuantity);
            groupBox3.Location = new Point(12, 103);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(135, 45);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Quantity";
            // 
            // nudQuantity
            // 
            nudQuantity.Dock = DockStyle.Fill;
            nudQuantity.Location = new Point(3, 19);
            nudQuantity.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudQuantity.Name = "nudQuantity";
            nudQuantity.Size = new Size(129, 23);
            nudQuantity.TabIndex = 1;
            nudQuantity.TextAlign = HorizontalAlignment.Center;
            nudQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(12, 154);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(65, 23);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            btnOk.Location = new Point(82, 154);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(65, 23);
            btnOk.TabIndex = 3;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // AddMaterialSelectorDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(159, 186);
            Controls.Add(btnOk);
            Controls.Add(btnCancel);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximumSize = new Size(175, 225);
            MinimumSize = new Size(175, 225);
            Name = "AddMaterialSelectorDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "AddMaterialSelector";
            Load += AddMaterialSelectorDialog_Load;
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nudQuantity).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cmbSpecifiedMaterial;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ComboBox cmbWarehouse;
        private GroupBox groupBox3;
        private NumericUpDown nudQuantity;
        private Button btnCancel;
        private Button btnOk;
    }
}