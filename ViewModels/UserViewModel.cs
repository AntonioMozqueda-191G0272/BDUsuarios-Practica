using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDUsuarios_Practica.Repositories;
using BDUsuarios_Practica.Views;
using BDUsuarios_Practica.Models;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace BDUsuarios_Practica.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Usuario usuario;
        public Usuario Usuario
        {
            get { return usuario; }
            set { usuario = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Usuario"));
            }
        }

        private string correo;
        public string Correo
        {
            get { return correo; }
            set { correo = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Correo"));
            }
        }
        private string contraseña;
        public string Contraseña
        {
            get { return contraseña; }
            set { contraseña = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Contraseña"));
            }
        }

        private string error;
        public string Error
        {
            get { return error; }
            set { error = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Error"));
            }
        }

        UsuariosRepositories reposUser = new UsuariosRepositories();
        LoginRepository reposlogin = new LoginRepository();

        public ICommand verRegistrarCommand { get; set; } //Abrir ventana para registrar
        public ICommand RegistrarUsuarioCommand { get; set; } //Registrar Usuario
        public ICommand verEditarCommand { get; set; }  //Abrir ventana para editar
        public ICommand EditarCommand { get; set; } //Editar Usuario
        public ICommand LoginCommand { get; set; }  //Iniciar sesion
        public ICommand VerAdministrarCommand { get; set; } //Abre la ventana Administrar
        public ICommand CerrarSesionCommand { get; set; }   //Cierra ventana Administrar

        RegistrarUsuarioView RegistrarDialog = new RegistrarUsuarioView();
        EditarView EditarDialog = new EditarView();
        AdministrarView AdminDialog = new AdministrarView();

        //LLAMADA VIEWS
        //ventana registrar
        private void verRegistrar()
        {
            Error = "";
            RegistrarDialog = new RegistrarUsuarioView();
            RegistrarDialog.DataContext = this;
            Usuario = new Usuario();
            RegistrarDialog.ShowDialog();
        }

        public void verEditar(Usuario u)
        {
            u = reposUser.GetByEMail(u);
            Error = "";
            if (u != null)
            {
                Usuario copia = new Usuario()
                {
                    EMail = u.EMail,
                    Nombre = u.Nombre,
                    Direccion = u.Direccion,
                    Telefono = u.Telefono,
                    Contraseña = null,
                    Id = u.Id
                };
                usuario = copia;
                EditarDialog = new EditarView();
                EditarDialog.DataContext = this;
                EditarDialog.ShowDialog();
            }
        }
        private void CerrarAdministrar()
        {
            error = "";
            AdminDialog = new AdministrarView();
            AdminDialog.Close();
        }

        //LLAMADA METODOS
        //Registrar
        private void RegistrarUsuario()
        {
            Error = "";
            try
            {
                if (reposUser.Validate(Usuario))
                {
                    reposUser.Registrar(Usuario);
                    RegistrarDialog.Close();
                }
            }
            catch (Exception ex)
            {

                Error = ex.Message;
            }

        }

        //Iniciar o login
        private void Iniciar()
        {
            Error = "";
            try
            {
                if (reposlogin.ValidateLogin(Usuario) == true)
                {
                    
                    AdminDialog = new AdministrarView();
                    AdminDialog.DataContext = this;
                    reposlogin.login(Usuario);
                    AdminDialog.ShowDialog();
                }
            }
            catch (Exception ex)
            {

                Error = ex.Message;
            }
        }

        public void ModificacionDatos()
        {
            Error = "";
            try
            {
                if (reposUser.Validate(usuario))
                {
                    reposUser.Update(usuario);
                    EditarDialog.Close();
                    Error = "";
                }
            }
            catch (Exception ex)
            {

                Error = ex.Message;
            }
        }

        //CONSTRUCTOR
        public UserViewModel()
        {
            verRegistrarCommand = new RelayCommand(verRegistrar);
            RegistrarUsuarioCommand = new RelayCommand(RegistrarUsuario);
            LoginCommand = new RelayCommand(Iniciar);
            Usuario = new Usuario();
            verEditarCommand = new RelayCommand<Usuario>(verEditar);
            EditarCommand = new RelayCommand(ModificacionDatos);
            CerrarSesionCommand = new RelayCommand(CerrarAdministrar);
        }
    }
}