using System.Threading;

namespace Hilos
{
    public partial class Form1 : Form
    {
        private Thread thread1;
        private Thread thread2;
        private bool running;
        public Form1()
        {
            InitializeComponent();
            running = true;
            this.Shown += Form1_Shown;
        }

        private void Form1_Shown(Object sender, EventArgs e)
        {
            thread1 = new Thread(new ThreadStart(MoverArribaAbajo));
            thread2 = new Thread(new ThreadStart(MoverIzquierdaDerecha));
            thread1.Start();
            thread2.Start();
        }
        private void MoverArribaAbajo()
        {
            while (running)
            {
                for (int i = 0; i < this.Height - pictureBox1.Height && running; i++)
                {
                    if (pictureBox1.InvokeRequired)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            pictureBox1.Top = i;
                        });
                    }
                    else
                    {
                        pictureBox1.Top = i;
                    }
                    Thread.Sleep(5); // Controlar la velocidad del movimiento
                }
            }
        }

        private void MoverIzquierdaDerecha()
        {
            while (running)
            {
                for(int i = 0; i < this.Width - pictureBox2.Width && running; i++)
                {
                    if (pictureBox2.InvokeRequired)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            pictureBox2.Left = i;
                        });
                    }
                    else
                    {
                        pictureBox2.Left = i;
                    }
                    Thread.Sleep(5);
                }
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            running = false;
            // Asegurarse de cerrar los hilos cuando se cierre la ventana
            if (thread1 != null && thread1.IsAlive)
            {
                thread1.Join();
            }
            if (thread2 != null && thread2.IsAlive)
            {
                thread2.Join();
            }
            base.OnFormClosing(e);
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
