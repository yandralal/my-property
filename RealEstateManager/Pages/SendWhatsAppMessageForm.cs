namespace RealEstateManager.Pages
{
    public partial class SendWhatsAppMessageForm : BaseForm
    {
        public string MessageText => textBoxMessage.Text;

        public SendWhatsAppMessageForm()
        {
            InitializeComponent();
        }
    }
}