using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_p2.Modelo
{
    public class Mensajes
    {
        public DateTime HoraFecha { get; set; } = DateTime.Now;
        public string Text { set; get; }
        public bool EsMio { set; get; }
        public bool Visto { set; get; }
        public bool Esmio { get; internal set; }
        public Mensajes RespondidoMensaje { set; get; }
      
    }
}
