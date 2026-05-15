using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace DATOS
{
    public class ConexionDatos
    {
        private string CadenaConexion = ConfigurationManager.ConnectionStrings["CadenaConexionBDD"].ConnectionString;

        public SqlConnection MtdConexionBDD()
        {
            return new SqlConnection(CadenaConexion);
        }
    }
}
