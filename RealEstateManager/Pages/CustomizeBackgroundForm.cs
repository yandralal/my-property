using Microsoft.Data.SqlClient;

namespace RealEstateManager.Pages
{
    public partial class CustomizeBackgroundForm : BaseForm
    {
        private readonly ColorDialog colorDialog;
        private readonly string currentUserId; 

        public CustomizeBackgroundForm(string userId)
        {
            InitializeComponent();
            colorDialog = new ColorDialog();
            currentUserId = userId; 
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
            string colorHex = ColorTranslator.ToHtml(previewPanel.BackColor);
            string connectionString = "Server=localhost;Database=MyProperty;Trusted_Connection=True;TrustServerCertificate=True;";
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("UPDATE Login SET BackgroundColor = @Color WHERE UserName = @UserName", conn))
            {
                cmd.Parameters.AddWithValue("@Color", colorHex);
                cmd.Parameters.AddWithValue("@UserName", currentUserId); 
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            // Set global color for current session
            GlobalBackgroundColor = previewPanel.BackColor;
            SetGlobalBackgroundColor(ColorTranslator.ToHtml(previewPanel.BackColor));

            foreach (Form frm in Application.OpenForms)
            {
                if (frm is BaseForm)
                {
                    frm.Invalidate(); 
                    frm.Update();
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}