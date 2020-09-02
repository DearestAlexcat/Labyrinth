using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
//using System.Threading;

namespace AAA
{
    public partial class Form1 : Form
    {
        //размеры лабиринта
        static int X = 65;
        static int Y = 65;
        //размер комнаты в px
        static int size = 10;
        Form2Size lab_size;
        Labyrinth test;
       // Timer timer = new Timer();
        
        public Form1()
        {
            InitializeComponent();
           // UpdateAll();
            this.ResizeRedraw = true;
            printDocument1.DocumentName = "Labyrinth.doc";
           
            lab_size = new Form2Size();
            lab_size.Owner = this;
            
            //timer.Interval = 40;
            //timer.Tick += new EventHandler(TimerOntic);
            //timer.Start();
            

        }

        //public void TimerOntic(Object sender, EventArgs e)
        //{
        //    if (start == false)
        //        return;


        //    double p = (Labyrinth.count / (X * Y) * 100);

        //    if(p == 100)
        //    {
        //        timer.Stop();
        //       // return;
        //    }


        //    Graphics gr = this.CreateGraphics();
        //    Rectangle Progress = new Rectangle((ClientSize.Width - 250) / 2, (ClientSize.Height - 50) / 2, 250, 50);
        //    gr.DrawRectangle(new Pen(Color.FromArgb(255, 0, 0), 2), Progress);
        //    StringFormat strf = new StringFormat();
        //    strf.Alignment = StringAlignment.Center;
        //    strf.LineAlignment = StringAlignment.Center;
            
        //    gr.DrawString("Progress: " + p + " %", Font, Brushes.Blue, Progress, strf);
        //    MessageBox.Show(Labyrinth.count + "");
           
        //}

        void UpdateAll()
        {
            Graphics gr = this.CreateGraphics();
            this.AutoScrollMinSize = new Size(Y * size, X * size + menuStrip1.Size.Height);
            gr.Dispose(); 

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // MessageBox.Show("AAA");
            //Timer ta = new Timer(new ThreadStart(TimerOntic));
            if (test == null)
                return;
            Graphics gr = e.Graphics;
            test.DrawLabyrint(gr, AutoScrollPosition, menuStrip1.Size.Height);
        }

        //кнопка генерации
        private void generationMazeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lab_size.ShowDialog() == DialogResult.OK)
            {           
                Y = lab_size.sizeX;
                X = lab_size.sizeY;
                test = new Labyrinth(X, Y, size);
                UpdateAll();
                test.GenerationMaze();
                this.Invalidate();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
            test.DrawLabyrint(e.Graphics, AutoScrollPosition, menuStrip1.Size.Height);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDocument1.Print(); // Печать
            
            //printPreviewDialog1.ShowDialog();
        }
    }
}
