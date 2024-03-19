document.addEventListener("DOMContentLoaded", function() {
    var form = document.getElementById('FormularioLibros');
    form.addEventListener('submit', function(event) {
        event.preventDefault();

        var titulo = document.getElementById('libroTitulo').value;
        var autor = document.getElementById('libroAutor').value;
        var editorial = document.getElementById('libroEditorial').value;
        var categoria = document.getElementById('libroCategoria').value;
        var fecha = document.getElementById('libroFecha').value;

        var listaLibros = document.getElementById('listaLibros');
        var newRow = listaLibros.insertRow(-1);
        var cell1 = newRow.insertCell(0);
        var cell2 = newRow.insertCell(1);
        var cell3 = newRow.insertCell(2);
        var cell4 = newRow.insertCell(3);
        var cell5 = newRow.insertCell(4);
        var cell6 = newRow.insertCell(5);

        cell1.innerHTML = titulo;
        cell2.innerHTML = autor;
        cell3.innerHTML = editorial;
        cell4.innerHTML = categoria;
        cell5.innerHTML = fecha;
        cell6.innerHTML = '<button class="deleteButton">Eliminar</button>';

        form.reset();
    });

    // Agregar evento de escucha para eliminar libro
    document.getElementById('listaLibros').addEventListener('click', function(event) {
        if (event.target.classList.contains('deleteButton')) {
            if (confirm('¿Estás seguro de eliminar este libro?')) {
                var row = event.target.parentNode.parentNode;
                row.parentNode.removeChild(row);
            }
        }
    });
});

