using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
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

        protected void SetAllControlsMargin(Padding margin)
        {
            SetControlsMarginRecursive(this, margin);
        }

        private void SetControlsMarginRecursive(Control parent, Padding margin)
        {
            foreach (Control ctrl in parent.Controls)
            {
                ctrl.Margin = margin;
                // Recursively set for child controls (e.g., inside GroupBox, Panel, etc.)
                if (ctrl.HasChildren)
                    SetControlsMarginRecursive(ctrl, margin);
            }
        }

        // Add this method to set padding for a single TextBox
        protected void SetTextBoxPadding(TextBox textBox, int left, int right)
        {
            const int EM_SETMARGINS = 0xd3;
            int lParam = left | (right << 16);
            SendMessage(textBox.Handle, EM_SETMARGINS, 0x3, lParam);
        }
        protected void SetLabelPadding(Label leb, int left, int right)
        {
            const int EM_SETMARGINS = 0xd3;
            int lParam = left | (right << 16);
            SendMessage(leb.Handle, EM_SETMARGINS, 0x3, lParam);
        }

        // Add this method to set padding for all TextBoxes in the form
        protected void SetPaddingForControls(int left, int right)
        {
            SetPaddingForControlsRecursive(this, left, right);
        }

        private void SetPaddingForControlsRecursive(Control parent, int left, int right)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox tb)
                {
                    SetTextBoxPadding(tb, left, right);
                }
                else if (ctrl is Label lb)
                {
                    SetLabelPadding(lb, left, right);
                }
                else if (ctrl is ComboBox cb)
                {
                    SetComboBoxPadding(cb, left, right);
                }
                if (ctrl.HasChildren)
                {
                    SetPaddingForControlsRecursive(ctrl, left, right);
                }
            }
        }

        protected static void SetComboBoxPadding(ComboBox comboBox, int left, int right)
        {
            // Only works for editable ComboBox (DropDownStyle.DropDown)
            if (comboBox.DropDownStyle == ComboBoxStyle.DropDownList)
            {
                const int EM_SETMARGINS = 0xd3;
                int lParam = left | (right << 16);
                SendMessage(comboBox.Handle, EM_SETMARGINS, 0x3, lParam);
            }

            comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox.DrawItem += (s, e) =>
            {
                e.DrawBackground();
                if (e.Index >= 0)
                {
                    string text = ((ComboBox)s).Items[e.Index].ToString();
                    using (var brush = new SolidBrush(e.ForeColor))
                    {
                        // Add left padding (e.g., 10px)
                        e.Graphics.DrawString(text, e.Font, brush, e.Bounds.Left + 10, e.Bounds.Top + 2);
                    }
                }
                e.DrawFocusRectangle();
            };
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
    }
}