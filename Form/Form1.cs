using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SortingAlgorithmVisualizer
{
    public partial class Form1 : Form
    {
        int[] TheArray;
        Graphics g;
        BackgroundWorker bgw = null;
        bool paused = false;
        public static bool isFinished = true;

        public Form1()
        {
            InitializeComponent();
            PopulateDropDown();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            if (TheArray == null) button_Reset_Click(null, null);

            bgw = new BackgroundWorker();
            bgw.WorkerSupportsCancellation = true;
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
            bgw.RunWorkerAsync(argument: comboBox1.SelectedItem);

            button_Pause.Enabled = true;
            //button_Reset.Enabled = false;
            isFinished = false;
            paused = false;
        }

        private void button_Pause_Click(object sender, EventArgs e)
        {
            if(!paused)
            {
                bgw.CancelAsync();
                paused = true;
                button_Pause.Text = "Resume";
                button_Start.Enabled = false;
                isFinished = true;
            }
            else
            {
                if(bgw.IsBusy)
                { 
                    return; 
                }
                int numEntries = panel1.Width;
                int maxVal = panel1.Height;
                paused = false;
                button_Pause.Text = "Pause";
                button_Start.Enabled = true;
                isFinished = false;
                for (int i = 0; i < numEntries; i++)
                {
                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), i, 0, 1, maxVal);
                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), i, maxVal - TheArray[i], 1, maxVal);
                }
                bgw.RunWorkerAsync(argument: comboBox1.SelectedItem);
            }
        }

        private void PopulateDropDown()
        {
            List<string> ClassList = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(ISortEngine).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => x.Name).ToList();
            ClassList.Sort();

            foreach(string entry in ClassList)
            {
                comboBox1.Items.Add(entry);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button_Reset_Click(object sender, EventArgs e)
        {
            if(isFinished)
            {
                g = panel1.CreateGraphics();
                int numEntries = panel1.Width;
                int maxVal = panel1.Height;
                //creare array
                TheArray = new int[numEntries];
                //Set colore panel a nero
                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), 0, 0, numEntries, maxVal);
                //Riempire l'array di numeri interi generati casualmente
                Random rand = new Random();
                for (int i = 0; i < numEntries; i++)
                {
                    TheArray[i] = rand.Next(0, maxVal);
                }
                for (int i = 0; i < numEntries; i++)
                {
                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), i, maxVal - TheArray[i], 1, maxVal);
                }

                button_Start.Enabled = true;
                button_Pause.Enabled = false;
                paused = false;
                button_Pause.Text = "Pause";
                //isFinished = false;
            }
        }

        #region BackGroundStuff

        public void bgw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            string SortEngineName = (string)e.Argument;
            Type type = Type.GetType("SortingAlgorithmVisualizer." + SortEngineName);
            var ctors = type.GetConstructors();

            try
            {
                //ISortEngine se2 = new BubbleSortEngine(TheArray, g, panel1.Height);
                ISortEngine se = (ISortEngine)ctors[0].Invoke(new object[] { TheArray, g, panel1.Height });
                while(!se.IsSorted() && (!bgw.CancellationPending))
                {
                    se.NextStep();
                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion
    }
}
