namespace Tuzep.UI
{
    partial class MainForm
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
            pnlMain = new Panel();
            btnImportCsv = new Button();
            btnWarehouses = new Button();
            pnlMain.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(btnImportCsv);
            pnlMain.Controls.Add(btnWarehouses);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(984, 561);
            pnlMain.TabIndex = 0;
            // 
            // btnImportCsv
            // 
            btnImportCsv.Location = new Point(494, 217);
            btnImportCsv.Name = "btnImportCsv";
            btnImportCsv.Size = new Size(157, 144);
            btnImportCsv.TabIndex = 0;
            btnImportCsv.Text = "Import .csv";
            btnImportCsv.UseVisualStyleBackColor = true;
            btnImportCsv.Click += btnImportCsv_Click;
            // 
            // btnWarehouses
            // 
            btnWarehouses.Location = new Point(232, 217);
            btnWarehouses.Name = "btnWarehouses";
            btnWarehouses.Size = new Size(157, 144);
            btnWarehouses.TabIndex = 0;
            btnWarehouses.Text = "Open warehouses";
            btnWarehouses.UseVisualStyleBackColor = true;
            btnWarehouses.Click += btnWarehouses_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(pnlMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tüzép kezelő";
            pnlMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMain;
        private Button btnWarehouses;
        private Button btnImportCsv;
    }
}