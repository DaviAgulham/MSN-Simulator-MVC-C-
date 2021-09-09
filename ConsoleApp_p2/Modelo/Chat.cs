using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_p2.Modelo
{
    public class Chat
    {
        public Contact Contacto { set; get; }
        
        public List<Mensajes> Mensajes { set; get; } = new List<Mensajes>();

        public Random Rng { set; get; } = new Random();

        public Chat(Contact contacto)
        {
            this.Contacto = contacto;
        }

        public int ContadorNoLeidos(List<Mensajes> mensajes)
        {
            int auxiliar = 0;
            foreach (Mensajes men in mensajes)
            {
                if (!men.EsMio && !men.Visto)
                {
                    auxiliar++;
                }
            }
            return auxiliar;
        }

        public void EnviarMensaje(Mensajes mensajes)
        {
            if (string.IsNullOrEmpty(mensajes.Text.Trim()))
            {
                throw new ArgumentException();
            }
            else
            {
                mensajes.EsMio = true;
                Mensajes.Add(mensajes);
            }
        }

        public bool ContieneTermino(string texto)
        {
            bool auxiliarBooleano = false;
            foreach (Mensajes men in Mensajes)
            {
                if (men.Text.Contains(texto))
                {
                    auxiliarBooleano = true;
                    break;
                }
            }
            return auxiliarBooleano;
        }

        public void VistoActualizado()
        {
            foreach (Mensajes men in this.Mensajes)
            {
                if (!men.EsMio && !men.Visto)
                {
                    men.Visto = true;
                }
            }
        }

        public int IndexDeMensaje(Mensajes mensajes)
        {
            if (this.Mensajes.Contains(mensajes))
                return this.Mensajes.IndexOf(mensajes);

            return -1;
        }

        public bool Refresh()
        {
            int valor = Rng.Next(0, 101);

            if (valor >= 25)
            {
                return false;
            }
            else
            {
                VistoActualizado();

                if (this.Mensajes.Count() == 0)
                {
                    Mensajes newMsj = new Mensajes()
                    {
                        Text = "Hola, todo bien?",
                        EsMio = false,
                        Visto = false,
                        RespondidoMensaje = null
                    };
                    this.Mensajes.Add(newMsj);
                }
                else if (this.Mensajes.Last().EsMio)
                {
                    Mensajes newMsj = new Mensajes()
                    {
                        Text = this.Mensajes.Last().Text.ToUpper(),
                        EsMio = false,
                        Visto = false,
                        RespondidoMensaje = null
                    };
                    this.Mensajes.Add(newMsj);
                }
                else if (!this.Mensajes.Last().EsMio)
                {
                    Mensajes newMsj = new Mensajes()
                    {
                        Text = "daleee che contestame",
                        EsMio = false,
                        Visto = false,
                        RespondidoMensaje = null
                    };
                    this.Mensajes.Add(newMsj);
                }
                return true;
            }
            /*foreach (Mensajes men in this.Mensajes)
            {
                if (men.EsMio && !men.Visto)
                {
                    men.Visto = true;
                }
            }

            if (this.Mensajes.Count() == 0)
            {
                this.Mensajes.Add(new Mensajes());
            }

            if (Mensajes.Last().EsMio)
            {
                this.Mensajes.Add(new Mensajes());
                //Bot spameando nuestro mensaje en mayusculas
            }

            if (!Mensajes.Last().EsMio)
            {
                this.Mensajes.Add(new Mensajes());
            }
            return true;*/
        }
    }
}
