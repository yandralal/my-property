using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Web;

namespace RealEstateManager.Pages
{
    public partial class BaseForm : Form
    {
        protected Label? footerLabel;

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (this.ClientRectangle.IsEmpty || IsInDesignMode())
                return;

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
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsInDesignMode())
            {
                this.BackColor = Color.AliceBlue;
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private static bool IsInDesignMode()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime;
        }

        public static void SendWhatsAppMessage(string phoneNumber, string message)
        {
            // Remove non-digits and ensure international format
            string cleanedNumber = new string([.. phoneNumber.Where(char.IsDigit)]);
            if (string.IsNullOrWhiteSpace(cleanedNumber)) return;
            string url = $"https://wa.me/{cleanedNumber}?text={HttpUtility.UrlEncode(message)}";
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
    }
}