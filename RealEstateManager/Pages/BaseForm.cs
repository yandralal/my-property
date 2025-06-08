using System.Drawing.Drawing2D;

namespace RealEstateManager.Pages
{
    public partial class BaseForm : Form
    {
        protected Label? footerLabel;

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(230, 240, 255), // Light blue
                Color.FromArgb(100, 140, 220), // Deeper blue
                LinearGradientMode.ForwardDiagonal))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }


        public BaseForm()
        {
            InitializeComponent();
            this.BackColor = Color.AliceBlue;
            this.BackgroundImageLayout = ImageLayout.Stretch; // Or Tile, Center, Zoom
        }
    }
}