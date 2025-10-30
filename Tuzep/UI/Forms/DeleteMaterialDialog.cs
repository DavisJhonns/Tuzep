namespace Tuzep.UI.Forms
{
    public partial class RemoveMaterialDialog : Form
    {
        public int QuantityToDelete => (int)nudQuantity.Value;
        public RemoveMaterialDialog()
        {
            InitializeComponent();
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
