namespace RealEstateManager.Pages
{
    partial class SendWhatsAppMessageForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelPrompt;
        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendWhatsAppMessageForm));
            labelPrompt = new Label();
            textBoxMessage = new TextBox();
            buttonOK = new Button();
            buttonCancel = new Button();
            SuspendLayout();
            // 
            // labelPrompt
            // 
            labelPrompt.AutoSize = true;
            labelPrompt.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPrompt.Location = new Point(20, 20);
            labelPrompt.Name = "labelPrompt";
            labelPrompt.Size = new Size(342, 23);
            labelPrompt.TabIndex = 0;
            labelPrompt.Text = "Enter the message to send via WhatsApp:";
            // 
            // textBoxMessage
            // 
            textBoxMessage.Font = new Font("Segoe UI", 10F);
            textBoxMessage.Location = new Point(24, 55);
            textBoxMessage.Multiline = true;
            textBoxMessage.Name = "textBoxMessage";
            textBoxMessage.ScrollBars = ScrollBars.Vertical;
            textBoxMessage.Size = new Size(420, 120);
            textBoxMessage.TabIndex = 1;
            // 
            // buttonOK
            // 
            buttonOK.BackColor = Color.Green;
            buttonOK.DialogResult = DialogResult.OK;
            buttonOK.FlatStyle = FlatStyle.Flat;
            buttonOK.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonOK.ForeColor = Color.White;
            buttonOK.Location = new Point(240, 190);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(100, 36);
            buttonOK.TabIndex = 2;
            buttonOK.Text = "Send";
            buttonOK.UseVisualStyleBackColor = false;
            // 
            // buttonCancel
            // 
            buttonCancel.BackColor = Color.Gray;
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.FlatStyle = FlatStyle.Flat;
            buttonCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonCancel.ForeColor = Color.White;
            buttonCancel.Location = new Point(344, 190);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(100, 36);
            buttonCancel.TabIndex = 3;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = false;
            // 
            // SendWhatsAppMessageForm
            // 
            AcceptButton = buttonOK;
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(470, 250);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            Controls.Add(textBoxMessage);
            Controls.Add(labelPrompt);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimizeBox = false;
            Name = "SendWhatsAppMessageForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Send WhatsApp Message";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}