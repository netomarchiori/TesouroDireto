using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjetoTransferencia
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string UsuarioNome { get; set; }
        public string Senha { get; set; }
        public DateTime Nascimento { get; set; }
        public int idAgenteCustodia { get; set; }

    }
}
