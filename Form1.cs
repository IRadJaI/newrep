using System;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;

namespace ForLeague
{ 

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }
    int YoungMan = 99;
    int YoungWoman = 99;
    string NYM;
    string NYW;

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

            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(HTML);
            var data = ForLeague.ConvertHelp.FromJson(HTML);
            dynamic obj1;
            for (int k = 0; k < obj.Count; k++)
            {
                string id = data[k].Id;
                string HTML2 = webClient.DownloadString(String.Format("http://testlodtask20172.azurewebsites.net/task/" + id));
                obj1 = Newtonsoft.Json.JsonConvert.DeserializeObject(HTML2);
                string age = Convert.ToString(obj1["age"]);
                if (data[k].Sex == "male")
                {
                    if (Convert.ToInt32(age) < YoungMan)
                    {
                        YoungMan = Convert.ToInt32(age);
                        NYM = data[k].Name;
                    }
                }
                else if (data[k].Sex == "female")
                {
                    if (Convert.ToInt32(age) < YoungWoman)
                    {
                        YoungWoman = Convert.ToInt32(age);
                        NYW = data[k].Name;
                    }
                }
            }
            MessageBox.Show(Convert.ToString("Самый молодой парень: " + NYM) + ". Самая молодая девушка: " + NYW);
        }
    }
    private void Form1_Load(object sender, EventArgs e)
    {

    }
}

public class Helper
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("sex")]
        public string Sex { get; set; }
    }
    public class ConvertHelp
    {
        public static Helper[] FromJson(string json) => JsonConvert.DeserializeObject<Helper[]>(json, Settings);
        public static string ToJson(Helper[] o) => JsonConvert.SerializeObject(o, Settings);

        static JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };

    }
}
