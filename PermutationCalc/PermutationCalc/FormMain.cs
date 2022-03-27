using System;
using System.Windows.Forms;

namespace PermutationCalc
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void ButtonCalc_Click(object sender, EventArgs e)
        {
            try
            {
                if (PermutationConvert.Check(textBoxInput1.Text, out string Message1))
                {
                    if (PermutationConvert.Check(textBoxInput2.Text, out string Message2))
                    {
                        long time = DateTime.Now.Ticks;
                        PermutationCycles p1 = new PermutationCycles(textBoxInput1.Text);
                        PermutationCycles p2 = new PermutationCycles(textBoxInput2.Text);
                        PermutationCycles p = PermutationCycles.Compose(p1, p2);
                        time = DateTime.Now.Ticks - time;

                        textBox_Results.Text = "Result PermutationCycles.Compose:\r\n"
                            + p.PrintCycles() 
                            + "\r\nRuntime= " + (100 * time).ToString("N0") 
                            + " (ns).\r\n";

                        time = DateTime.Now.Ticks;
                        PermutationCodomain P1 = new PermutationCodomain(textBoxInput1.Text);
                        PermutationCodomain P2 = new PermutationCodomain(textBoxInput2.Text);
                        PermutationCodomain P = PermutationCodomain.Compose(P1, P2, out string operation);
                        time = DateTime.Now.Ticks - time;

                        textBox_Results.Text += "Result PermutationCodomain.Compose:\r\n" 
                            + P.PrintCycles() 
                            + "\r\nRuntime= " + (100 * time).ToString("N0")
                            + " (ns).\r\nPermutations two-line notation:\r\n" + operation;
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
