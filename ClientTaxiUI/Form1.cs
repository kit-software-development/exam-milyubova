using System;
using System.Windows.Forms;

namespace ClientTaxiUI {

    public partial class Form1 : Form {

        Client client = new Client();

        public Form1() {
            InitializeComponent();
            client.Start();
        }

        private void Order_Click(object sender, EventArgs e) {
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

    }
}
