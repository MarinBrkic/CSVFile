using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CSVFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonUnesi_Click(object sender, EventArgs e)
        {
            string Ime = textBoxIme.Text.Trim();
            string Prezime = textBoxPrezime.Text.Trim();
            string godinaRodenja = textBoxGodina.Text.Trim();
            string Email = textBoxEmail.Text.Trim();

            bool tocnoIme = !string.IsNullOrEmpty(Ime) && Ime.All(char.IsLetter);
            bool tocnoPrezime = !string.IsNullOrEmpty(Prezime) && Prezime.All(char.IsLetter);

            if (string.IsNullOrEmpty(Ime) || string.IsNullOrEmpty(Prezime) ||
                string.IsNullOrEmpty(godinaRodenja) || string.IsNullOrEmpty(Email))
            {
                MessageBox.Show("Molimo vas da popunite sve traženo!", "Ne ispunjeno", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
                return;
            }

            if (!tocnoIme)
            {
                MessageBox.Show("Molimo unesite ispravno ime!", "Neispravno ime",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!tocnoPrezime)
            {
                MessageBox.Show("Molimo unesite ispravno prezime!", "Neispravno prezime",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!IspravnaGodinaRodenja(godinaRodenja))
            {
                MessageBox.Show("Molimo unesite ispravnu godinu rodenja", "Neispravana godina rodenja",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!IspravanEmail(Email))
            {
                MessageBox.Show("Molimo unesite ispravnu e-mail adresu", "Neispravana e-mail adresa", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
                return;
            }


            string csvPath = "User.csv";

            using (StreamWriter sw = File.AppendText(csvPath))
            {
                sw.WriteLine($"{Ime}, {Prezime}, {godinaRodenja}, {Email}");
            }

            MessageBox.Show("Podatci su uspiješno spremljeni", "Uspijeh!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            textBoxIme.Clear();
            textBoxPrezime.Clear();
            textBoxGodina.Clear();
            textBoxEmail.Clear();
        }

        private bool IspravanEmail(string Email)
        {
            string slova = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            return Regex.IsMatch(Email, slova);
        }

        private bool IspravnaGodinaRodenja(string godinaRodenja)
        {
            if (int.TryParse(godinaRodenja, out int year))
            {
                if (year >= 1900 && year <= DateTime.Now.Year)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
