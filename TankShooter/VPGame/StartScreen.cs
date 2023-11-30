using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPGame
{
    public partial class StartScreen : Form
    {
        Button easy;
        Button normal;
        Button hard;
        public int Difficulty { get; set; } = 1;
        public StartScreen()
        {
            easy = new Button();
            normal = new Button();
            hard = new Button();
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Button play = new Button();
            play.BackColor = Color.FromArgb(151, 151, 151);
            play.Width = 360;
            play.Height = 60;
            play.Location = new Point(this.Width / 2 - play.Width / 2, 170);
            play.ForeColor = Color.White;
            play.FlatStyle = FlatStyle.Flat;
            play.Text = "Play";
            play.Font = new Font("Bahnschrift", 22, FontStyle.Bold);
            play.FlatAppearance.BorderColor = Color.FromArgb(68, 68, 68);
            play.FlatAppearance.BorderSize = 5;
            play.MouseClick += play_MouseClick;
            this.Controls.Add(play);

            easy.BackColor = Color.FromArgb(151, 151, 151);
            easy.Width = 150;
            easy.Height = 50;
            easy.Location = new Point(this.Width / 2 - play.Width / 2 - easy.Width / 2, 250);
            easy.ForeColor = Color.White;
            easy.FlatStyle = FlatStyle.Flat;
            easy.Text = "Easy";
            easy.Font = new Font("Bahnschrift", 18, FontStyle.Bold);
            easy.FlatAppearance.BorderColor = Color.FromArgb(68, 68, 68);
            easy.FlatAppearance.BorderSize = 5;
            easy.MouseClick += easy_MouseClick;
            this.Controls.Add(easy);

            normal.BackColor = Color.FromArgb(144, 204, 114);
            normal.Width = 150;
            normal.Height = 50;
            normal.Location = new Point(this.Width / 2 - normal.Width / 2, 250);
            normal.ForeColor = Color.White;
            normal.FlatStyle = FlatStyle.Flat;
            normal.Text = "Normal";
            normal.Font = new Font("Bahnschrift", 18, FontStyle.Bold);
            normal.FlatAppearance.BorderColor = Color.FromArgb(68, 68, 68);
            normal.FlatAppearance.BorderSize = 5;
            normal.MouseClick += normal_MouseClick;
            this.Controls.Add(normal);

            hard.BackColor = Color.FromArgb(151, 151, 151);
            hard.Width = 150;
            hard.Height = 50;
            hard.Location = new Point(this.Width / 2 + play.Width / 2 - hard.Width / 2, 250);
            hard.ForeColor = Color.White;
            hard.FlatStyle = FlatStyle.Flat;
            hard.Text = "Hard";
            hard.Font = new Font("Bahnschrift", 18, FontStyle.Bold);
            hard.FlatAppearance.BorderColor = Color.FromArgb(68, 68, 68);
            hard.FlatAppearance.BorderSize = 5;
            hard.MouseClick += hard_MouseClick;
            this.Controls.Add(hard);
        }
        private void play_MouseClick(object sender, MouseEventArgs e)
        {
            this.Hide();
            Form1 form = new Form1(Difficulty);
            form.FormClosed += (s, args) => this.Close();
            form.Show();
        }
        private void easy_MouseClick(object sender, MouseEventArgs e)
        {
            Difficulty = 0;
            easy.BackColor = Color.FromArgb(144, 204, 114);
            normal.BackColor = Color.FromArgb(151, 151, 151);
            hard.BackColor = Color.FromArgb(151, 151, 151);
        }
        private void normal_MouseClick(object sender, MouseEventArgs e)
        {
            Difficulty = 1;
            normal.BackColor = Color.FromArgb(144, 204, 114);
            hard.BackColor = Color.FromArgb(151, 151, 151);
            easy.BackColor = Color.FromArgb(151, 151, 151);
        }
        private void hard_MouseClick(object sender, MouseEventArgs e)
        {
            Difficulty = 2;
            hard.BackColor = Color.FromArgb(144, 204, 114);
            normal.BackColor = Color.FromArgb(151, 151, 151);
            easy.BackColor = Color.FromArgb(151, 151, 151);
        }
    }
}
