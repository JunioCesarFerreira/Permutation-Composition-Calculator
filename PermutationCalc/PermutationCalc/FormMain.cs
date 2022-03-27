using System;
using System.Threading;
using PermutationLibrary;
using System.Windows.Forms;

namespace PermutationCalc
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void Thread_Cycles()
        {
            long time = DateTime.Now.Ticks;
            PermutationCycles p1 = new PermutationCycles(textBoxInput1.Text);
            PermutationCycles p2 = new PermutationCycles(textBoxInput2.Text);
            PermutationCycles p = PermutationCycles.Composition(p1, p2);
            time = DateTime.Now.Ticks - time;

            Invoke(new Action(() => textBox_Results.Text += new string('*',32)
                + "\r\nResult PermutationCycles.Composition:\r\n"
                + p.PrintCycles()
                + "\r\nRuntime= " + (100 * time).ToString("N0")
                + " (ns).\r\n")
                );
        }

        private void Thread_Codomain()
        {
            long time = DateTime.Now.Ticks;
            PermutationCodomain P1 = new PermutationCodomain(textBoxInput1.Text);
            PermutationCodomain P2 = new PermutationCodomain(textBoxInput2.Text);
            PermutationCodomain P = PermutationCodomain.Composition(P1, P2, out string operation);
            time = DateTime.Now.Ticks - time;

            Invoke(new Action(() => textBox_Results.Text += new string('*', 32)
               + "\r\nResult PermutationCodomain.Composition:\r\n"
               + P.PrintCycles()
               + "\r\nRuntime= " + (100 * time).ToString("N0")
               + " (ns).\r\nPermutations two-line notation:\r\n" + operation + "\r\n")
               );
        }

        private void ButtonCalc_Click(object sender, EventArgs e)
        {
            try
            {
                if (PermutationConvert.CheckCycleNotation(textBoxInput1.Text, out string Message1))
                {
                    if (PermutationConvert.CheckCycleNotation(textBoxInput2.Text, out string Message2))
                    {
                        textBox_Results.Text = "";
                        new Thread(Thread_Cycles).Start();
                        new Thread(Thread_Codomain).Start();
                    }
                    else
                    {
                        MessageBox.Show(Message2, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show(Message1, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
