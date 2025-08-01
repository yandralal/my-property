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
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonPickColor = new System.Windows.Forms.Button();
            this.previewPanel = new System.Windows.Forms.Panel();
            this.labelInstruction = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelInstruction
            // 
            this.labelInstruction.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelInstruction.Text = "Click 'Pick Color' to choose a background color.";
            this.labelInstruction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelInstruction.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.labelInstruction.Height = 40;
            // 
            // previewPanel
            // 
            this.previewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewPanel.Location = new System.Drawing.Point(0, 40);
            this.previewPanel.Name = "previewPanel";
            this.previewPanel.Size = new System.Drawing.Size(400, 130);
            this.previewPanel.TabIndex = 1;
            this.previewPanel.BackColor = RealEstateManager.Pages.BaseForm.GlobalBackgroundColor;
            // 
            // buttonPickColor
            // 
            this.buttonPickColor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPickColor.Location = new System.Drawing.Point(0, 170);
            this.buttonPickColor.Name = "buttonPickColor";
            this.buttonPickColor.Size = new System.Drawing.Size(400, 40);
            this.buttonPickColor.TabIndex = 2;
            this.buttonPickColor.Text = "Pick Color";
            this.buttonPickColor.UseVisualStyleBackColor = true;
            // 
            // buttonApply
            // 
            this.buttonApply.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonApply.Location = new System.Drawing.Point(0, 210);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(400, 40);
            this.buttonApply.TabIndex = 0;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            // 
            // CustomizeBackgroundForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 250);
            this.Controls.Add(this.previewPanel);
            this.Controls.Add(this.labelInstruction);
            this.Controls.Add(this.buttonPickColor);
            this.Controls.Add(this.buttonApply);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomizeBackgroundForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Customize Background";
            this.ResumeLayout(false);
        }

        #endregion
    }
}