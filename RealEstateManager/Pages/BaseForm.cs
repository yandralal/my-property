using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;

namespace RealEstateManager.Pages
{
    public partial class BaseForm : Form
    {
        public static Color GlobalBackgroundColor { get; set; } = Color.White;
        protected Label? footerLabel;

        public BaseForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (var brush = new SolidBrush(GlobalBackgroundColor))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        public static void SendWhatsAppMessage(string phoneNumber, string message)
        {
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