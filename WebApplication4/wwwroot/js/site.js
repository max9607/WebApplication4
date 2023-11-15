// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    //$.fn.dataTable.moment('dd/MM/yyyy HH:mm');
    //$.fn.dataTable.moment('HH:mm MMM D, YY');
    //$.fn.dataTable.moment('ddd, MMMM Do, YYYY');
    $.fn.dataTable.moment('DD-MM-YYYY HH:mm');
    $('#myTable').DataTable({              
        "paging": true,

            
        columnDefs: [{
           
            targets: [3,4],
            /* 			render: $.fn.dataTable.render.moment( 'DD/MM/YY' ) */
            render: function (data) {
                return moment(data, 'YYYY-MM-DD HH:mm').format('DD-MM-YYYY HH:mm');
            }
        }],

        /*language: {
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
            searchBuilder: {
                add: "Añadir Filtro",
                logicAnd: "Y",
                logicOr: "O",
                data: "Datos",
                condition: "Condiciones",
                value: "Valor",
                conditions: {
                    array: {
                        contains: 'Contiene',
                        empty: 'Vacío',
                        equals: 'Igual',
                        not: 'No',
                        notEmpty: 'No vacío',
                        without: 'Sin que'
                    },
                    string: {
                        contains: 'Contiene',
                        empty: 'Vacío',
                        endsWith: 'Termina en',
                        equals: 'Igual',
                        not: 'No sea',
                        notContains: 'No contiene',
                        notEmpty: 'No esté vacío',
                        notEndsWith: 'No termina con',
                        notStartsWith: 'No empieza con',
                        startsWith: 'Empieza con',
                    },
                    number: {
                        between: 'Entre',
                        empty: 'Vacío',
                        equals: 'Igual',
                        gt: 'Mayor que',
                        gte: 'Mayor o igual que',
                        lt: 'Menor que',
                        lte: 'Menor o igual que',
                        not: 'No sea',
                        notBetween: 'No está entre',
                        notEmpty: 'No esté vacío',
                    },
                    date: {
                        after: 'Después',
                        before: 'Antes de',
                        between: 'Entre',
                        empty: 'Vacío',
                        equals: 'Igual',
                        not: 'No sea',
                        notBetween: 'No estén entre',
                        notEmpty: 'No esté vacío'
                    }
                },
                clearAll: "Borrar todos los filtros",
                title: "Busqueda avanzada",
            },
        
        		

        },*/
        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.13.1/i18n/es-ES.json',
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
$(document).ready(function () {
    //$.fn.dataTable.moment('dd/MM/yyyy HH:mm');
    //$.fn.dataTable.moment('HH:mm MMM D, YY');
    //$.fn.dataTable.moment('ddd, MMMM Do, YYYY');
    $.fn.dataTable.moment('DD-MM-YYYY HH:mm');
    $('#busqueda').DataTable({
        "paging": true,


        columnDefs: [{

            targets: 4,
            /* 			render: $.fn.dataTable.render.moment( 'DD/MM/YY' ) */
            render: function (data) {
                return moment(data, 'YYYY-MM-DD HH:mm').format('DD-MM-YYYY HH:mm');
            }
        }],

        language: {
            url: 'https://cdn.datatables.net/plug-ins/1.13.1/i18n/es-ES.json',
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

