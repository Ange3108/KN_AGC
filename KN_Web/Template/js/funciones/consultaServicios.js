$(function () {

    var table = new DataTable('#tablaServicios', {
        language: {
            url: 'https://cdn.datatables.net/plug-ins/2.3.7/i18n/es-ES.json',
        },
        columnDefs: [
            { targets: '_all', className: 'text-start' }
        ]
    });
});