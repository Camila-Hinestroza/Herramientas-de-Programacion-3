document.addEventListener("DOMContentLoaded", function() {
    var form = document.getElementById('FormularioUsuarios');
    form.addEventListener('submit', function(event) {
        event.preventDefault();

        var nombre = document.getElementById('usuarioNombre').value;
        var correo = document.getElementById('usuarioCorreo').value;
        var nacimiento = document.getElementById('usuarioNacimiento').value;


        var listaUsuarios = document.getElementById('listaUsuarios');
        var newRow = listaUsuarios.insertRow(-1);
        var cell1 = newRow.insertCell(0);
        var cell2 = newRow.insertCell(1);
        var cell3 = newRow.insertCell(2);
        var cell4 = newRow.insertCell(3);

        cell1.innerHTML = nombre;
        cell2.innerHTML = correo;
        cell3.innerHTML = nacimiento;
        cell4.innerHTML = '<button class="deleteButton">Eliminar</button>';

        form.reset();
    });

    // Agregar evento de escucha para eliminar usurios
    document.getElementById('listaUsuarios').addEventListener('click', function(event) {
        if (event.target.classList.contains('deleteButton')) {
            if (confirm('¿Estás seguro de eliminar este Usuario?')) {
                var row = event.target.parentNode.parentNode;
                row.parentNode.removeChild(row);
            }
        }
    });
});
