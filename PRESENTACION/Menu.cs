using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PRESENTACION
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            panelContenedor.Dock = DockStyle.Fill;
            panelContenedor.BackColor = Color.White;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnRestaurar.Visible = false;
            btnMaximizar.Visible = true;
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void AbrirFormularioEnPanel(object formHijo)
        {
            foreach (Control c in this.panelContenedor.Controls)
            {
                if (c is Form)
                {
                    this.panelContenedor.Controls.Remove(c);
                    break;
                }
            }

            Form fh = formHijo as Form;
            if (fh != null)
            {
                fh.TopLevel = false;
                fh.FormBorderStyle = FormBorderStyle.None;
                fh.AutoScaleMode = AutoScaleMode.None;
                fh.Size = this.panelContenedor.Size;
                fh.Dock = DockStyle.Fill;
                fh.BackColor = Color.White;

                this.panelContenedor.Controls.Add(fh);
                this.panelContenedor.Tag = fh;
                fh.Show();
                lblHora.BringToFront();
                lblFecha.BringToFront();
            }
        }

        private void btnHabitaciones_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new AsignacionForm());
            
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new ClientesForm());
        }

        private void btnHuespedes_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new AsignacionForm());
        }

        private void btnServicios_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new ServiciosForm());
        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
            lblFecha.Text = DateTime.Now.ToLongDateString();
        }

        private void picApagar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Estás seguro de que quieres cerrar el programa?",
                                            "Confirmar salida",
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void lblHora_Click(object sender, EventArgs e)
        {

        }

        private void lblFecha_Click(object sender, EventArgs e)
        {

        }

        private void Menu_Load(object sender, EventArgs e)
        {
            foreach (Control ctl in this.Controls)
            {
                if (ctl is MdiClient)
                {
                    ctl.BackColor = Color.White;
                    break;
                }
            }
        }
    }
}
