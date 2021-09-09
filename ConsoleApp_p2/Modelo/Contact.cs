using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_p2.Modelo
{
    public class Contact
    {
        public string Nombre { set; get; }
        public string Info { set; get; }

        public Contact(string nombre, string info)
        {
            if (nombre == null)
                throw new ArgumentException("Nombre");
            
            if (info == null)
                throw new ArgumentException("Info");


            this.Nombre = nombre;
            this.Info = info;
        }

        public Contact(Vista.ContactoViewModel contactoView)
        {
            this.Nombre = contactoView.Nombre;
            this.Info = contactoView.Info;
        }
    }
}
