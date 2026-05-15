using MODELO;
using NEGOCIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRESENTACION
{
    public partial class ClientesForm : Form
    {
        ClientesNegocio clientesNegocio = new ClientesNegocio();

        public ClientesForm()
        {
            InitializeComponent();
        }

        

        /* CONSULTAR CLIENTES */

        private void MtdConsultarClientes()
        {
            try
            {
                dgvClientes.DataSource = clientesNegocio.MtdConsultarClientes();
                dgvClientes.ClearSelection();
                dgvClientes.CurrentCell = null;

                filaActiva = null;

                MtdContarTotalRegistros();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al consultar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MtdRenombrarNombreColumna()
        {
            if (dgvClientes.Columns.Contains("IdCliente")) dgvClientes.Columns["IdCliente"].HeaderText = "ID";

            if (dgvClientes.Columns.Contains("CodigoCliente")) dgvClientes.Columns["CodigoCliente"].HeaderText = "Código";
            if (dgvClientes.Columns.Contains("NombreCliente")) dgvClientes.Columns["NombreCliente"].HeaderText = "Nombre";
            if (dgvClientes.Columns.Contains("FechaAlta")) dgvClientes.Columns["FechaAlta"].HeaderText = "Fecha Alta";
            if (dgvClientes.Columns.Contains("Direccion")) dgvClientes.Columns["Direccion"].HeaderText = "Dirección";
            if (dgvClientes.Columns.Contains("Correo")) dgvClientes.Columns["Correo"].HeaderText = "Correo";
            if (dgvClientes.Columns.Contains("Telefono")) dgvClientes.Columns["Telefono"].HeaderText = "Teléfono";
        }

        private void CambiarTitulo(string nombreColumna, string titulo)
        {
            if (dgvClientes.Columns.Contains(nombreColumna))
            {
                dgvClientes.Columns[nombreColumna].HeaderText = titulo;
            }
        }

        private void MtdContarTotalRegistros()
        {
            int totalRegistros = dgvClientes.Rows.Count;
            lblTotalRegistros.Text = "Total de registros: " + totalRegistros.ToString();
        }

        /* ESTADO CONTROLES */

        private void MtdEstadoBotonNuevo()
        {
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = false;
            btnNuevo.Enabled = true;
            btnGuardar.Enabled = false;

            txtID.Enabled = false;

            txtCodigoCliente.Enabled = true;
            txtNombre.Enabled = true;
            txtDireccion.Enabled = true;
            txtCorreo.Enabled = true;
            nudTelefono.Enabled = true;
            dtpFechaAlta.Enabled = true;

            rdbActivo.Enabled = true;
            rdbInactivo.Enabled = true;
        }

        private void MtdEstadoControles(bool editable)
        {
            btnGuardar.Enabled = editable;
            btnCancelar.Enabled = editable;

            btnNuevo.Enabled = !editable;
            btnEditar.Enabled = !editable;
            btnEliminar.Enabled = !editable;

            txtCodigoCliente.Enabled = editable;
            txtNombre.Enabled = editable;
            txtDireccion.Enabled = editable;
            txtCorreo.Enabled = editable;
            nudTelefono.Enabled = editable;
            dtpFechaAlta.Enabled = editable;

            rdbActivo.Enabled = editable;
            rdbInactivo.Enabled = editable;

            txtID.Enabled = false;
        }

        /* LIMPIAR FORMULARIO */

        private void MtdLimpiarControlesForm()
        {
            txtID.Clear();
            txtCodigoCliente.Clear();
            txtNombre.Clear();
            txtDireccion.Clear();
            txtCorreo.Clear();
            nudTelefono.Value = 0;

            dtpFechaAlta.Value = DateTime.Now;

            rdbActivo.Checked = false;
            rdbInactivo.Checked = false;

            dgvClientes.ClearSelection();
            dgvClientes.CurrentCell = null;

            foreach (DataGridViewRow row in dgvClientes.Rows)
            {
                row.Cells["Seleccionar"].Value = false;
                row.DefaultCellStyle.BackColor = Color.White;
            }
        }

        /* FILA SELECCIONADA */

        private int? filaActiva = null;

        private void MtdEstadoFilaSelecionada(bool estado)
        {
            btnEditar.Enabled = estado;
            btnEliminar.Enabled = estado;
            btnCancelar.Enabled = estado;

            btnGuardar.Enabled = false;
            btnNuevo.Enabled = !estado;
        }

        private void MtdCargarDatosFilaEnControlesForm(int filaSeleccionada)
        {
            if (filaSeleccionada >= 0 && filaSeleccionada < dgvClientes.Rows.Count)
            {
                DataGridViewRow fila = dgvClientes.Rows[filaSeleccionada];

                txtID.Text = fila.Cells["IdCliente"].Value?.ToString();
                txtCodigoCliente.Text = fila.Cells["CodigoCliente"].Value?.ToString();
                txtNombre.Text = fila.Cells["Nombre"].Value?.ToString();
                txtDireccion.Text = fila.Cells["Direccion"].Value?.ToString();
                txtCorreo.Text = fila.Cells["Correo"].Value?.ToString();
                nudTelefono.Text = fila.Cells["Telefono"].Value?.ToString();

                if (DateTime.TryParse(fila.Cells["FechaAlta"].Value?.ToString(), out DateTime fechaAlta))
                {
                    dtpFechaAlta.Value = fechaAlta;
                }
                else
                {
                    dtpFechaAlta.Value = DateTime.Now;
                }

                string estado = fila.Cells["Estado"].Value?.ToString();

                rdbActivo.Checked = estado == "ACTIVO";
                rdbInactivo.Checked = estado == "INACTIVO";

                MtdEstadoFilaSelecionada(true);
            }
        }

        private void MtdActivarFilaSeleccionada(int filaSeleccionada)
        {
            try
            {
                if (filaActiva.HasValue && filaActiva.Value < dgvClientes.Rows.Count)
                {
                    dgvClientes.Rows[filaActiva.Value].Cells["Seleccionar"].Value = false;
                    dgvClientes.Rows[filaActiva.Value].DefaultCellStyle.BackColor = Color.White;
                }

                if (filaSeleccionada >= 0 && filaSeleccionada < dgvClientes.Rows.Count)
                {
                    filaActiva = filaSeleccionada;

                    dgvClientes.Rows[filaSeleccionada].Cells["Seleccionar"].Value = true;

                    dgvClientes.Rows[filaSeleccionada].DefaultCellStyle.BackColor =
                        Color.FromArgb(220, 235, 255);

                    MtdCargarDatosFilaEnControlesForm(filaSeleccionada);
                }
            }
            catch
            {
                filaActiva = null;
            }
        }

        private void MtdDesactivaFilaSeleccionada()
        {
            if (filaActiva.HasValue)
            {
                dgvClientes.Rows[filaActiva.Value].Cells["Seleccionar"].Value = false;
                dgvClientes.Rows[filaActiva.Value].DefaultCellStyle.BackColor = Color.White;
            }

            filaActiva = null;

            MtdLimpiarControlesForm();
            MtdEstadoFilaSelecionada(false);
        }

        /* VALIDAR DATOS */

        private bool MtdValidarDatos()
        {
            if (
                string.IsNullOrWhiteSpace(txtCodigoCliente.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                string.IsNullOrWhiteSpace(nudTelefono.Text)
            )
            {
                MessageBox.Show(
                    "Por favor complete todos los campos.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            return true;
        }

        /* BOTON NUEVO */

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            MtdLimpiarControlesForm();
            MtdEstadoControles(true);

            ClientesNegocio negocio = new ClientesNegocio();
            txtCodigoCliente.Text = negocio.MtdGenerarNuevoCodigo(); 

            txtCodigoCliente.Enabled = false;
            btnGuardar.Enabled = true;
            btnNuevo.Enabled = false;
        }

        /* BOTON GUARDAR */

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (MtdValidarDatos() == false)
                return;

            try
            {
                ClientesEntidad cliente = new ClientesEntidad
                {
                    CodigoCliente = txtCodigoCliente.Text,
                    Nombre = txtNombre.Text,
                    FechaAlta = dtpFechaAlta.Value,
                    Direccion = txtDireccion.Text,
                    Correo = txtCorreo.Text,
                    Telefono = nudTelefono.Text,
                    Estado = rdbActivo.Checked ? "ACTIVO" : "INACTIVO"
                };

                string mensaje = clientesNegocio.MtdAgregarCliente(cliente);

                MessageBox.Show(
                    mensaje,
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                MtdConsultarClientes();
                MtdLimpiarControlesForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /* BOTON EDITAR */

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show(
                    "Seleccione un cliente.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (MtdValidarDatos() == false)
                return;

            try
            {
                ClientesEntidad cliente = new ClientesEntidad
                {
                    IdCliente = Convert.ToInt32(txtID.Text),
                    CodigoCliente = txtCodigoCliente.Text,
                    Nombre = txtNombre.Text,
                    FechaAlta = dtpFechaAlta.Value,
                    Direccion = txtDireccion.Text,
                    Correo = txtCorreo.Text,
                    Telefono = nudTelefono.Text,
                    Estado = rdbActivo.Checked ? "ACTIVO" : "INACTIVO"
                };

                string mensaje = clientesNegocio.MtdEditarCliente(cliente);

                MessageBox.Show(
                    mensaje,
                    "Confirmación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                MtdConsultarClientes();
                MtdLimpiarControlesForm();
                MtdDesactivaFilaSeleccionada();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /* BOTON ELIMINAR */

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtID.Text))
                    throw new Exception("Seleccione un cliente.");

                int idCliente = Convert.ToInt32(txtID.Text);

                DialogResult confirmacion = MessageBox.Show(
                    "¿Desea eliminar este cliente?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.No)
                    return;

                string mensaje = clientesNegocio.MtdEliminarCliente(idCliente);

                MessageBox.Show(
                    mensaje,
                    "Confirmación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                MtdLimpiarControlesForm();
                MtdConsultarClientes();
                MtdDesactivaFilaSeleccionada();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /* BOTON BUSCAR */

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string cliente = txtBuscarNombre.Text.Trim();

                dgvClientes.DataSource = clientesNegocio.MtdBuscarCliente(cliente);

                dgvClientes.ClearSelection();
                dgvClientes.CurrentCell = null;

                filaActiva = null;

                MtdContarTotalRegistros();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* BOTON LIMPIAR */

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscarNombre.Clear();
            MtdConsultarClientes();

            MtdLimpiarControlesForm();
            MtdEstadoFilaSelecionada(false);
            MtdContarTotalRegistros();
        }
        /* BOTON CANCELAR */
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            MtdDesactivaFilaSeleccionada();
            MtdLimpiarControlesForm();
            MtdEstadoBotonNuevo();
        }
        /* BOTON CERRAR */

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    

    private void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            dgvClientes.Columns["Seleccionar"].ReadOnly = !chkSeleccionar.Checked;

            MtdDesactivaFilaSeleccionada();
            MtdEstadoControles(true);
        }

        

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvClientes.EndEdit();
            if (e.RowIndex < 0)
                return;

            if (dgvClientes.Columns[e.ColumnIndex].Name != "Seleccionar")
                return;
            if (!chkSeleccionar.Checked)
            {
                dgvClientes.Rows[e.RowIndex].Cells["Seleccionar"].Value = false;
                return;
            }
            bool seleccionActual = Convert.ToBoolean(
                dgvClientes.Rows[e.RowIndex].Cells["Seleccionar"].Value ?? false);

            if (seleccionActual)
            
                MtdDesactivaFilaSeleccionada();
            
            else
            
                MtdActivarFilaSeleccionada(e.RowIndex);

            

            if (e.RowIndex >= 0 && dgvClientes.Rows.Count > 0)
            {
                MtdActivarFilaSeleccionada(e.RowIndex);
                MtdEstadoFilaSelecionada(true);
            }
        }

        private void ClientesForm_Load_1(object sender, EventArgs e)
        {
            MtdConsultarClientes();
            MtdRenombrarNombreColumna();
            MtdEstadoBotonNuevo();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.Rows.Count == 0)
            {
                MessageBox.Show("No hay clientes en la lista para poder exportar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog guardar = new SaveFileDialog();
            guardar.Filter = "Archivos de Excel (*.csv)|*.csv";
            guardar.Title = "Exportar Base de Clientes - Grupo Hame";
            guardar.FileName = "Reporte_Clientes_" + DateTime.Now.ToString("yyyyMMdd");

            if (guardar.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(guardar.FileName, false, System.Text.Encoding.UTF8))
                    {
                        List<string> encabezados = new List<string>();
                        foreach (DataGridViewColumn col in dgvClientes.Columns)
                        {
                            if (col.Name != "Seleccionar" && col.Visible)
                            {
                                encabezados.Add(col.HeaderText);
                            }
                        }
                        sw.WriteLine(string.Join(";", encabezados));

                        foreach (DataGridViewRow fila in dgvClientes.Rows)
                        {
                            if (!fila.IsNewRow)
                            {
                                List<string> celdas = new List<string>();
                                foreach (DataGridViewCell celda in fila.Cells)
                                {
                                    if (dgvClientes.Columns[celda.ColumnIndex].Name != "Seleccionar" && dgvClientes.Columns[celda.ColumnIndex].Visible)
                                    {
                                        string valor = celda.Value != null ? celda.Value.ToString().Replace(";", ",") : "";
                                        celdas.Add(valor);
                                    }
                                }
                                sw.WriteLine(string.Join(";", celdas));
                            }
                        }
                    }
                    MessageBox.Show("¡Lista de clientes exportada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }


}