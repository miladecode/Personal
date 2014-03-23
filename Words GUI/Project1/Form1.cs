using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using ClassLibrary1;

namespace Project1
{
    public partial class Form1 : Form
    {
        public int start = 0;

        public Form1()
        {
            InitializeComponent();
        }



        private void btn_run_Click(object sender, EventArgs e)
        {
            
            string mytext = richTextBox1.Text;
            Dictionary<string  , int> dic = new Dictionary<string , int>();
            int[] arrcount = new int[123];
            int num_word;
            listView1.Items.Clear();
            listView2.Items.Clear();
            
            listView4.Items.Clear();
            listView5.Items.Clear();
            WProcessor wp = new WProcessor();
            dic = wp.Dictionary(mytext);

            num_word = wp.number_word(mytext);
            int count_email = wp.Email(mytext);
            int count_site = wp.Site(mytext);
            int count_phone = wp.Phone(mytext);
            int count_words = wp.Words(mytext);
            
            
            arrcount=wp.cunt_char(mytext); 
            int max_number,max_num;
            max_number =wp.max_count_number (arrcount );
            max_num = wp.max_count_char(arrcount);

// ezafe karadn num_word


            ListViewItem lstview2 = new ListViewItem();
            lstview2.Text = "Words";
            lstview2.SubItems.Add(num_word.ToString ());
            listView2.Items.Add(lstview2);
            lstview2.BackColor = Color.YellowGreen;
            lstview2.ForeColor = Color.Red;


             
// ezafe kardan adad 
     
            for (int i = 48; i < 58; i++)
            {
                ListViewItem lstview3 = new ListViewItem();
                
                if (arrcount[i] != 0)
                {
                    char ch = (char)i;
                    lstview3.BackColor = Color.White;
                    lstview3.Text = ch.ToString();
                    lstview3.SubItems.Add(arrcount[i].ToString());
                   

                    if (arrcount[i] == max_number)
                    {
                        lstview3.BackColor = Color.YellowGreen;
                        lstview3.ForeColor  = Color.Red ;


                    }
                } 
            }
            
            
            
            
// ezafe kardan karekter
            
            for (int j = 65; j < 91; j++)
            {
                ListViewItem lstview1 = new ListViewItem();

                if (arrcount[j]!=0 || arrcount[j+32]!=0)
                {
                    char ch = (char)j;
                    int add_arrcount=arrcount[j]+arrcount [j+32];

                    lstview1.Text = ch.ToString();
                    lstview1.SubItems.Add(add_arrcount .ToString ());
                    listView1.Items.Add(lstview1);
                    if (add_arrcount == max_num)
                    {
                        lstview1.BackColor = Color.YellowGreen;
                        lstview1.ForeColor = Color.Red;
                    }
                    
                }
                
            }

            

            //moshakhas kardane tedad Email .....

            ListViewItem list4 = new ListViewItem();
            list4.Text = "Email";
            list4.SubItems.Add(count_email + "");
            listView4.Items.Add(list4);
            //....................................

            //moshakhas kardane tedad Site .....
            ListViewItem list4_site = new ListViewItem();
            list4_site.Text = "Site";
            list4_site.SubItems.Add(count_site + "");
            listView4.Items.Add(list4_site);
            //....................................

            //moshakhas kardane tedad phone .....
            ListViewItem list4_phone = new ListViewItem();
            list4_phone.Text = "Phone";
            list4_phone.SubItems.Add(count_phone + "");
            listView4.Items.Add(list4_phone);
            //....................................


            //moshakhas kardane tedad Kelemat .....
            ListViewItem list4_char = new ListViewItem();
            list4_char.Text = "Words";
            list4_char.SubItems.Add(count_words + "");
            listView4.Items.Add(list4_char);
            //....................................


            //ezafe kardane kalemat......
            foreach (string  str_key in dic.Keys)
            {
                ListViewItem list5_word = new ListViewItem();
                list5_word.Text = str_key.ToString();
                list5_word.SubItems.Add(dic[str_key].ToString());
                listView5.Items.Add(list5_word);

            }
              
           // .............................


            
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //string str = Clipboard.GetText();
            richTextBox1.Paste();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            richTextBox1.ForeColor = Color.Black;
            cunter_search.Text = "";
            WProcessor wp = new WProcessor();
            //string st_rich = richTextBox1.Text;
            //string txtserach = txt_search.Text;
            //int length = txtserach.Length;
            //int start;
            //WProcessor wp = new WProcessor();
            //start=wp.Search(txtserach, st_rich, length);


            wp.globalstr = richTextBox1.Text;
            int index = -1;
            int count = 0;
            int i = 0;

            foreach (char c in wp.globalstr)
            {

                index = wp.findWord(txt_search.Text, i);


                //richTextBox1.Select(start, txtserach.Length-1);
                //richTextBox1.SelectionColor = Color.Blue;
                //richTextBox1.Refresh();



                if (index > -1)
                {
                    richTextBox1.Select(index, txt_search.Text.Length);
                    richTextBox1.SelectionColor = Color.Red;
                    richTextBox1.Refresh();
                    start = index + txt_search.Text.Length;
                    //  richTextBox1.SelectionStart = index;
                    /// richTextBox1.SelectionLength = txt_search .Text.Length;
                    // richTextBox1.Focus();

                    i = index + txt_search.Text.Length;
                    count++;
                }
                index = -1;
            }

            if (count == 0)
                MessageBox.Show("Not Found Word");

            cunter_search.Text = cunter_search.Text + count.ToString();

            //string name =txt_search.Text;
            //string teststring = richTextBox1.Text;
            //string myExp = string.Format(@"\b{0}\b", name);
            ////OR string myExp = "\\b"+name+"\\b";      
            //string strfind = txt_search.Text ;
            //Regex r = new Regex(myExp);
            //MatchCollection result = r .Matches(teststring);    

        }



        private void btnrep_Click(object sender, EventArgs e)
        {
            WProcessor wp = new WProcessor();
            string str = richTextBox1.Text;
            richTextBox1.Text = wp.Replace(str, txt_old.Text, txt_new.Text);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mytext = richTextBox1.Text;
            WProcessor wp = new WProcessor();
            int count_email = wp.Email(mytext);
            MessageBox.Show(count_email + " ");

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }






    }
}
