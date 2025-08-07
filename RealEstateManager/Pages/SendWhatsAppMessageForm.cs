namespace RealEstateManager.Pages
{
    public partial class SendWhatsAppMessageForm : Form
    {
        public string MessageText => textBoxMessage.Text;

        public SendWhatsAppMessageForm()
        {
            InitializeComponent();
        }
    }
}