namespace RealEstateManager.Pages
{
    public class BaseForm : Form
    {
        protected Label? footerLabel;

        public BaseForm()
        {
            InitializeFooter();
        }

        private void InitializeFooter()
        {
            footerLabel = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 32,
                Text = "© 2025 RealEstateManager. All rights reserved.",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                BackColor = Color.FromArgb(30, 60, 114),
                ForeColor = Color.White
            };
            Controls.Add(footerLabel);
        }
    }
}