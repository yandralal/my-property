using RealEstateManager.Entities;
using RealEstateManager.Repositories;
using System.Drawing;

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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string contact = txtContact.Text.Trim();

            // Name validation: required, at least 3 chars, only letters and spaces
            if (string.IsNullOrWhiteSpace(name) || name.Length < 3 || !name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                MessageBox.Show("Please enter a valid agent name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
                return;
            }

            // Phone validation: must be exactly 10 digits and numeric, starts with 6-9
            if (contact.Length != 10 || !contact.All(char.IsDigit) || !"6789".Contains(contact[0]))
            {
                MessageBox.Show("Please enter a valid 10-digit mobile number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContact.Focus();
                return;
            }

            if (_editAgent == null)
            {
                var agent = new Agent
                {
                    Name = name,
                    Contact = contact,
                    Agency = txtAgency.Text
                };
                AgentRepository.AddAgent(agent);
                MessageBox.Show("Agent registered successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _editAgent.Name = name;
                _editAgent.Contact = contact;
                _editAgent.Agency = txtAgency.Text;
                AgentRepository.UpdateAgent(_editAgent);
                MessageBox.Show("Agent updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void TxtContact_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}