using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Grupo9_Ape3_ValidacionesWindowsForms.Ejercicio1;

namespace Grupo9_Ape3_ValidacionesWindowsForms
{
    public partial class Ejercicio1 : Form
    {

        // Lista para almacenar los datos
        private List<Persona> personas = new List<Persona>();
        private bool validacionesHabilitadas = true;  // Bandera para controlar las validaciones

        public Ejercicio1()
        {
            InitializeComponent();
        }

        // Clase para almacenar los datos de cada persona
        public class Persona
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Telefono { get; set; }
            public string FechaNacimiento { get; set; }
            public string Email { get; set; }
            public string Cedula { get; set; }
        }

        // Evento de Validación para cada campo (ya te los proporcioné antes)

        // Evento de Validating para la validación de cédula
        private void txtCedula_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!validacionesHabilitadas) return;
            string cedula = txtCedula.Text.Trim();
            if (!ValidarCedula(cedula))
            {
                errorProvider.SetError(txtCedula, "Cédula inválida.");
                e.Cancel = true;  // Cancela la validación si no es válida
            }
            else
            {
                errorProvider.SetError(txtCedula, "");
            }
        }

        // Evento de KeyPress para permitir solo números en la cédula
        private void txtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            validacionesHabilitadas = false;
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')  // Permite solo números y backspace
            {
                e.Handled = true;
            }
        }

        // Validación de la cédula (solo ejemplo simple de longitud)
        private bool ValidarCedula(string cedula)
        {
            
            return cedula.Length == 10 && long.TryParse(cedula, out _);  // Ejemplo de validación simple
        }

        // Evento de Validating para el teléfono
        private void txtTelefono_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!validacionesHabilitadas) return;
            string telefono = txtTelefono.Text.Trim();
            if (!Regex.IsMatch(telefono, @"^\d{10}$"))  // Valida que el teléfono tenga 10 dígitos
            {
                errorProvider.SetError(txtTelefono, "Teléfono inválido. Debe tener 10 dígitos.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtTelefono, "");
            }
        }

        // Evento de Validating para el correo electrónico
        private void txtEmail_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!validacionesHabilitadas) return;
            string email = txtEmail.Text.Trim();
            if (!Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                errorProvider.SetError(txtEmail, "Correo electrónico inválido.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtEmail, "");
            }
        }

        // Evento de KeyPress para permitir solo caracteres válidos en el nombre y apellido
        private void txtNombreApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!validacionesHabilitadas) return;
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != ' ')  // Solo letras y espacio
            {
                e.Handled = true;
            }
        }

        // Evento de Validating para el campo Nombre
        private void txtNombre_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!validacionesHabilitadas) return;
            string nombre = txtNombre.Text.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                errorProvider.SetError(txtNombre, "El nombre es obligatorio.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtNombre, "");
            }
        }

        // Evento de Validating para el campo Apellido
        private void txtApellido_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!validacionesHabilitadas) return;
            string apellido = txtApellido.Text.Trim();
            if (string.IsNullOrEmpty(apellido))
            {
                errorProvider.SetError(txtApellido, "El apellido es obligatorio.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtApellido, "");
            }
        }

        // Evento de Validating para la Fecha de Nacimiento
        private void txtFechaNacimiento_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!validacionesHabilitadas) return;
            DateTime fechaNacimiento;
            if (!DateTime.TryParse(txtFechaNacimiento.Text.Trim(), out fechaNacimiento) || fechaNacimiento > DateTime.Now)
            {
                errorProvider.SetError(txtFechaNacimiento, "Fecha de nacimiento inválida.");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(txtFechaNacimiento, "");
            }
        }

        // Evento de Click para el botón de validación
        private void btnValidar_Click(object sender, EventArgs e)
        {
            // Se activan las validaciones de todos los controles
            if (ValidateChildren())
            {
                MessageBox.Show("Todos los datos son válidos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Existen errores en los datos. Revise los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarFormulario())
            {
                // Crear un nuevo objeto persona con los datos del formulario
                Persona nuevaPersona = new Persona
                {
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Telefono = txtTelefono.Text,
                    FechaNacimiento = txtFechaNacimiento.Text,
                    Email = txtEmail.Text,
                    Cedula = txtCedula.Text
                };

                // Agregar el objeto persona a la lista
                personas.Add(nuevaPersona);

                // Actualizar el DataGridView
                dataGridView.DataSource = null;
                dataGridView.DataSource = personas;

                // Limpiar los campos
                LimpiarCampos();
            }
        }

        // Método para limpiar los campos del formulario
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
            txtFechaNacimiento.Clear();
            txtEmail.Clear();
            txtCedula.Clear();
        }

        // Método de validación del formulario
        private bool ValidarFormulario()
        {
            bool esValido = true;

            // Validar cada campo antes de permitir el guardado
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                errorProvider.SetError(txtNombre, "El nombre es obligatorio.");
                esValido = false;
            }
            else
            {
                errorProvider.SetError(txtNombre, "");
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                errorProvider.SetError(txtApellido, "El apellido es obligatorio.");
                esValido = false;
            }
            else
            {
                errorProvider.SetError(txtApellido, "");
            }

            if (string.IsNullOrWhiteSpace(txtTelefono.Text) || txtTelefono.Text.Length < 10)
            {
                errorProvider.SetError(txtTelefono, "El teléfono debe tener al menos 10 caracteres.");
                esValido = false;
            }
            else
            {
                errorProvider.SetError(txtTelefono, "");
            }

            if (string.IsNullOrWhiteSpace(txtFechaNacimiento.Text))
            {
                errorProvider.SetError(txtFechaNacimiento, "La fecha de nacimiento es obligatoria.");
                esValido = false;
            }
            else
            {
                errorProvider.SetError(txtFechaNacimiento, "");
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                errorProvider.SetError(txtEmail, "El correo electrónico no es válido.");
                esValido = false;
            }
            else
            {
                errorProvider.SetError(txtEmail, "");
            }

            if (string.IsNullOrWhiteSpace(txtCedula.Text) || txtCedula.Text.Length < 10)
            {
                errorProvider.SetError(txtCedula, "La cédula no es válida.");
                esValido = false;
            }
            else
            {
                errorProvider.SetError(txtCedula, "");
            }

            return esValido;
        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            validacionesHabilitadas = false;
            // Mensaje de ayuda detallado
            string mensajeAyuda =
                "Guía paso a paso para usar este formulario:\n\n" +
                "1. Ingrese su nombre en el campo 'Nombre'.\n" +
                "2. Ingrese su apellido en el campo 'Apellido'.\n" +
                "3. Ingrese su número de teléfono en el campo 'Teléfono'.\n" +
                "4. Ingrese su fecha de nacimiento en el campo 'Fecha de Nacimiento'.\n" +
                "5. Ingrese su correo electrónico en el campo 'Correo Electrónico'.\n" +
                "6. Ingrese su número de cédula de identidad en el campo 'Cédula'.\n" +
                "7. Asegúrese de que la cédula tenga 10 dígitos.\n" +
                "8. Después de completar todos los campos, haga clic en 'Guardar Datos' para guardar la información.\n" +
                "9. Si algún dato no es válido, se mostrará un mensaje de error.\n\n" +
                "¡Gracias por usar la aplicación!";

            // Mostrar el mensaje de ayuda en un MessageBox
            MessageBox.Show(mensajeAyuda, "Instrucciones de Uso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            validacionesHabilitadas = false;
            this.Hide();

            // Abre Form2
            Portada portada = new Portada();
            portada.ShowDialog();

            // Muestra de nuevo Form1 cuando se cierre Form2
            this.Show();
        }
    }
}


