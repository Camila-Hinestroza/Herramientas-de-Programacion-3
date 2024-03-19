document.addEventListener("DOMContentLoaded", function() {
    // Seleccionar el formulario y agregar un evento de escucha para el envío
    var form = document.querySelector('form');
    form.addEventListener('submit', function(event) {
        event.preventDefault(); // Prevenir el envío del formulario por defecto

        // Obtener los valores de los campos del formulario
        var nombre = document.getElementById('nombre').value;
        var email = document.getElementById('email').value;
        var mensaje = document.getElementById('mensaje').value;

        // Verificar si algún campo está vacío
        if (nombre === '' || email === '' || mensaje === '') {
            alert('Por favor, complete todos los campos del formulario.');
        } else {
            // Mostrar una ventana de alerta con la información del formulario
            var info = 'Nombre: ' + nombre + '\nCorreo Electrónico: ' + email + '\nMensaje: ' + mensaje;
            alert(info);

            // Restablecer los valores de los campos del formulario
            form.reset();
        }
    });
});
