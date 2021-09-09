using ConsoleApp_p2.Modelo;
using ConsoleApp_p2.Vista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_p2.Controladoor
{
    public class Controlador
    {
        public MSNMessenger Modelo = new MSNMessenger();
        public MSNMessengerView Vista = new MSNMessengerView();

        public void Funcionar()
        {
            bool terminar = true;
            do
            {               
                MenuPrincipalOpt auxiliar = Vista.MostarMenuPrincipal();

                switch (auxiliar)
                {
                    case MenuPrincipalOpt.NuevoContacto:
                        Modelo.Refresh();
                        NuevoContacto();
                        break;

                    case MenuPrincipalOpt.NuevoChat:
                        Modelo.Refresh();
                        NuevoChat();
                        break;

                    case MenuPrincipalOpt.VerChats:
                        Modelo.Refresh();
                        VerChats(Modelo.Chats);
                        break;

                    case MenuPrincipalOpt.BuscarChats:
                        Modelo.Refresh();
                        BuscarChats();
                        break;
                }
            } while (terminar);
        }

        public void NuevoContacto()
        {
            ContactoViewModel contacto = Vista.MostrarPantallaCrearContacto();

            if (contacto != null)
            {
                Contact contactoAdd = new Contact(contacto.Nombre, contacto.Info);

                if (!Modelo.AddContacto(contactoAdd))
                {
                    Vista.MostrarError("Perdon, pero el contato ya existe");
                }
                return;
            }
        }

        public void NuevoChat()
        {
            int interoAuxiliar;

            List<ContactoViewModel> vmContacto = new List<ContactoViewModel>();

            foreach (Contact modContacto in Modelo.Contactos)
            {
                
                ContactoViewModel novoContacto = new ContactoViewModel()
                {
                    Nombre = modContacto.Nombre,
                    Info = modContacto.Info
                };

                vmContacto.Add(novoContacto);
            }

            interoAuxiliar = Vista.MostrarPantallaSeleccionDeContacto(vmContacto);

            if (interoAuxiliar != -1)
            {
                Charlar(Modelo.AddChat(Modelo.Contactos[interoAuxiliar]));
            }

        }

        public void Charlar(Chat NovoChat)
        {
            NovoChat.VistoActualizado();
           

            ChatViewModel cVM = new ChatViewModel()
            {
                Nombre = NovoChat.Contacto.Nombre,
                Info = NovoChat.Contacto.Info,
                Mensajes = new List<MensajeViewModel>()
            };
            foreach (Mensajes mens in NovoChat.Mensajes)
            {
                MensajeViewModel newMensajeNuevoVM = new MensajeViewModel()
                {
                    EsMio = mens.Esmio,
                    FechaHora = mens.HoraFecha,
                    Texto = mens.Text,
                    Visto = mens.Visto,
                    MensajeCitadoIndex = NovoChat.IndexDeMensaje(mens.RespondidoMensaje)
                };
                cVM.Mensajes.Add(newMensajeNuevoVM);
            };

            MensajeViewModel mensajeNuevoVM = new MensajeViewModel();

            mensajeNuevoVM = Vista.MostrarPantallaDeChat(cVM);

            while (mensajeNuevoVM != null)
            {
                Mensajes novoMensaje = new Mensajes();

                if (mensajeNuevoVM.MensajeCitadoIndex != -1)
                {
                    novoMensaje = new Mensajes()
                    {
                        EsMio = mensajeNuevoVM.EsMio,
                        HoraFecha = mensajeNuevoVM.FechaHora,
                        Text = mensajeNuevoVM.Texto,
                        Visto = mensajeNuevoVM.Visto,
                        RespondidoMensaje = NovoChat.Mensajes[mensajeNuevoVM.MensajeCitadoIndex]
                    };
                }
                else
                {
                    novoMensaje = new Mensajes()
                    {
                        EsMio = mensajeNuevoVM.EsMio,
                        HoraFecha = mensajeNuevoVM.FechaHora,
                        Text = mensajeNuevoVM.Texto,
                        Visto = mensajeNuevoVM.Visto,
                    };
                }

                NovoChat.EnviarMensaje(novoMensaje);
                NovoChat.VistoActualizado();
                NovoChat.Refresh();

                cVM.Mensajes.Clear();

                foreach (Mensajes mensa in NovoChat.Mensajes)
                {
                    MensajeViewModel newMensajeNuevoVM = new MensajeViewModel()
                    {
                        EsMio = mensa.Esmio,
                        FechaHora = mensa.HoraFecha,
                        Texto = mensa.Text,
                        Visto = mensa.Visto,
                        MensajeCitadoIndex = NovoChat.IndexDeMensaje(mensa.RespondidoMensaje)
                    };
                    cVM.Mensajes.Add(newMensajeNuevoVM);
                }

                mensajeNuevoVM = Vista.MostrarPantallaDeChat(cVM);
            }
        }

        public void VerChats(List<Chat> chat)
        {
            int aux;

            List<ChatItemViewModel> viewModelChat = new List<ChatItemViewModel>();

            foreach (Chat xat in chat)
            {
                int contadorNew = 0;

                foreach (Mensajes me in xat.Mensajes)
                {
                    if (me.Visto == false && !me.EsMio == true)
                    {
                        contadorNew++;
                    }
                }
                ChatItemViewModel newCITVM = new ChatItemViewModel()
                {
                    Info = xat.Contacto.Info,
                    CantMsjsNuevos = contadorNew,
                    Nombre = xat.Contacto.Nombre,
                    UltimoMsj = xat.Mensajes.Last().Text,
                };

                viewModelChat.Add(newCITVM);
            }
            aux = Vista.MostrarPantallaSeleccionDeChat(viewModelChat);

            if (aux != -1)
            {
                Charlar(chat[aux]);
            }
        }

        public void BuscarChats()
        {
            string auxiliar = Vista.MostrarPantallaDeBusqueda();

            List<Chat> listAuxiliarChat = new List<Chat>();

            if (auxiliar != null)
            {
                listAuxiliarChat = Modelo.SearchChat(auxiliar);

            }
        }
    }
}
