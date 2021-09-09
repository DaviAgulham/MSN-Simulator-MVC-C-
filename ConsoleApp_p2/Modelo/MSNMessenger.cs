using ConsoleApp_p2.Modelo;
using ConsoleApp_p2.Vista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_p2.Modelo
{
    public class MSNMessenger
    {
        public List<Contact> Contactos { set; get; } = new List<Contact>();
        public List<Chat> Chats { set; get; } = new List<Chat>();

        public bool AddContacto(Contact newContacto)
        {
            foreach (Contact con in this.Contactos)
            {
                if (con.Nombre == newContacto.Nombre)
                {
                    return false;
                }
            }
            this.Contactos.Add(newContacto);
            return true;
        }

        public Chat AddChat(Contact contacto)
        {
            foreach (Chat cha in Chats)
            {
                if (cha.Contacto == contacto)
                {
                    return cha;
                }                     
            }
            this.Chats.Add(new Chat(contacto));

            return Chats.Last();
        }

        public void Refresh()
        {
            foreach (Chat cha in this.Chats)
            {
                cha.Refresh();
            }
        }

        public List<Chat> SearchChat(string busca)
        {
            List<Chat> auxList = new List<Chat>();

            foreach (Chat cha in Chats)
            {
                foreach (Mensajes men in cha.Mensajes)
                {
                    if (men.Text.Contains(busca))
                    {
                        auxList.Add(cha);
                        break;
                    }
                }
            }
            return auxList;
        }

    }
}
