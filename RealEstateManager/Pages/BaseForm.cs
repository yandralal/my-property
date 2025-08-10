using System.Data;
using System.Diagnostics;
using System.Web;

namespace RealEstateManager.Pages
{
    public partial class BaseForm : Form
    {
        public static Color GlobalBackgroundColor { get; set; } = Color.FromArgb(245, 247, 250); 
        protected Label? footerLabel;
        public static string? LoggedInUserId { get; set; }

        public BaseForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (var brush = new SolidBrush(GlobalBackgroundColor))
            {
                e.Graphics.FillRectangle(brush, ClientRectangle);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyGroupBoxBackColorRecursive(this, SystemColors.Control);
        }

        public static void SendWhatsAppMessage(string phoneNumber, string message)
        {
            string cleanedNumber = new string([..phoneNumber.Where(char.IsDigit)]);
            if (string.IsNullOrWhiteSpace(cleanedNumber)) return;
            string url = $"https://wa.me/{cleanedNumber}?text={HttpUtility.UrlEncode(message)}";
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        public void SetGlobalBackgroundColor(string colorHex)
        {
            if (!string.IsNullOrWhiteSpace(colorHex))
            {
                GlobalBackgroundColor = ColorTranslator.FromHtml(colorHex);
            }
        }

        protected void ApplyGroupBoxBackColorRecursive(Control parent, Color color)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is GroupBox groupBox)
                    groupBox.BackColor = color;
                ApplyGroupBoxBackColorRecursive(control, color);
            }
        }
    }
}