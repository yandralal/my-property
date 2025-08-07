using RealEstateManager.Entities;
using RealEstateManager.Repositories;

namespace RealEstateManager.Pages
{
    public partial class AgentRegistrationForm : BaseForm
    {
        private Agent? _editAgent;

        public AgentRegistrationForm()
        {
            InitializeComponent();
            groupBoxAgent.Text = "Agent Details";
            txtContact.KeyPress += TxtContact_KeyPress;
        }

        // Overload for editing
        public AgentRegistrationForm(Agent agent) : this()
        {
            _editAgent = agent;
            txtName.Text = agent.Name;
            txtContact.Text = agent.Contact;
            txtAgency.Text = agent.Agency;
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string contact = txtContact.Text.Trim();

            // Validate contact number: must be exactly 10 digits and numeric
            if (contact.Length != 10 || !contact.All(char.IsDigit))
            {
                CustomMessageBox.Show("Please enter a valid 10-digit mobile number.", "Error", CustomMessageType.Error);
                txtContact.Focus();
                return;
            }

            if (_editAgent == null)
            {
                var agent = new Agent
                {
                    Name = txtName.Text,
                    Contact = txtContact.Text,
                    Agency = txtAgency.Text
                };
                AgentRepository.AddAgent(agent);
                MessageBox.Show("Agent registered successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _editAgent.Name = txtName.Text;
                _editAgent.Contact = txtContact.Text;
                _editAgent.Agency = txtAgency.Text;
                AgentRepository.UpdateAgent(_editAgent);
                MessageBox.Show("Agent updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void TxtContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control keys (e.g., backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}