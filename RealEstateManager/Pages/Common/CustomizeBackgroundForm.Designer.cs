namespace RealEstateManager.Pages
{
    partial class CustomizeBackgroundForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonPickColor;
        private System.Windows.Forms.Panel previewPanel;
        private System.Windows.Forms.Label labelInstruction;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            buttonApply = new Button();
            buttonPickColor = new Button();
            previewPanel = new Panel();
            labelInstruction = new Label();
            SuspendLayout();
            // 
            // buttonApply
            // 
            buttonApply.Dock = DockStyle.Bottom;
            buttonApply.Location = new Point(0, 210);
            buttonApply.Name = "buttonApply";
            buttonApply.Size = new Size(400, 40);
            buttonApply.TabIndex = 0;
            buttonApply.Text = "Apply";
            buttonApply.UseVisualStyleBackColor = true;
            // 
            // buttonPickColor
            // 
            buttonPickColor.Dock = DockStyle.Bottom;
            buttonPickColor.Location = new Point(0, 170);
            buttonPickColor.Name = "buttonPickColor";
            buttonPickColor.Size = new Size(400, 40);
            buttonPickColor.TabIndex = 2;
            buttonPickColor.Text = "Pick Color";
            buttonPickColor.UseVisualStyleBackColor = true;
            // 
            // previewPanel
            // 
            previewPanel.BackColor = Color.FromArgb(245, 247, 250);
            previewPanel.Dock = DockStyle.Fill;
            previewPanel.Location = new Point(0, 40);
            previewPanel.Name = "previewPanel";
            previewPanel.Size = new Size(400, 130);
            previewPanel.TabIndex = 1;
            // 
            // labelInstruction
            // 
            labelInstruction.Dock = DockStyle.Top;
            labelInstruction.Font = new Font("Segoe UI", 10F);
            labelInstruction.Location = new Point(0, 0);
            labelInstruction.Name = "labelInstruction";
            labelInstruction.Size = new Size(400, 40);
            labelInstruction.TabIndex = 2;
            labelInstruction.Text = "Click 'Pick Color' to choose a background color.";
            labelInstruction.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CustomizeBackgroundForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 250);
            Controls.Add(previewPanel);
            Controls.Add(labelInstruction);
            Controls.Add(buttonPickColor);
            Controls.Add(buttonApply);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CustomizeBackgroundForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Customize Background";
            ResumeLayout(false);
        }

        #endregion
    }
}