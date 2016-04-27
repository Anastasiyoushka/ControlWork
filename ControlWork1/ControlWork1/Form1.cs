using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ControlWork1
{
    public struct MechGroup
    {

        public string name;
        public string lastName;
        public string otchestvo;
        public double teorMech;
        public double progr;
        public double sopr;
    }
    public partial class Form1 : Form
    {
        public List<MechGroup> MyListResult = new List<MechGroup>();
        FileSaver obj;
        string myTextovik = "";
        string Put = "";
        public int schetchik;
        public Form1()
        {
            InitializeComponent();
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "MechLab.chm");
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        bool FunctionChek()
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
            {

                return false;
            }
            return true;
        }
        void FunctionSaver(FileStream fs)
        {
            MechGroup f = new MechGroup();
            Encoder enc = Encoding.UTF8.GetEncoder();


            f.name = textBox1.Text;
            f.lastName = textBox2.Text;
            f.otchestvo = textBox3.Text;
            f.teorMech = Convert.ToDouble(textBox4.Text);
            f.progr = Convert.ToDouble(textBox5.Text);
            f.sopr = Convert.ToDouble(textBox6.Text);

            char[] chbuffer = new char[100];
            string stbuffer;
            byte[] bybuffer = new byte[100];


            stbuffer = f.name + "\t" + f.lastName + "\t" + f.otchestvo + "\t" + Convert.ToString(f.teorMech) + "\t" + Convert.ToString(f.progr) + "\t" + Convert.ToString(f.sopr) + "\n";
            chbuffer = stbuffer.ToCharArray();
            enc.GetBytes(chbuffer, 0, chbuffer.Length, bybuffer, 0, true);
            fs.Write(bybuffer, 0, 100);

            fs.Close();
        }

        private void заполнитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zandop.Text = "Занеcти";
            zandop.Visible = true;
            

        }



        List<MechGroup> readFileToEnd(FileStream fs)
        {
            List<MechGroup> result = new List<MechGroup>();
            byte[] bybuffer = new byte[100];
            Decoder d = Encoding.UTF8.GetDecoder();
            char[] chbuffer = new char[100];
            fs.Seek(0, SeekOrigin.Begin);
            while (fs.Read(bybuffer, 0, bybuffer.Length) != 0)
            {
                d.GetChars(bybuffer, 0, bybuffer.Length, chbuffer, 0);
                result.Add(StringToStruct(new string(chbuffer)));
                Array.Clear(bybuffer, 0, 100);
            }
            return result;

        }
        MechGroup StringToStruct(string str)
        {
            string[] ArraySplit = str.Split(new char[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            MechGroup buf = new MechGroup();
            buf.name = ArraySplit[0];
            buf.lastName = ArraySplit[1];
            buf.otchestvo = ArraySplit[2];
            buf.teorMech = Convert.ToDouble(ArraySplit[3]);
            buf.progr = Convert.ToDouble(ArraySplit[4]);
            buf.sopr = Convert.ToDouble(ArraySplit[5]);
            return buf;
        }
        private void VivodNaFormu(int k)
        {

            textBox1.Text = Convert.ToString(MyListResult[k].name);
            textBox2.Text = Convert.ToString(MyListResult[k].lastName);
            textBox3.Text = Convert.ToString(MyListResult[k].otchestvo);
            textBox4.Text = Convert.ToString(MyListResult[k].teorMech);
            textBox5.Text = Convert.ToString(MyListResult[k].progr);
            textBox6.Text = Convert.ToString(MyListResult[k].sopr);
        }


        private void просмотретьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                schetchik = 0;
                myTextovik = openFileDialog1.FileName;
                Put = myTextovik;
                FileStream fs = new FileStream(Put, FileMode.Open, FileAccess.ReadWrite);
                Encoder enc = Encoding.UTF8.GetEncoder();
                fs.Seek(0, SeekOrigin.End);
                readFileToEnd(fs);
                this.MyListResult = readFileToEnd(fs);
                if (MyListResult.Count == 1)
                {

                    VivodNaFormu(schetchik);
                    return;

                }


                zandop.Visible = true;
                VivodNaFormu(schetchik);
            }

        }


        
        private void zandop_Click(object sender, EventArgs e)
        {
            if(zandop.Text=="Занеcти")
            {
                zandop.Text = "Занеcти";
                zandop.Visible = true;
                if (!FunctionChek())
                {
                    string message = "Заполните все поля!";
                    var result = MessageBox.Show(message);
                    return;
                }
                obj = new FileSaver();
                if (obj.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(FileSaver.name) == false)
                    {
                        if (FileSaver.name == "")
                        {
                            string message = "Нету имени, нету файла ^_^";
                            var result = MessageBox.Show(message);
                            return;

                        }
                        FileStream fs = new FileStream(FileSaver.name, FileMode.OpenOrCreate, FileAccess.Write);
                        MechGroup f = new MechGroup();
                        Encoder enc = Encoding.UTF8.GetEncoder();


                        f.name = textBox1.Text;
                        f.lastName = textBox2.Text;
                        f.otchestvo = textBox3.Text;
                        f.teorMech = Convert.ToDouble(textBox4.Text);
                        f.progr = Convert.ToDouble(textBox5.Text);
                        f.sopr = Convert.ToDouble(textBox6.Text);

                        char[] chbuffer = new char[100];
                        string stbuffer;
                        byte[] bybuffer = new byte[100];


                        stbuffer = f.name + "\t" + f.lastName + "\t" + f.otchestvo + "\t" + Convert.ToString(f.teorMech) + "\t" + Convert.ToString(f.progr) + "\t" + Convert.ToString(f.sopr) + "\n";
                        chbuffer = stbuffer.ToCharArray();
                        enc.GetBytes(chbuffer, 0, chbuffer.Length, bybuffer, 0, true);
                        fs.Write(bybuffer, 0, 100);

                        fs.Close();
                    }
                    else
                        if (File.Exists(FileSaver.name) == true)
                        {

                            DialogResult rt = MessageBox.Show("Файл с таким именем уже существует. Заменить его?", "Подтвердить сообщение в виде", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (rt == DialogResult.Yes)
                            {
                                FileStream fs = new FileStream(FileSaver.name, FileMode.OpenOrCreate, FileAccess.Write);
                                FunctionSaver(fs);
                            }
                        }
                }
            }
            
            if (zandop.Text == "Занести")
            {
                zandop.Visible = true;
                if (!FunctionChek())
                {
                    string message = "Заполните все поля!";
                    var result = MessageBox.Show(message);
                    return;
                }


                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    myTextovik = openFileDialog1.FileName;
                    Put = myTextovik;
                    FileStream fs = new FileStream(Put, FileMode.Open, FileAccess.ReadWrite);
                    Encoder enc = Encoding.UTF8.GetEncoder();
                    fs.Seek(0, SeekOrigin.End);
                    FunctionSaver(fs);

                }
            }
            if (zandop.Text == "Следующий")
            {
                if (schetchik == MyListResult.Count)
                {
                    zandop.Visible = false;

                }
                VivodNaFormu(++schetchik);



                if (schetchik < MyListResult.Count - 1)
                {
                    zandop.Visible = true;
                }
                if (schetchik == MyListResult.Count - 1)
                {
                    zandop.Visible = false;
                    return;

                }
            }

        }

        private void дополнитьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            zandop.Text = "Занести";
            zandop.Visible = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.KeyChar);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.KeyChar);
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.KeyChar);
        }

        private void просмотретьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                schetchik = 0;
                myTextovik = openFileDialog1.FileName;
                Put = myTextovik;
                FileStream fs = new FileStream(Put, FileMode.Open, FileAccess.ReadWrite);
                Encoder enc = Encoding.UTF8.GetEncoder();
                fs.Seek(0, SeekOrigin.End);
                readFileToEnd(fs);
                this.MyListResult = readFileToEnd(fs);
                if (MyListResult.Count == 1)
                {

                    VivodNaFormu(schetchik);
                    return;

                }


                zandop.Visible = true;
                VivodNaFormu(schetchik);
            }
            zandop.Text = "Следующий";
            zandop.Visible = true;
        }

        private void SredniyBal_Click(object sender, EventArgs e)
        {
            double result;
            result = Convert.ToDouble(textBox4.Text) + Convert.ToDouble(textBox5.Text) + Convert.ToDouble(textBox6.Text);
            result = result / 3;
            MessageBox.Show("Средний балл студента: " + result);
        }




    }
}

