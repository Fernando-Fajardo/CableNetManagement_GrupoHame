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
    public partial class ServiciosForm : Form
    {
        ServiciosNegocio serviciosNegocio = new ServiciosNegocio();

        public ServiciosForm()
        {
            InitializeComponent();
        }

        private void ServiciosForm_Load(object sender, EventArgs e)
        {
            MtdConsultarServicios();
            MtdRenombrarNombreColumna();
            MtdEstadoBotonNuevo();
        }

        private void MtdConsultarServicios()
        {
            try
            {
                dgvServicios.DataSource = serviciosNegocio.MtdConsultar();
                dgvServicios.ClearSelection();
                dgvServicios.CurrentCell = null;
                filaActiva = null;
                lblTotalRegistros.Text = "Total de registros: " + dgvServicios.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MtdRenombrarNombreColumna()
        {
            if (dgvServicios.Columns.Contains("IdServicio")) dgvServicios.Columns["IdServicio"].HeaderText = "ID";
            if (dgvServicios.Columns.Contains("NombreServicio")) dgvServicios.Columns["NombreServicio"].HeaderText = "Nombre Servicio";
            if (dgvServicios.Columns.Contains("TipoServicio")) dgvServicios.Columns["TipoServicio"].HeaderText = "Tipo";
            if (dgvServicios.Columns.Contains("VelocidadMbps")) dgvServicios.Columns["VelocidadMbps"].HeaderText = "Velocidad";
            if (dgvServicios.Columns.Contains("PrecioMensual")) dgvServicios.Columns["PrecioMensual"].HeaderText = "Precio";
        }

        private void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            dgvServicios.Columns["Seleccionar"].ReadOnly = !chkSeleccionar.Checked;

            MtdDesactivaFilaSeleccionada();
            MtdEstadoControles(true);
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
        private void dgvServicios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvServicios.EndEdit();
            if (e.RowIndex < 0)
                return;

            if (dgvServicios.Columns[e.ColumnIndex].Name != "Seleccionar")
                return;
            if (!chkSeleccionar.Checked)
            {
                dgvServicios.Rows[e.RowIndex].Cells["Seleccionar"].Value = false;
                return;
            }
            bool seleccionActual = Convert.ToBoolean(
                dgvServicios.Rows[e.RowIndex].Cells["Seleccionar"].Value ?? false);

            if (seleccionActual)

                MtdDesactivaFilaSeleccionada();

            else

                MtdActivarFilaSeleccionada(e.RowIndex);



            if (e.RowIndex >= 0 && dgvServicios.Rows.Count > 0)
            {
                MtdActivarFilaSeleccionada(e.RowIndex);
                MtdEstadoFilaSelecionada(true);
            }
        }
        private void MtdCargarDatosFilaEnControlesForm(int filaSeleccionada)
        {
            if (filaSeleccionada >= 0 && filaSeleccionada < dgvServicios.Rows.Count)
            {
                DataGridViewRow fila = dgvServicios.Rows[filaSeleccionada];

                txtIdServicio.Text = fila.Cells["IdServicio"].Value?.ToString();
                cboxNombreServicio.Text = fila.Cells["NombreServicio"].Value?.ToString();
                cboxTipoServicio.Text = fila.Cells["TipoServicio"].Value?.ToString();
                cboxVelocidad.Text = fila.Cells["VelocidadMbps"].Value?.ToString();
                cboxTipoCable.Text = fila.Cells["TipoCable"].Value?.ToString();
                nudCantidadCanales.Text = fila.Cells["CantidadCanales"].Value?.ToString();
                txtPrecio.Text = fila.Cells["PrecioMensual"].Value?.ToString();
                              

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
                if (filaActiva.HasValue && filaActiva.Value < dgvServicios.Rows.Count)
                {
                    dgvServicios.Rows[filaActiva.Value].Cells["Seleccionar"].Value = false;
                    dgvServicios.Rows[filaActiva.Value].DefaultCellStyle.BackColor = Color.White;
                }

                if (filaSeleccionada >= 0 && filaSeleccionada < dgvServicios.Rows.Count)
                {
                    filaActiva = filaSeleccionada;

                    dgvServicios.Rows[filaSeleccionada].Cells["Seleccionar"].Value = true;
                    dgvServicios.Rows[filaSeleccionada].DefaultCellStyle.BackColor =
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
            if (filaActiva.HasValue && filaActiva < dgvServicios.Rows.Count)
            {
                dgvServicios.Rows[filaActiva.Value].DefaultCellStyle.BackColor = Color.White;
                dgvServicios.Rows[filaActiva.Value].Cells["Seleccionar"].Value = false;
            }
            filaActiva = null;
            MtdLimpiarControlesForm();
            MtdEstadoFilaSeleccionada(false);
        }

        private void MtdEstadoFilaSeleccionada(bool estado)
        {
            btnEditar.Enabled = estado;
            btnEliminar.Enabled = estado;
            btnNuevo.Enabled = !estado;
            btnCancelar.Enabled = estado;
        }
        private void MtdEstadoBotonNuevo()
        {
            MtdEstadoControles(false);
            btnNuevo.Enabled = true;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = false;
        }

        private void MtdEstadoControles(bool editable)
        {
            cboxNombreServicio.Enabled = editable;
            cboxTipoServicio.Enabled = editable;
            cboxVelocidad.Enabled = editable;
            cboxTipoCable.Enabled = editable;
            nudCantidadCanales.Enabled = editable;
            txtPrecio.Enabled = editable;
            btnGuardar.Enabled = editable;
            btnCancelar.Enabled = editable;
        }

        private void MtdLimpiarControlesForm()
        {
            txtIdServicio.Clear();
            cboxNombreServicio.SelectedIndex = -1;
            cboxTipoServicio.SelectedIndex = -1;
            cboxVelocidad.SelectedIndex = -1;
            cboxTipoCable.SelectedIndex = -1;
            nudCantidadCanales.Value = 0;
            txtPrecio.Clear();
            

            rdbActivo.Checked = false;
            rdbInactivo.Checked = false;

            dgvServicios.ClearSelection();
            dgvServicios.CurrentCell = null;

            foreach (DataGridViewRow row in dgvServicios.Rows)
            {
                row.Cells["Seleccionar"].Value = false;
                row.DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            MtdLimpiarControlesForm();
            MtdEstadoControles(true);

            btnGuardar.Enabled = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (MtdValidarDatos() == false)
                return;

            try
            {
                ServiciosEntidad servicio = new ServiciosEntidad
                {
                    NombreServicio = cboxNombreServicio.Text,
                    TipoServicio = cboxTipoServicio.Text,
                    VelocidadMbps = Convert.ToInt32(cboxVelocidad.Text),
                    TipoCable = cboxTipoCable.Text,
                    CantidadCanales = Convert.ToInt32(nudCantidadCanales.Value),
                    PrecioMensual = Convert.ToDecimal(txtPrecio.Text),
                    Estado = rdbActivo.Checked ? "ACTIVO" : "INACTIVO"
                };

                string mensaje = serviciosNegocio.MtdAgregarServicio(servicio);

                MessageBox.Show(
                    mensaje,
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                MtdConsultarServicios();
                MtdLimpiarControlesForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool MtdValidarDatos()
        {
            if (
                string.IsNullOrWhiteSpace(cboxNombreServicio.Text) ||
                string.IsNullOrWhiteSpace(cboxTipoServicio.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                (rdbActivo.Checked == false && rdbInactivo.Checked == false)

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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdServicio.Text))
            {
                MessageBox.Show(
                    "Seleccione un servicio.",
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
                ServiciosEntidad servicio = new ServiciosEntidad
                {
                    IdServicio = Convert.ToInt32(txtIdServicio.Text),
                    NombreServicio = cboxNombreServicio.Text,
                    TipoServicio = cboxTipoServicio.Text,
                    VelocidadMbps = Convert.ToInt32(cboxVelocidad.Text == "" ? "0" : cboxVelocidad.Text),
                    TipoCable = cboxTipoCable.Text == "" ? null : cboxTipoCable.Text,
                    CantidadCanales = Convert.ToInt32(nudCantidadCanales.Value),
                    PrecioMensual = Convert.ToDecimal(txtPrecio.Text),
                    Estado = rdbActivo.Checked ? "ACTIVO" : "INACTIVO"
                };

                string mensaje = serviciosNegocio.MtdEditarServicio(servicio);

                MessageBox.Show(
                    mensaje,
                    "Confirmación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                MtdConsultarServicios(); 
                MtdLimpiarControlesForm();
                MtdEstadoBotonNuevo(); 
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtIdServicio.Text))
                    throw new Exception("Seleccione un servicio.");

                int idServicio = Convert.ToInt32(txtIdServicio.Text);

                DialogResult confirmacion = MessageBox.Show(
                    "¿Desea eliminar este servicio?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.No)
                    return;

                string mensaje = serviciosNegocio.MtdEliminarServicio(idServicio);

                MessageBox.Show(
                    mensaje,
                    "Confirmación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                MtdLimpiarControlesForm();
                MtdConsultarServicios();
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            MtdDesactivaFilaSeleccionada();
            MtdLimpiarControlesForm();
            MtdEstadoBotonNuevo();
        }
        private void MtdContarTotalRegistros()
        {
            int totalRegistros = dgvServicios.Rows.Count;
            lblTotalRegistros.Text = "Total de registros: " + totalRegistros.ToString();
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscarNombre.Clear();
            MtdConsultarServicios();

            MtdLimpiarControlesForm();
            MtdEstadoFilaSelecionada(false);
            MtdContarTotalRegistros();
        }

        private void btnBuscar_Click(object sender, EventArgs e) {
            try
            {
                string servicio = txtBuscarNombre.Text.Trim();

                dgvServicios.DataSource = serviciosNegocio.MtdBuscarServicio(servicio);

                dgvServicios.ClearSelection();
                dgvServicios.CurrentCell = null;

                filaActiva = null;

                MtdContarTotalRegistros();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCerrar_Click(object sender, EventArgs e) => this.Close();

        private void cboxNombreServicio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvServicios.Rows.Count == 0)
            {
                MessageBox.Show("No hay servicios registrados en la tabla para exportar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.Filter = "Archivos de Excel (*.csv)|*.csv";
            guardar.Title = "Exportar Catálogo de Servicios - Grupo Hame";
            guardar.FileName = "Catálogo_Servicios_" + DateTime.Now.ToString("yyyyMMdd");

            if (guardar.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(guardar.FileName, false, System.Text.Encoding.UTF8))
                    {
                        List<string> encabezados = new List<string>();
                        foreach (DataGridViewColumn col in dgvServicios.Columns)
                        {
                            if (col.Name != "Seleccionar" && col.Visible)
                            {
                                encabezados.Add(col.HeaderText);
                            }
                        }
                        sw.WriteLine(string.Join(";", encabezados));

                        foreach (DataGridViewRow fila in dgvServicios.Rows)
                        {
                            if (!fila.IsNewRow)
                            {
                                List<string> celdas = new List<string>();
                                foreach (DataGridViewCell celda in fila.Cells)
                                {
                                    if (dgvServicios.Columns[celda.ColumnIndex].Name != "Seleccionar" && dgvServicios.Columns[celda.ColumnIndex].Visible)
                                    {
                                        string valor = celda.Value != null ? celda.Value.ToString().Replace(";", ",") : "";
                                        celdas.Add(valor);
                                    }
                                }
                                sw.WriteLine(string.Join(";", celdas));
                            }
                        }
                    }
                    MessageBox.Show("¡Catálogo de servicios exportado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar servicios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}