public enum CustomMessageType
{
    Info,
    Warning,
    Error
}

public class CustomMessageBox : Form
{
    public CustomMessageBox(string message, string title = "Info", CustomMessageType type = CustomMessageType.Info)
    {
        Text = title;
        int minWidth = 320;
        int maxWidth = 700;
        int padding = 120;

        // Measure message width more accurately
        using var g = CreateGraphics();
        var font = new Font("Segoe UI", 10, FontStyle.Bold);
        SizeF textSize = g.MeasureString(message, font);
        int boxWidth = (int)textSize.Width + padding;
        if (boxWidth < minWidth) boxWidth = minWidth;
        if (boxWidth > maxWidth) boxWidth = maxWidth;

        Size = new Size(boxWidth, 190);
        StartPosition = FormStartPosition.CenterParent;
        BackColor = Color.AliceBlue;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;

        Icon iconImage;
        Color labelColor;
        switch (type)
        {
            case CustomMessageType.Error:
                iconImage = SystemIcons.Error;
                labelColor = Color.DarkRed;
                break;
            case CustomMessageType.Warning:
                iconImage = SystemIcons.Warning;
                labelColor = Color.DarkOrange;
                break;
            default:
                iconImage = SystemIcons.Information;
                labelColor = Color.MidnightBlue;
                break;
        }

        // Set the form's title bar icon
        this.Icon = iconImage;

        var icon = new PictureBox
        {
            Image = iconImage.ToBitmap(),
            Location = new Point(30, 35),
            Size = new Size(38, 38)
        };
        Controls.Add(icon);

        var lbl = new Label
        {
            Text = message,
            Font = font,
            ForeColor = labelColor,
            Location = new Point(80, 35),
            Size = new Size(boxWidth - 110, 40),
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleLeft
        };
        Controls.Add(lbl);

        var btn = new Button
        {
            Text = "OK",
            DialogResult = DialogResult.OK,
            BackColor = Color.Green,
            ForeColor = Color.White,
            Font = font,
            Size = new Size(120, 32),
            Location = new Point((boxWidth - 120) / 2, 90)
        };
        Controls.Add(btn);

        AcceptButton = btn;
    }

    public static void Show(string message, string title = "Info", CustomMessageType type = CustomMessageType.Info)
    {
        using (var box = new CustomMessageBox(message, title, type))
        {
            box.ShowDialog();
        }
    }
}