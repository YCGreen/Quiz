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
using System.Net;

namespace Quiz
{
    public partial class Form1 : Form
    {
        String FileName { get; set; } = "";
        StreamReader reader { get; set; }
        int CorrectAnswer { get; set; }
        RadioButton[] answers;
        int Points = 0;

        public Form1(String[] args)
        {
            InitializeComponent();
            if (InitializeFiles(args))
            {
                CorrectAnswer = LoadQuestion();
                answers = new RadioButton[] { radioButton1, radioButton2, radioButton3, radioButton4, radioButton5 };
            }

        }

        private void GameOver()
        {
            MessageBox.Show("Game over\n Your score is: " + Points);
            this.Close();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private Boolean InitializeFiles(String[] args)
        {
            bool aok = true;
            if (args.Length > 0)
            {
                FileName = args[0];
                try
                {
                    reader = new StreamReader(FileName);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                    aok = false;
                }
            }
            else
            {
                aok = false;
            }
            return aok;
        }

        private int LoadQuestion()
        {
            pictureBox1.Visible = false;
            buttonSubmit.Enabled = true;
            int retVal = 0;
            lblQuestion.Text = reader.ReadLine();
            if (lblQuestion.Text.Length > 1)
            {
                radioButton1.Text = reader.ReadLine();
                radioButton2.Text = reader.ReadLine();
                radioButton3.Text = reader.ReadLine();
                radioButton4.Text = reader.ReadLine();
                radioButton5.Text = reader.ReadLine();
                if (!Int16.TryParse(reader.ReadLine(), out Int16 CorrectAnswer))
                {
                    MessageBox.Show("We don't know correct answer");
                    retVal = 6;
                }
                retVal = CorrectAnswer;
            }

            if(retVal == 0 )
            {
                GameOver();
            }

            return retVal;
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            checkSubmission();
            buttonSubmit.Enabled = false;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if(buttonSubmit.Enabled)
            {
                DisplayCorrect();
            }
           
            CorrectAnswer = LoadQuestion();
        }

        private void checkSubmission()
        {

            if (answers[CorrectAnswer - 1].Checked)
            {
                SetCorrect();
            }

            else
            {
                DisplayCorrect();
            }

        }

        private void SetCorrect()
        {
            Points++;
            lblPoints.Text = "Points: " + Points.ToString();
            pictureBox1.Visible = true;
        }

        private void DisplayCorrect()
        {
            MessageBox.Show("Correct answer: " + CorrectAnswer);
        }

       

       
    }
}
