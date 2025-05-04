using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grupo9_Ape3_ValidacionesWindowsForms;

namespace Grupo9_Ape3_ValidacionesWindowsForms
{
    public partial class Portada: Form
    {
        public Portada()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        

        

        private void button2_Click(object sender, EventArgs e)
        {
            // Abre 
            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Abre 
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Abre 
            
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Ejercicio1   ejercicio1 = new Ejercicio1();
            ejercicio1.ShowDialog();
            // Muestra de nuevo Form1 cuando se cierre Form2
         
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit(); // Cierra toda la aplicación
        }
    }
}
