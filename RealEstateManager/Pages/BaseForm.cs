using System.Data;
using System.Diagnostics;
using System.Web;

namespace RealEstateManager.Pages
{
    public partial class BaseForm : Form
    {
        public static Color GlobalBackgroundColor { get; set; } = Color.FromArgb(245, 247, 250);
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

        public void SendWhatsAppMessage(string phoneNumber, string message)
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

        public static void SetGlobalBackgroundColor(string colorHex)
        {
            if (!string.IsNullOrWhiteSpace(colorHex))
            {
                GlobalBackgroundColor = ColorTranslator.FromHtml(colorHex);
            }
        }

        protected static void ApplyGroupBoxBackColorRecursive(Control parent, Color color)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is GroupBox groupBox)
                    groupBox.BackColor = color;
                ApplyGroupBoxBackColorRecursive(control, color);
            }
        }

        protected void ApplyGridStyle(DataGridView grid)
        {
            var alternatingStyle = new DataGridViewCellStyle { BackColor = Color.AliceBlue };
            var headerStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                BackColor = Color.MidnightBlue,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                SelectionBackColor = SystemColors.Highlight,
                SelectionForeColor = SystemColors.HighlightText,
                WrapMode = DataGridViewTriState.True
            };
            var defaultStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                BackColor = Color.AliceBlue,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.MidnightBlue,
                SelectionBackColor = Color.LightCyan,
                SelectionForeColor = Color.Black,
                WrapMode = DataGridViewTriState.False
            };

            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AlternatingRowsDefaultCellStyle = alternatingStyle;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.BackgroundColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle = headerStyle;
            grid.ColumnHeadersHeight = 29;
            grid.DefaultCellStyle = defaultStyle;
            grid.GridColor = SystemColors.WindowFrame;
        }

        protected void ApplyButtonStyle(Button button)
        {
            button.BackColor = Color.ForestGreen;
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button.ForeColor = Color.White;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Color.DarkGreen;
            button.FlatAppearance.MouseDownBackColor = Color.Green;
        }
    }
}