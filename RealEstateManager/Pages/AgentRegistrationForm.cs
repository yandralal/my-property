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
        }

        // Overload for editing
        public AgentRegistrationForm(Agent agent) : this()
        {
            _editAgent = agent;
            txtName.Text = agent.Name;
            txtContact.Text = agent.Contact;
            txtAgency.Text = agent.Agency;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
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
            this.Close();
        }

        // Example delete logic (call this from a delete button or context menu)
        private void DeleteAgent(int agentId)
        {
            if (MessageBox.Show("Are you sure you want to delete this agent?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                AgentRepository.DeleteAgent(agentId);
                MessageBox.Show("Agent deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}