using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
namespace ClientApp
{
    public partial class Form1 : Form
    {
        public List<HTTPClient> cachedClients = new();
        public Form1()
        {
            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }


        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void UpdateKeyData()
        {

        }

        private async void SendToServerButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!cachedClients.Any(c => c.username == UserNameTextBox.Text))
                {
                    throw new Exception($"Ошибка! Незарегистрированный пользователь в поле <Пользователь>");
                }
                if (!cachedClients.Any(c => c.username == DestinationSourceTextBox.Text))
                {
                    throw new Exception($"Ошибка! Незарегистрированный пользователь в поле <Кому>");
                }
                var Sender = cachedClients.First(c => c.username == UserNameTextBox.Text);
                var Receiver = cachedClients.First(c => c.username == DestinationSourceTextBox.Text);
                var Message = EnteredMessageTextBox.Text;

                var MessageResponse = await Sender.SendMessage(Receiver.username, Message);
                if (MessageResponse != null)
                {
                    EncryptedMessageLabel.Text = Convert.ToBase64String(MessageResponse.ReEncrypted[..20]);
                }

                RSAKeyLabel.Text = Sender.ClientKeys[0][..20];
                DHKeyLabel.Text = Sender.ClientKeys[1][..20];
                AESKeyLabel.Text = Sender.ClientKeys[2][..20];

                MessageBox.Show("Отправлено!");
                DestinationSourceTextBox.Text = "";
                EnteredMessageTextBox.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show("SendToServer error : \n" + ex.Message);
            }
        }

        private async Task<bool> Register(string username)
        {
            try
            {
                var client = new HTTPClient(username);
                var connResponse = await client.Connect();
                if (connResponse.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Connection error! StatusCode:{((int)connResponse.StatusCode)}, Content:{connResponse.Content.ReadAsStringAsync()}");
                }
                var AuthResponse = await client.POSTAuthenticateUser(client.username);
                if (AuthResponse.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Authentication error! StatusCode:{((int)AuthResponse.StatusCode)}, Content:{AuthResponse.Content.ReadAsStringAsync()}");
                }
                if (AuthResponse.StatusCode == HttpStatusCode.OK && connResponse.StatusCode == HttpStatusCode.OK)
                {
                    await client.GetKeysForUser();
                    cachedClients.Add(client);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Register error : \n" + ex.Message);
                return false;
            }
        }

        private async void RegisterButton_Click(object sender, EventArgs e)
        {
            var UserName = UserNameTextBox.Text;
            var reg = await Register(UserName);

            if (reg) { MessageBox.Show("Регистрация успешна!"); }
        }

        private async void UpdateStatusButton_Click(object sender, EventArgs e)
        {
            var currentUser = cachedClients.FirstOrDefault(c => c.username == UserNameTextBox.Text);
            if (currentUser == null)
            {
                MessageBox.Show("Пользователь не выбран или не найден!");
                return;
            }

            var messages = await currentUser.GetMessagesForUser();

            DataTable table = new DataTable();
            table.Columns.Add("Отправитель", typeof(string));
            table.Columns.Add("Сообщение", typeof(string));
            table.Columns.Add("Дата", typeof(DateTime));

            foreach (var message in messages)
            {
                table.Rows.Add(message.Sender, message.Content, message.Timestamp);
            }

            dataGridView1.Invoke((MethodInvoker)delegate
            {
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = table;

                dataGridView1.Columns.Clear();

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    HeaderText = "Отправитель",
                    DataPropertyName = "Отправитель",
                    Width = 150
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    HeaderText = "Сообщение",
                    DataPropertyName = "Сообщение",
                    Width = 250
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    HeaderText = "Дата",
                    DataPropertyName = "Дата",
                    Width = 150,
                    DefaultCellStyle = new DataGridViewCellStyle()
                    {
                        Format = "dd.MM.yyyy HH:mm"
                    }
                });

                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView1.Refresh();
            });
        }
    }
}
