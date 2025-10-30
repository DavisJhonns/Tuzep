namespace Tuzep.UI.Forms
{
    partial class WarehouseForm
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            pnlMain = new Panel();
            groupBox8 = new GroupBox();
            btnAdd = new Button();
            btnRemove = new Button();
            btnEdit = new Button();
            btnRefresh = new Button();
            btnValue = new Button();
            groupBox7 = new GroupBox();
            dgvWarehouseContent = new DataGridView();
            Icon_col = new DataGridViewImageColumn();
            Id_col = new DataGridViewTextBoxColumn();
            Name_col = new DataGridViewTextBoxColumn();
            Type_col = new DataGridViewTextBoxColumn();
            UnitPrice_col = new DataGridViewTextBoxColumn();
            Vat_col = new DataGridViewTextBoxColumn();
            GrossPrice_col = new DataGridViewTextBoxColumn();
            Quantity_col = new DataGridViewTextBoxColumn();
            Specification_col = new DataGridViewTextBoxColumn();
            groupBox1 = new GroupBox();
            btnResetFilter = new Button();
            btnFilter = new Button();
            groupBox6 = new GroupBox();
            txtFilterName = new TextBox();
            groupBox5 = new GroupBox();
            cmbMaterialType = new ComboBox();
            groupBox2 = new GroupBox();
            groupBox4 = new GroupBox();
            nudMaxPrice = new NumericUpDown();
            groupBox3 = new GroupBox();
            nudMinPrice = new NumericUpDown();
            cmbWarehouses = new ComboBox();
            lblTotalValue = new Label();
            label1 = new Label();
            pnlMain.SuspendLayout();
            groupBox8.SuspendLayout();
            groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvWarehouseContent).BeginInit();
            groupBox1.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudMaxPrice).BeginInit();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudMinPrice).BeginInit();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(groupBox8);
            pnlMain.Controls.Add(groupBox7);
            pnlMain.Controls.Add(groupBox1);
            pnlMain.Controls.Add(cmbWarehouses);
            pnlMain.Controls.Add(lblTotalValue);
            pnlMain.Controls.Add(label1);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(1186, 461);
            pnlMain.TabIndex = 0;
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(btnAdd);
            groupBox8.Controls.Add(btnRemove);
            groupBox8.Controls.Add(btnEdit);
            groupBox8.Controls.Add(btnRefresh);
            groupBox8.Controls.Add(btnValue);
            groupBox8.Location = new Point(929, 255);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(245, 170);
            groupBox8.TabIndex = 9;
            groupBox8.TabStop = false;
            groupBox8.Text = "Actions";
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(6, 22);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(227, 23);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnRemove
            // 
            btnRemove.Location = new Point(6, 80);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(227, 23);
            btnRemove.TabIndex = 6;
            btnRemove.Text = "Remove";
            btnRemove.UseVisualStyleBackColor = true;
            btnRemove.Click += btnRemove_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(6, 51);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(227, 23);
            btnEdit.TabIndex = 6;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(6, 138);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(227, 23);
            btnRefresh.TabIndex = 6;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnValue
            // 
            btnValue.Location = new Point(6, 109);
            btnValue.Name = "btnValue";
            btnValue.Size = new Size(227, 23);
            btnValue.TabIndex = 6;
            btnValue.Text = "Value";
            btnValue.UseVisualStyleBackColor = true;
            btnValue.Click += btnValue_Click;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(dgvWarehouseContent);
            groupBox7.Location = new Point(12, 35);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(911, 390);
            groupBox7.TabIndex = 8;
            groupBox7.TabStop = false;
            groupBox7.Text = "Warehouse items";
            // 
            // dgvWarehouseContent
            // 
            dgvWarehouseContent.AllowUserToAddRows = false;
            dgvWarehouseContent.AllowUserToDeleteRows = false;
            dgvWarehouseContent.AllowUserToResizeColumns = false;
            dgvWarehouseContent.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvWarehouseContent.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvWarehouseContent.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvWarehouseContent.Columns.AddRange(new DataGridViewColumn[] { Icon_col, Id_col, Name_col, Type_col, UnitPrice_col, Vat_col, GrossPrice_col, Quantity_col, Specification_col });
            dgvWarehouseContent.Dock = DockStyle.Fill;
            dgvWarehouseContent.Location = new Point(3, 19);
            dgvWarehouseContent.Name = "dgvWarehouseContent";
            dgvWarehouseContent.ReadOnly = true;
            dgvWarehouseContent.RowHeadersVisible = false;
            dgvWarehouseContent.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvWarehouseContent.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvWarehouseContent.Size = new Size(905, 368);
            dgvWarehouseContent.TabIndex = 5;
            dgvWarehouseContent.TabStop = false;
            dgvWarehouseContent.CellDoubleClick += dgvWarehouseContent_CellDoubleClick;
            // 
            // Icon_col
            // 
            Icon_col.HeaderText = "ICON";
            Icon_col.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Icon_col.Name = "Icon_col";
            Icon_col.ReadOnly = true;
            Icon_col.Width = 50;
            // 
            // Id_col
            // 
            Id_col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Id_col.HeaderText = "ID";
            Id_col.Name = "Id_col";
            Id_col.ReadOnly = true;
            Id_col.Width = 43;
            // 
            // Name_col
            // 
            Name_col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Name_col.HeaderText = "NAME";
            Name_col.Name = "Name_col";
            Name_col.ReadOnly = true;
            Name_col.Width = 66;
            // 
            // Type_col
            // 
            Type_col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Type_col.HeaderText = "TYPE";
            Type_col.Name = "Type_col";
            Type_col.ReadOnly = true;
            Type_col.Width = 58;
            // 
            // UnitPrice_col
            // 
            UnitPrice_col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            UnitPrice_col.HeaderText = "UNIT PRICE";
            UnitPrice_col.Name = "UnitPrice_col";
            UnitPrice_col.ReadOnly = true;
            UnitPrice_col.Width = 92;
            // 
            // Vat_col
            // 
            Vat_col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Vat_col.HeaderText = "VAT (%)";
            Vat_col.Name = "Vat_col";
            Vat_col.ReadOnly = true;
            Vat_col.Width = 72;
            // 
            // GrossPrice_col
            // 
            GrossPrice_col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            GrossPrice_col.HeaderText = "GROSS PRICE";
            GrossPrice_col.Name = "GrossPrice_col";
            GrossPrice_col.ReadOnly = true;
            GrossPrice_col.Width = 102;
            // 
            // Quantity_col
            // 
            Quantity_col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Quantity_col.HeaderText = "QUANTITY";
            Quantity_col.Name = "Quantity_col";
            Quantity_col.ReadOnly = true;
            Quantity_col.Width = 88;
            // 
            // Specification_col
            // 
            Specification_col.HeaderText = "SPECIFICATIONS";
            Specification_col.Name = "Specification_col";
            Specification_col.ReadOnly = true;
            Specification_col.SortMode = DataGridViewColumnSortMode.NotSortable;
            Specification_col.Width = 250;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnResetFilter);
            groupBox1.Controls.Add(btnFilter);
            groupBox1.Controls.Add(groupBox6);
            groupBox1.Controls.Add(groupBox5);
            groupBox1.Controls.Add(groupBox2);
            groupBox1.Location = new Point(928, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(246, 246);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Filter";
            // 
            // btnResetFilter
            // 
            btnResetFilter.Location = new Point(6, 206);
            btnResetFilter.Name = "btnResetFilter";
            btnResetFilter.Size = new Size(113, 23);
            btnResetFilter.TabIndex = 4;
            btnResetFilter.Text = "Reset filter";
            btnResetFilter.UseVisualStyleBackColor = true;
            btnResetFilter.Click += btnResetFilter_Click;
            // 
            // btnFilter
            // 
            btnFilter.Location = new Point(126, 206);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(113, 23);
            btnFilter.TabIndex = 4;
            btnFilter.Text = "Filter";
            btnFilter.UseVisualStyleBackColor = true;
            btnFilter.Click += btnFilter_Click;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(txtFilterName);
            groupBox6.Location = new Point(6, 22);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(233, 50);
            groupBox6.TabIndex = 3;
            groupBox6.TabStop = false;
            groupBox6.Text = "By name";
            // 
            // txtFilterName
            // 
            txtFilterName.Dock = DockStyle.Fill;
            txtFilterName.Location = new Point(3, 19);
            txtFilterName.Name = "txtFilterName";
            txtFilterName.Size = new Size(227, 23);
            txtFilterName.TabIndex = 2;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(cmbMaterialType);
            groupBox5.Location = new Point(6, 78);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(233, 46);
            groupBox5.TabIndex = 5;
            groupBox5.TabStop = false;
            groupBox5.Text = "By material type";
            // 
            // cmbMaterialType
            // 
            cmbMaterialType.Dock = DockStyle.Fill;
            cmbMaterialType.FormattingEnabled = true;
            cmbMaterialType.Location = new Point(3, 19);
            cmbMaterialType.Name = "cmbMaterialType";
            cmbMaterialType.Size = new Size(227, 23);
            cmbMaterialType.TabIndex = 1;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(groupBox4);
            groupBox2.Controls.Add(groupBox3);
            groupBox2.Location = new Point(6, 130);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(233, 70);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Unit price";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(nudMaxPrice);
            groupBox4.Location = new Point(117, 15);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(110, 50);
            groupBox4.TabIndex = 1;
            groupBox4.TabStop = false;
            groupBox4.Text = "To";
            // 
            // nudMaxPrice
            // 
            nudMaxPrice.DecimalPlaces = 1;
            nudMaxPrice.Dock = DockStyle.Fill;
            nudMaxPrice.Location = new Point(3, 19);
            nudMaxPrice.Name = "nudMaxPrice";
            nudMaxPrice.Size = new Size(104, 23);
            nudMaxPrice.TabIndex = 10;
            nudMaxPrice.TextAlign = HorizontalAlignment.Center;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(nudMinPrice);
            groupBox3.Location = new Point(6, 15);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(110, 50);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "From";
            // 
            // nudMinPrice
            // 
            nudMinPrice.DecimalPlaces = 1;
            nudMinPrice.Dock = DockStyle.Fill;
            nudMinPrice.Location = new Point(3, 19);
            nudMinPrice.Name = "nudMinPrice";
            nudMinPrice.Size = new Size(104, 23);
            nudMinPrice.TabIndex = 10;
            nudMinPrice.TextAlign = HorizontalAlignment.Center;
            // 
            // cmbWarehouses
            // 
            cmbWarehouses.FormattingEnabled = true;
            cmbWarehouses.Location = new Point(87, 6);
            cmbWarehouses.Name = "cmbWarehouses";
            cmbWarehouses.Size = new Size(138, 23);
            cmbWarehouses.TabIndex = 1;
            cmbWarehouses.SelectedIndexChanged += cmbWarehouses_SelectedIndexChanged;
            // 
            // lblTotalValue
            // 
            lblTotalValue.AutoSize = true;
            lblTotalValue.Location = new Point(12, 437);
            lblTotalValue.Name = "lblTotalValue";
            lblTotalValue.Size = new Size(119, 15);
            lblTotalValue.TabIndex = 0;
            lblTotalValue.Text = "Total value: __________";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(69, 15);
            label1.TabIndex = 0;
            label1.Text = "Warehouse:";
            // 
            // WarehouseForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1186, 461);
            Controls.Add(pnlMain);
            Name = "WarehouseForm";
            Text = "___WarehouseForm";
            Load += WarehouseForm_Load;
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            groupBox8.ResumeLayout(false);
            groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvWarehouseContent).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nudMaxPrice).EndInit();
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nudMinPrice).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMain;
        private ComboBox cmbWarehouses;
        private Label label1;
        private TextBox txtFilterName;
        private Button btnFilter;
        private ComboBox cmbMaterialType;
        private Button btnRefresh;
        private Button btnValue;
        private Button btnEdit;
        private Button btnRemove;
        private Button btnAdd;
        private DataGridView dgvWarehouseContent;
        private Label lblTotalValue;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox4;
        private GroupBox groupBox3;
        private Button btnResetFilter;
        private GroupBox groupBox6;
        private GroupBox groupBox5;
        private GroupBox groupBox8;
        private GroupBox groupBox7;
        private NumericUpDown nudMinPrice;
        private NumericUpDown nudMaxPrice;
        private DataGridViewImageColumn Icon_col;
        private DataGridViewTextBoxColumn Id_col;
        private DataGridViewTextBoxColumn Name_col;
        private DataGridViewTextBoxColumn Type_col;
        private DataGridViewTextBoxColumn UnitPrice_col;
        private DataGridViewTextBoxColumn Vat_col;
        private DataGridViewTextBoxColumn GrossPrice_col;
        private DataGridViewTextBoxColumn Quantity_col;
        private DataGridViewTextBoxColumn Specification_col;
    }
}