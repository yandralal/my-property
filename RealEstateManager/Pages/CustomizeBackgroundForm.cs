namespace RealEstateManager.Pages
{
    public partial class CustomizeBackgroundForm : BaseForm
    {
        private ColorDialog colorDialog;

        public CustomizeBackgroundForm()
        {
            InitializeComponent();
            colorDialog = new ColorDialog();

            // Wire up events for designer-created controls
            buttonApply.Click += ButtonApply_Click;
            buttonPickColor.Click += ButtonPickColor_Click;
        }

        private void ButtonPickColor_Click(object? sender, EventArgs e)
        {
            colorDialog.Color = previewPanel.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                previewPanel.BackColor = colorDialog.Color;
            }
        }

        private void ButtonApply_Click(object? sender, EventArgs e)
        {
            BaseForm.GlobalBackgroundColor = previewPanel.BackColor;
            foreach (Form frm in Application.OpenForms)
            {
                frm.Invalidate();
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}