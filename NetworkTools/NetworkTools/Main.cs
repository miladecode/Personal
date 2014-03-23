using System;
using System.Linq;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json.Linq;

namespace NetworkTools
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void pingButton_Click(object sender, EventArgs e)
        {
            try
            {
                int c = 3;
                IPAddress ipAddress = Dns.GetHostEntry(hostTextBox.Text).AddressList[0];
                resultsListView.Items.Clear();
                for (int i = 0; i < c; i++)
                {
                    var ping = new Ping();
                    var pingReply = ping.Send(ipAddress);
                    var result = new ListViewItem(pingReply.Address.ToString());
                    result.SubItems.Add(pingReply.Buffer.Count().ToString());
                    result.SubItems.Add(pingReply.RoundtripTime.ToString());
                    result.SubItems.Add(pingReply.Options.Ttl.ToString());
                    result.SubItems.Add(pingReply.Status.ToString());
                    resultsListView.Items.Add(result);
                    System.Threading.Thread.Sleep(100);
                }
            }
            catch (SocketException)
            {
                MessageBox.Show("Could not resolve host name.");
            }
            catch (PingException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Please enter the host name or IP address to ping.");
            }
            hostTextBox.Focus();
        }

        private void lookupButton_Click(object sender, EventArgs e)
        {
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(lookupHostTextBox.Text);
                lookupListView.Items.Clear();
                foreach (IPAddress ipAddress in hostEntry.AddressList)
                lookupListView.Items.Add(ipAddress.ToString());
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Please enter the host name or IP address to ping.");
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("The host name should be less than 127 characters");
            }
            catch (SocketException)
            {
                MessageBox.Show("Could not resolve host name.");
            }
            hostTextBox.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var URL = "http://domains.yougetsignal.com/domains.php?remoteAddress=" + textBox1.Text;
                using (var webClient = new WebClient())
                {
                    var json = webClient.DownloadString(URL);
                    JObject o = JObject.Parse(json);
                    var original = Convert.ToString(o["domainArray"]);
                    var extra = new[] { "\"", "[", "]"};
                    foreach (var rem in extra)
                    {
                        original = original.Replace(rem, "");
                    }
                    string[] parts = original.Split(',');
                    foreach (string part in parts)
                    {
                        listView1.Items.Add(part);
                    }
                   }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Please enter the host name or IP address to ping.");
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("The host name should be less than 127 characters");
            }
            catch (SocketException)
            {
                MessageBox.Show("Could not resolve host name.");
            }
            hostTextBox.Focus();
        }
    }
}
