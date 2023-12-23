using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfPce.vistas
{
    /// <summary>
    /// Lógica de interacción para AgregarEquipo.xaml
    /// </summary>
    public partial class AgregarEquipo : Window
    {
        private static Regex s_regex = new Regex("[^0-9]+");
        public AgregarEquipo()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbNombreEquipo.Text) ||
                string.IsNullOrWhiteSpace(tbCantJugadores.Text) ||
                string.IsNullOrWhiteSpace(tbNombreDt.Text) ||
                cbTipoEquipo.SelectedItem == null ||
                string.IsNullOrWhiteSpace(tbCapitanEquipo.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.");
                return;
            }
            try
            {
                NegocioPce.Equipo equipo = new NegocioPce.Equipo();
                equipo.NombreEquipo = tbNombreEquipo.Text;
                equipo.CantidadJugadores = int.Parse(tbCantJugadores.Text);
                equipo.NombreDT = tbNombreDt.Text;

                if (cbTipoEquipo.SelectedItem is ComboBoxItem selected)
                {
                    equipo.TipoEquipo = selected.Content.ToString();
                }

                equipo.CapitanEquipo = tbCapitanEquipo.Text;
                equipo.TieneSub21 = cbTiene21.IsChecked ?? false;

                bool response = equipo.Create();

                if (response)
                {
                    MessageBox.Show("Equipo fue agregado correctamente");
                    AgregarOtroEquipo();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error. Intentelo más tarde");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CheckTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = s_regex.IsMatch(e.Text);
        }
        private void AgregarOtroEquipo()
        {
            string title = "Agregar nuevo Equipo";
            string message = "¿Desea agregar nuevo equipo?";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(message, title, buttons);

            if (result == MessageBoxResult.No)
            {
                this.Close();
            }
        }

        private void LimpiarCampos()
        {
            tbNombreEquipo.Text = string.Empty;
            tbCantJugadores.Text = string.Empty;
            tbNombreDt.Text = string.Empty;
            cbTipoEquipo.Text = string.Empty;
            tbCapitanEquipo.Text = string.Empty;
            cbTiene21.IsChecked = false;
        }
    }
}
