using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ИМ_Лаб14
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cassa1 = new Cassa();
            cassa2 = new Cassa();
            cassa3 = new Cassa();
        }

        double time;
        int ID_client;
        Queue<int> queue = new Queue<int>();
        Cassa cassa1, cassa2, cassa3;
        
        class Cassa {
            bool free = true;
            public int ID_client;
            public double t;
            Random rnd = new Random();

            public bool toServe(Queue<int> queue, double time)
            {
                if (free) {
                    ID_client = queue.Dequeue();
                    t = time + rnd.NextDouble() * (0.1 + 0.01) - 0.01;
                    return free = false;
                }
                return free = true;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            time = 0;
            ID_client = 0;
            listBox1.Items.Clear();
            listBox1.Items.Add("Касса 1: \nсвободна");
            listBox1.Items.Add("Касса 2: \nсвободна");
            listBox1.Items.Add("Касса 3: \nсвободна");
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time += 0.03;
            queue.Enqueue(ID_client++);
            label1.Text = "Количество людей в очереди: " + queue.Count.ToString();

            if (cassa1.t < time && queue.Count > 0)
            {
                if(!cassa1.toServe(queue, time))
                {
                    listBox1.Items.Add($"Кассир 1: \nОбслуживает клиента {cassa1.ID_client}");
                }
                else
                {
                    listBox1.Items.Add("Касса 1: \nсвободна");
                }
            }
            if (cassa2.t < time && queue.Count > 0)
            {
                if(!cassa2.toServe(queue, time))
                {
                    listBox1.Items.Add($"Кассир 2: \nОбслуживает клиента {cassa2.ID_client}");
                }
                else
                {
                    listBox1.Items.Add("Касса 2: \nсвободна");
                }
            }
            if (cassa3.t < time && queue.Count > 0)
            {
                if (!cassa3.toServe(queue, time))
                {
                    listBox1.Items.Add($"Кассир 3: \nОбслуживает клиента {cassa3.ID_client}");
                }
                else
                {
                    listBox1.Items.Add("Касса 3: \nсвободна");
                }
            }
        }
    }
}
