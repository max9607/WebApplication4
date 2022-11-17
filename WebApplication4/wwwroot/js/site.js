// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
     $('#myTable').DataTable({
        "paging": true,
        language: {
            info: "Mostrando _PAGE_ de _PAGES_ paginas",
            lengthMenu: "Mostrando _MENU_ resultados",
            search: "Buscar:",
            infoEmpty: "Mostrando 0 a 0 de 0 resultados",
            infoFiltered: "(Busqueda de _MAX_ registros totales)",
            paginate: {
                first: "Primero",
                previous: "Anterior",
                next: "Siguiente",
                last: "Ultimo"
            },
            buttons: {
                copy: "Copiar",
                pdf: "Descargar"
            },
            

        },
         //dom: "<Bfl<t>ip>",
         dom: "Q<'row'<'col-sm-12 col-md-6'l><'col-sm-12 col-md-6'f>>" +
             "<'row'<'col-sm-12'tr>>" +
             "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>" + "<'row'<'col-sm-12 pb-3'B>>",
         // es como un <row clas='col-sm-12 -pb3'> funciona igual que un div
         //https://datatables.net/reference/option/dom
         buttons: [
             'copy', 'excel', 'pdf',
             
         ]
    });
});

$(document).ready(function () {
    $('#Secundaria').DataTable({
        "paging": true,
        language: {
            info: "Mostrando _PAGE_ de _PAGES_ paginas",
            lengthMenu: "Mostrando _MENU_ resultados",
            search: "Buscar:",
            infoEmpty: "Mostrando 0 a 0 de 0 resultados",
            infoFiltered: "(Busqueda de _MAX_ registros totales)",
            paginate: {
                first: "Primero",
                previous: "Anterior",
                next: "Siguiente",
                last: "Ultimo"
            },
            buttons: {
                copy: "Copiar",
                pdf: "Descargar"
            },


        },
        //dom: "<Bfl<t>ip>",
        dom: "<'row'<'col-sm-12 col-md-6'l><'col-sm-12 col-md-6'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>" + "<'row'<'col-sm-12 pb-3'>>",
        // es como un <row clas='col-sm-12 -pb3'> funciona igual que un div
        //https://datatables.net/reference/option/dom
        buttons: [
            'copy', 'excel', 'pdf',

        ]
    });
});


// Get the modal
var modal = document.getElementById("myModal");

// Get the image and insert it inside the modal - use its "alt" text as a caption
var img = document.getElementById("myImg");
var modalImg = document.getElementById("img01");
var captionText = document.getElementById("caption");
img.onclick = function () {
    modal.style.display = "block";
    modalImg.src = this.src;
    captionText.innerHTML = this.alt;
}

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// When the user clicks on <span> (x), close the modal
span.onclick = function () {
    modal.style.display = "none";
}