using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfPce.vistas
{
    /// <summary>
    /// Lógica de interacción para ModificarEquipo.xaml
    /// </summary>
    public partial class ModificarEquipo : Window
    {
        NegocioPce.Equipo equipo;
        private static Regex s_regex = new Regex("[^0-9]+");
        public ModificarEquipo(int id)
        {
            InitializeComponent();
            this.Title = string.Format("Actualizar equipo {0}", id);

            equipo = new NegocioPce.Equipo();

            CargarFormulario(id);
        }

        private void ButtonActualizar_Click(object sender, RoutedEventArgs e)
        {
            equipo.NombreEquipo = tbModNombreEquipo.Text;
            equipo.CantidadJugadores = int.Parse(tbModCantJugadores.Text);
            equipo.NombreDT = tbModNombreDt.Text;

            if (tbModTipoEquipo.SelectedItem is ComboBoxItem selected)
            {
                equipo.TipoEquipo = selected.Content.ToString();
            }

            equipo.CapitanEquipo = tbModCapitanEquipo.Text;
            equipo.TieneSub21 = (cbModTiene21.IsChecked.Value) ? true : false;

            if (string.IsNullOrWhiteSpace(tbModNombreEquipo.Text) ||
                string.IsNullOrWhiteSpace(tbModCantJugadores.Text) ||
                string.IsNullOrWhiteSpace(tbModNombreDt.Text) ||
                tbModTipoEquipo.SelectedItem == null ||
                string.IsNullOrWhiteSpace(tbModCapitanEquipo.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.");
                return;
            }

            bool response = equipo.Update();

            if (response)
            {
                MessageBox.Show(string.Format("Equipo {0} ha sido actualizado exitósamente!", equipo.EquipoId));
                this.Close();
            }
            else
            {
                MessageBox.Show(string.Format("No fue posible actualizar el equipo {0}", equipo.EquipoId));
                this.Close();
            }
        }
        private void CargarFormulario(int id)
        {
            bool response = equipo.Read(id);
            if (response)
            {
                tbModNombreEquipo.Text = equipo.NombreEquipo;
                tbModCantJugadores.Text = equipo.CantidadJugadores.ToString();
                tbModNombreDt.Text = equipo.NombreDT;

                if (equipo.TipoEquipo == "FEMENINO")
                {
                    tbModTipoEquipo.SelectedIndex = 0;
                }
                else if (equipo.TipoEquipo == "MASCULINO")
                {
                    tbModTipoEquipo.SelectedIndex = 1;
                }

                tbModCapitanEquipo.Text = equipo.CapitanEquipo;
                cbModTiene21.IsChecked = equipo.TieneSub21;
            }
            else
            {
                MessageBox.Show(string.Format("El equipo con ID {0} no fue encontrado.", id));
            }
        }
        private void CheckTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = s_regex.IsMatch(e.Text);
        }
    }
}
