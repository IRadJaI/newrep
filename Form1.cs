using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace ForLeague
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        string path = @"C:\WINDOWS\Temp\text.txt";
        String pattern = @"\{([^\{\}]+)\}";
        
        int ctr = 0;
        int abc = 0;
        int NumbOfYoung = 0;
        int NumbOfYoung2 = 0;
        string GOFY1; // Gender Of First Young 1
        string GOFY2;
        private void button1_Click(object sender, EventArgs e)
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                MessageBox.Show("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения");
                Form Formname = new Form1();
                Formname.Show();
                this.Hide();
            }
            else {
               
                    WebClient webClient = new WebClient();
                    webClient.Encoding = System.Text.Encoding.UTF8;

                    string HTML = webClient.DownloadString(String.Format("http://testlodtask20172.azurewebsites.net/task"));
                    System.IO.File.WriteAllText(@"C:\WINDOWS\Temp\text.txt", HTML);

                    string[] members = new string[11];
                    string[] id = new string[11];
                    string[] name = new string[11];
                    string[] sex = new string[11];
                    string[] age = new string[11];
                    string[] garbage = new string[78];

                    if (File.Exists(path))
                    {
                        string[] Text = File.ReadAllLines(path);
                        foreach (string s in Text)
                        {
                            foreach (Match m in Regex.Matches(s, pattern))
                            {
                                members[ctr] = m.Groups[1].Value;
                                ctr++;
                            }
                        }

                        for (int i = 0; i < 11; i++)
                        {
                            string[] split = members[i].Split(new Char[] { ' ', ',', '.', ':', '\t' });
                            foreach (string t in split)
                            {
                                if (t.Trim() != "")
                                    // MessageBox.Show(t);
                                    garbage[abc] = t;
                                abc++;
                            }
                        }
                    }
                    #region Здесь можно смеяться
                    id[0] = garbage[1];
                    name[0] = garbage[3] + " " + garbage[4];
                    sex[0] = garbage[6];

                    id[1] = garbage[8];
                    name[1] = garbage[10] + " " + garbage[11];
                    sex[1] = garbage[13];

                    id[2] = garbage[15];
                    name[2] = garbage[17] + " " + garbage[18];
                    sex[2] = garbage[20];

                    id[3] = garbage[22];
                    name[3] = garbage[24] + " " + garbage[25];
                    sex[3] = garbage[27];

                    id[4] = garbage[29];
                    name[4] = garbage[31] + " " + garbage[32];
                    sex[4] = garbage[34];

                    id[5] = garbage[36];
                    name[5] = garbage[38] + " " + garbage[39];
                    sex[5] = garbage[41];

                    id[6] = garbage[43];
                    name[6] = garbage[45] + " " + garbage[46];
                    sex[6] = garbage[48];

                    id[7] = garbage[50];
                    name[7] = garbage[52] + " " + garbage[53];
                    sex[7] = garbage[55];

                    id[8] = garbage[57];
                    name[8] = garbage[59] + " " + garbage[60];
                    sex[8] = garbage[62];

                    id[9] = garbage[64];
                    name[9] = garbage[66] + " " + garbage[67];
                    sex[9] = garbage[69];

                    id[10] = garbage[71];
                    name[10] = garbage[73] + " " + garbage[74];
                    sex[10] = garbage[76];
                    #endregion

                    WebClient webClient1 = new WebClient();
                    webClient1.Encoding = System.Text.Encoding.UTF8;
                    for (int u = 0; u < 11; u++)
                    {
                        string HTML2 = webClient1.DownloadString(String.Format("http://testlodtask20172.azurewebsites.net/task/" + id[u].Trim("\"".ToCharArray())));
                        age[u] = HTML2.Substring(HTML2.Length - 3);
                        age[u] = age[u].TrimEnd('}');
                    }

                    for (int j = 0; j < 11; j++)
                    {
                        try
                        {

                            if (Convert.ToInt32(age[NumbOfYoung]) > Convert.ToInt32(age[j]))
                            {
                                NumbOfYoung = j;
                            }

                        }
                        catch { }

                    }
                    GOFY1 = sex[NumbOfYoung];

                    for (int j = 0; j < 11; j++)
                    {
                        try
                        {

                            if (Convert.ToInt32(age[NumbOfYoung2]) > Convert.ToInt32(age[j]) && (sex[j] != GOFY1))
                            {
                                NumbOfYoung2 = j;
                            }
                        }
                        catch { }

                    }
                    GOFY2 = sex[NumbOfYoung2];

                    GOFY1 = GOFY1.TrimEnd('"');
                    GOFY1 = GOFY1.TrimStart('"');

                    if (GOFY1 == "male")
                        MessageBox.Show(Convert.ToString("Самый молодой парень: " + name[NumbOfYoung]) + ". Самая молодая девушка: " + name[NumbOfYoung2]);
                    else if (GOFY1 == "female")
                        MessageBox.Show(Convert.ToString("Самая молодая девушка: " + name[NumbOfYoung]) + ". Самый молодой парень: " + name[NumbOfYoung2]);
                File.Delete(path);
                    Application.Exit();

                }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
