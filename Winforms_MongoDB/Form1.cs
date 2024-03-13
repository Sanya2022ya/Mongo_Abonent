using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Winforms_MongoDB.Classes;

namespace Winforms_MongoDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static List<ClientDto> clientDto;
        public Form fr;
        public List<ClientDto> GetClientInfo()
        {
            MongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
            var db = mongoClient.GetDatabase("AbonentDB");
            var collection = db.GetCollection<ClientDto>("Client");
            var clients = collection.Find(new BsonDocument()).ToList();
            var services = clients[0].Services.ToList();
            return clients;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            clientDto = GetClientInfo();
            bindingSource1.DataSource = clientDto;
            dataGridView1.DataSource = bindingSource1;
            dataGridView1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (fr != null)
                fr.Close();
            fr = new FormReceipt();
            fr.Show();
        }
    }
}
