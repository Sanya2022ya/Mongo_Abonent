using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Winforms_MongoDB.Classes;

namespace Winforms_MongoDB
{
    public partial class FormReceipt : Form
    {
        public FormReceipt()
        {
            InitializeComponent();
        }

        public static List<ClientDto> clientDto;
        public static List<ServicesDto> servicesDto;
        public List<ServicesDto> GetClientInfo()
        {
            MongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
            var db = mongoClient.GetDatabase("AbonentDB");
            var collection = db.GetCollection<ClientDto>("Client");
            var clients = collection.Find(new BsonDocument()).ToList();
            clientDto = clients.ToList();
            var services = clients[0].Services.ToList();
            return services;
        }
        public static void SaveToMongo()
        {
            MongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
            var db = mongoClient.GetDatabase("AbonentDB");
            var collection = db.GetCollection<BsonDocument>("Client");
            clientDto[0].Services = servicesDto;
            var filter = new BsonDocument { { "_id", clientDto[0]._id } };
            //collection.ReplaceOne(filter, clientDto[0]);
            BsonDocument doc = clientDto[0].ToBsonDocument();
            var result = collection.ReplaceOne(filter, doc);
        }

        public double GetTotalSum()
        {
            double totalSum = 0.0;
            for (int i = 0; i < servicesDto.Count; i++) 
            {
                totalSum += servicesDto[i].Cost;
            }
            return totalSum;
        }
        public double GetDebtSum()
        {
            double debtSum = 0.0;
            for (int i = 0; i < servicesDto.Count; i++)
            {
                debtSum += servicesDto[i].Debt;
            }
            return debtSum;
        }
        private void FormReceipt_Load(object sender, EventArgs e)
        {
            servicesDto= GetClientInfo();
            bindingSource1.DataSource = servicesDto;
            dataGridView1.DataSource = bindingSource1;
            dataGridView1.Refresh();
            textBoxTotalSum.Text = GetTotalSum().ToString() + " руб.";
        }

        private void buttonSavePayment_Click(object sender, EventArgs e)
        {
            var cur = (ServicesDto)bindingSource1.Current;
            double sum;
            var validate = double.TryParse(textBoxPayment.Text, out sum);
            if (TxtValidate()) 
            {
                cur.Payment = sum;
                cur.Debt = cur.Cost - sum;
                servicesDto[cur.Id] = cur;
                MessageBox.Show("Оплата прошла успешно",
                        "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dataGridView1.Refresh();
            textBoxDebt.Text = GetDebtSum().ToString() + " руб.";
            //SaveToMongo();
        }

        private bool TxtValidate()
        {
            double sum;
            var validate = double.TryParse(textBoxPayment.Text, out sum);
            if (validate)
            {
                if (sum < 30 || sum > 150000)
                {
                    MessageBox.Show("Сумма платежа должна быть не меньше 30 руб. и не превышать 150000 руб.",
                        "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                MessageBox.Show("Введенное значение не соответствует необходимому формату",
                        "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveToMongo();
        }
    }
}
