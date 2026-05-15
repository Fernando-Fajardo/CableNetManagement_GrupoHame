using NEGOCIO;
using MODELO;
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
    public partial class AsignacionForm : Form
    {
        AsignacionNegocio asignacionNegocio = new AsignacionNegocio();
        public AsignacionForm()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private void CambiarTitulo(string nombreColumna, string titulo, int orden)
        {
            if (dgvAsignacion.Columns.Contains(nombreColumna))
            {
                dgvAsignacion.Columns[nombreColumna].HeaderText = titulo;
                dgvAsignacion.Columns[nombreColumna].DisplayIndex = orden;
                dgvAsignacion.Columns[nombreColumna].Visible = true;
            }
        }
        /* ---- RENOMBRAR Y ORDENAR COLUMNAS ---- */
        private void MtdRenombrarNombreColumna()
        {
            if (dgvAsignacion.Columns.Contains("IdCliente")) dgvAsignacion.Columns["IdCliente"].Visible = false;
            if (dgvAsignacion.Columns.Contains("IdServicio")) dgvAsignacion.Columns["IdServicio"].Visible = false;

            CambiarTitulo("IdClienteServicio", "ID", 1);
            CambiarTitulo("Cliente", "Cliente", 2);
            CambiarTitulo("NombreServicio", "Servicio", 3);
            CambiarTitulo("DireccionInstalacion", "Dirección", 4);
            CambiarTitulo("FechaInstalacion", "Fecha Instalación", 5);
            CambiarTitulo("Estado", "Estado", 6);

            dgvAsignacion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void MtdContarTotalRegistros()
        {
            int totalRegistros = dgvAsignacion.Rows.Count;
            lblTotalRegistros.Text = "Total de registros: " + totalRegistros.ToString();
        }
        private void MtdConsultarAsignaciones()
        {
            try
            {
                dgvAsignacion.AutoGenerateColumns = true;

                dgvAsignacion.DataSource = asignacionNegocio.MtdConsultar();

                MtdRenombrarNombreColumna();

                dgvAsignacion.ClearSelection();
                dgvAsignacion.CurrentCell = null;
                filaActiva = null;
                MtdContarTotalRegistros();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al consultar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HuespedesForm_Load(object sender, EventArgs e)
        {
            MtdConsultarAsignaciones();
            MtdRenombrarNombreColumna();
            MtdEstadoBotonNuevo();
            MtdConfiguracionBusqueda();
        }
        private void MtdEstadoBotonNuevo()
        {
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = false;
            btnNuevo.Enabled = true;
            btnGuardar.Enabled = false;
            txtIdAsignacion.Enabled = false;
            cboxServicio.Enabled = true;
            dtpFechaInstalacion.Enabled = true;
            txtDirección.Enabled = true;
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

            cboxServicio.Enabled = editable;
            dtpFechaInstalacion.Enabled = editable;
            txtDirección.Enabled = editable;
            rdbActivo.Enabled = editable;
            rdbInactivo.Enabled = editable;

            txtIdAsignacion.Enabled = false;
        }
        // Limpia Formulario
        private void MtdLimpiarControlesForm()
        {
            txtIdAsignacion.Clear();
            cboxCliente.SelectedIndex = -1;
            cboxServicio.SelectedIndex = -1;
            dtpFechaInstalacion.Value = DateTime.Now;
            txtDirección.Clear();
            rdbActivo.Checked = false;
            rdbInactivo.Checked = false;

            dgvAsignacion.ClearSelection();
            dgvAsignacion.CurrentCell = null;

            foreach (DataGridViewRow row in dgvAsignacion.Rows)
            {
                row.Cells["Seleccionar"].Value = false;
                row.DefaultCellStyle.BackColor = Color.White;
            }
        }
        private void MtdEstadoFilaSelecionada(bool Estado)
        {
            btnEditar.Enabled = Estado;
            btnEliminar.Enabled = Estado;
            btnCancelar.Enabled = Estado;
            btnGuardar.Enabled = false;
            btnNuevo.Enabled = !Estado;
            txtIdAsignacion.Enabled = false;
            cboxServicio.Enabled = true;
            dtpFechaInstalacion.Enabled = true;
            txtDirección.Enabled = true;
            rdbActivo.Enabled = true;
            rdbInactivo.Enabled = true;
        }
        //Cargar datos de la fila selecciohnada en los controles del Form
        private void MtdCargarDatosFilaEnControlesForm(int filaSeleccionada)
        {
            if (filaSeleccionada >= 0 && filaSeleccionada < dgvAsignacion.Rows.Count)
            {
                DataGridViewRow fila = dgvAsignacion.Rows[filaSeleccionada];
                txtIdAsignacion.Text = fila.Cells["IdClienteServicio"].Value?.ToString();
                cboxCliente.Text = fila.Cells["Cliente"].Value?.ToString();
                cboxServicio.Text = fila.Cells["NombreServicio"].Value?.ToString();
                txtDirección.Text = fila.Cells["DireccionInstalacion"].Value?.ToString();
                if (DateTime.TryParse(fila.Cells["FechaInstalacion"].Value?.ToString(), out DateTime fechaInstalacion))
                {
                    dtpFechaInstalacion.Value = fechaInstalacion;
                }
                else
                {
                    dtpFechaInstalacion.Value = DateTime.Now;
                }

                string estado = fila.Cells["Estado"].Value?.ToString();

                rdbActivo.Checked = estado == "ACTIVO";
                rdbInactivo.Checked = estado == "INACTIVO";

                MtdEstadoFilaSelecionada(true);
            }
        }
        //Activar fila seleccionada
        private int? filaActiva = null;
        private void MtdActivarFilaSeleccionada(int filaSeleccionada)
        {
            try
            {
                if (filaActiva.HasValue && filaActiva.Value < dgvAsignacion.Rows.Count)
                {
                    dgvAsignacion.Rows[filaActiva.Value].Cells["Seleccionar"].Value = false;
                    dgvAsignacion.Rows[filaActiva.Value].DefaultCellStyle.BackColor = Color.White;
                }
                if (filaSeleccionada >= 0 && filaSeleccionada < dgvAsignacion.Rows.Count)
                {
                    filaActiva = filaSeleccionada;
                    dgvAsignacion.Rows[filaSeleccionada].Cells["Seleccionar"].Value = true;
                    dgvAsignacion.Rows[filaSeleccionada].DefaultCellStyle.BackColor = Color.FromArgb(220, 235, 255);

                    MtdCargarDatosFilaEnControlesForm(filaSeleccionada);
                }
            }
            catch (Exception)
            {
                filaActiva = null;
            }
        }
        //Desactivar fila seleccionada
        private void MtdDesactivaFilaSeleccionada()
        {
            if (filaActiva.HasValue)
            {
                dgvAsignacion.Rows[filaActiva.Value].Cells["Seleccionar"].Value = false;
                dgvAsignacion.Rows[filaActiva.Value].DefaultCellStyle.BackColor = Color.White;
            }

            filaActiva = null;

            MtdLimpiarControlesForm();
            MtdEstadoFilaSelecionada(false);
        }
        /* ---- VALIDAR DATOS ---- */
        private bool MtdValidarDatos()
        {
            if (
                string.IsNullOrWhiteSpace(cboxCliente.Text) ||
                string.IsNullOrWhiteSpace(cboxServicio.Text) ||
                string.IsNullOrWhiteSpace(txtDirección.Text) ||
                (!rdbActivo.Checked && !rdbInactivo.Checked)
               )
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvHuespedes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvAsignacion.EndEdit();
            if (e.RowIndex < 0)
                return;

            if (dgvAsignacion.Columns[e.ColumnIndex].Name != "Seleccionar")
                return;

            if (!chkSeleccionar.Checked)
            {
                dgvAsignacion.Rows[e.RowIndex].Cells["Seleccionar"].Value = false;
                return;
            }

            bool seleccionActual = Convert.ToBoolean(
                dgvAsignacion.Rows[e.RowIndex].Cells["Seleccionar"].Value ?? false);

            if (seleccionActual)
                MtdDesactivaFilaSeleccionada();
            else
                MtdActivarFilaSeleccionada(e.RowIndex);
            if (e.RowIndex >= 0 && dgvAsignacion.Rows.Count > 0)
            {
                MtdActivarFilaSeleccionada(e.RowIndex);
                MtdEstadoFilaSelecionada(true);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            MtdLimpiarControlesForm();
            MtdEstadoFilaSelecionada(false);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtBuscarNombre.Text.Trim();

                dgvAsignacion.DataSource = asignacionNegocio.MtdBuscarAsignacion(nombre);

                dgvAsignacion.ClearSelection();
                dgvAsignacion.CurrentCell = null;

                filaActiva = null;

                MtdContarTotalRegistros();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscarNombre.Clear();
            MtdConsultarAsignaciones();

            MtdLimpiarControlesForm();
            MtdEstadoFilaSelecionada(false);
            MtdContarTotalRegistros();
        }

        private void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            dgvAsignacion.Columns["Seleccionar"].ReadOnly = !chkSeleccionar.Checked;

            MtdDesactivaFilaSeleccionada();
            MtdEstadoControles(true);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            MtdLimpiarControlesForm();
            MtdEstadoFilaSelecionada(false);
            MtdEstadoControles(true);
            btnGuardar.Enabled = true;  
            btnCancelar.Enabled = true;  
            btnNuevo.Enabled = false;    
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        /* ---- BOTON GUARDAR ---- */
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (MtdValidarDatos() == false)
                return;

            try
            {
                AsignacionEntidad asignacion = new AsignacionEntidad
                {
                    IdCliente = Convert.ToInt32(cboxCliente.SelectedValue),
                    IdServicio = Convert.ToInt32(cboxServicio.SelectedValue),
                    DireccionInstalacion = txtDirección.Text.Trim(),
                    FechaInstalacion = dtpFechaInstalacion.Value,
                    Estado = rdbActivo.Checked ? "ACTIVO" : "INACTIVO"
                };

                string mensaje = asignacionNegocio.MtdAgregarAsignacion(asignacion);

                MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MtdLimpiarControlesForm();
                MtdConsultarAsignaciones();
                MtdEstadoFilaSelecionada(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* ---- BOTON EDITAR ---- */
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdAsignacion.Text))
            {
                MessageBox.Show("Seleccione una asignación para editar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MtdValidarDatos() == false)
                return;

            try
            {
                AsignacionEntidad asignacion = new AsignacionEntidad
                {
                    IdClienteServicio = Convert.ToInt32(txtIdAsignacion.Text),
                    IdCliente = Convert.ToInt32(cboxCliente.SelectedValue),
                    IdServicio = Convert.ToInt32(cboxServicio.SelectedValue),
                    DireccionInstalacion = txtDirección.Text.Trim(),
                    FechaInstalacion = dtpFechaInstalacion.Value,
                    Estado = rdbActivo.Checked ? "ACTIVO" : "INACTIVO"
                };

                string mensaje = asignacionNegocio.MtdEditarAsignacion(asignacion);

                MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MtdLimpiarControlesForm();
                MtdConsultarAsignaciones();
                MtdEstadoFilaSelecionada(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* ---- BOTON ELIMINAR ---- */
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtIdAsignacion.Text))
                    throw new Exception("Debe seleccionar una asignación");

                int idClienteServicio = Convert.ToInt32(txtIdAsignacion.Text);

                DialogResult confirmacion = MessageBox.Show(
                    "¿Está seguro de eliminar esta asignación?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.No)
                    return;

                string mensaje = asignacionNegocio.MtdEliminarAsignacion(idClienteServicio);

                MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                filaActiva = null;

                MtdLimpiarControlesForm();
                MtdConsultarAsignaciones();
                MtdEstadoFilaSelecionada(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MtdConfiguracionBusqueda()
        {
            /* --- PARTE DE CLIENTES ---*/
            ClientesNegocio objNegocio = new ClientesNegocio();
            DataTable dt = objNegocio.ListarParaCombo();

            cboxCliente.DataSource = dt;
            cboxCliente.DisplayMember = "Nombre";
            cboxCliente.ValueMember = "IdCliente";    
            cboxCliente.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboxCliente.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboxCliente.SelectedIndex = -1;

            /* --- PARTE DE SERVICIOS ---*/
            ServiciosNegocio objServNegocio = new ServiciosNegocio();
            DataTable dtServ = objServNegocio.ListarServiciosCombo();

            cboxServicio.DataSource = dtServ;
            cboxServicio.DisplayMember = "NombreServicio"; 
            cboxServicio.ValueMember = "IdServicio";     

            cboxServicio.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboxServicio.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboxServicio.DropDownStyle = ComboBoxStyle.DropDown;

            cboxServicio.SelectedIndex = -1;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvAsignacion.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos en la tabla para poder exportar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SaveFileDialog guardarArchivo = new SaveFileDialog();
            guardarArchivo.Filter = "Archivos de Excel (*.csv)|*.csv";
            guardarArchivo.Title = "Exportar Asignaciones - Grupo Hame";
            guardarArchivo.FileName = "Reporte_Asignaciones_" + DateTime.Now.ToString("yyyyMMdd");

            if (guardarArchivo.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(guardarArchivo.FileName, false, System.Text.Encoding.UTF8))
                    {
                        List<string> encabezados = new List<string>();
                        foreach (DataGridViewColumn columna in dgvAsignacion.Columns)
                        {
                            if (columna.Name != "Seleccionar" && columna.Visible)
                            {
                                encabezados.Add(columna.HeaderText);
                            }
                        }
                        sw.WriteLine(string.Join(";", encabezados));

                        foreach (DataGridViewRow fila in dgvAsignacion.Rows)
                        {
                            if (!fila.IsNewRow)
                            {
                                List<string> celdas = new List<string>();
                                foreach (DataGridViewCell celda in fila.Cells)
                                {
                                    if (dgvAsignacion.Columns[celda.ColumnIndex].Name != "Seleccionar" && dgvAsignacion.Columns[celda.ColumnIndex].Visible)
                                    {
                                        string valor = celda.Value != null ? celda.Value.ToString().Replace(";", ",") : "";
                                        celdas.Add(valor);
                                    }
                                }
                                sw.WriteLine(string.Join(";", celdas));
                            }
                        }
                    }
                    MessageBox.Show("¡Reporte exportado correctamente!\nYa puede abrirlo en Excel.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al guardar el archivo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        
        }
    }
}
