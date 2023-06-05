var urlSite;

function mostrar_modal() {
    $("#modal-loader").modal("show"); 
};

function ocultar_modal() {
    $("#modal-loader").modal("hide");
};

$(document).ready(function () {

    $("#combo-hosts").select2({
        minimumInputLength: 2,
        width: '100%',
        placeholder: 'Ingrese búsqueda',
        language: "es",
        ajax: {
            url: urlSite + "/Downloads/GetHostsWeb",
            dataType: 'json',
            type: "GET",
            quietMillis: 300,
            processResults: function (data) {
                var results = [];
                $.each(data, function (index, item) {
                    results.push({
                        id: item.name,
                        text: item.name
                    })
                });

                return { results };
            }
        },
        cache: true
    });

    $("#tablaHosts").DataTable({
        language: {
            "url": urlSite + "/Script/Spanish.json",
            "processing": "Procesando... espere"
        },
        dom: "frtip",
        ajax: {
            url: urlSite + "/Hosts/GetHosts",
            dataType: 'json',
            type: "POST"
        },
        columnDefs: [
            { "name": "Name", "targets": 0 },
            { "name": "Tags", "targets": 1 },
            { "name": "Enable", "targets": 2 },
            { "name": "LastConsult", "targets": 3 }
        ],
        processing: "true",

        serverSide: "true",
        order: [0, "asc"]
    });

    $("#tablaDownloads").DataTable({
        language: {
            "url": urlSite + "/Script/Spanish.json",
            "processing": "Procesando... espere"
        },
        dom: "frtip",
        ajax: {
            url: urlSite + "/Downloads/GetDownloads",
            dataType: 'json',
            type: "POST"
        },
        columnDefs: [
            { "name": "Order", "targets": 0 },
            { "name": "HostName", "targets": 1 },
            { "name": "FileID", "targets": 2 },
            { "name": "BytesTransfer", "targets": 3 },
            { "name": "Status", "targets": 4 },
            { "name": "Output", "targets": 5 },
            { "name": "LastUpdate", "targets": 6 }
        ],
        processing: "true",

        serverSide: "true",
        order: [0, "asc"]
    });

    $("#tablaUsuarios").DataTable({
        language: {
            "url": "https://cdn.datatables.net/plug-ins/1.10.12/i18n/Spanish.json",
            "processing": "Procesando... espere"
        },
        dom: "frtip",
    });

    $("#tablaFiles").DataTable({
        language: {
            "url": urlSite + "/Script/Spanish.json",
            "processing": "Procesando... espere"
        },
        dom: "frtip",
    });

    $("#tablaHostsReporte").DataTable({
        language: {
            "url": "https://cdn.datatables.net/plug-ins/1.10.12/i18n/Spanish.json",
            "processing": "Procesando... espere"
        },
        dom: "Bfrtip",
        buttons: [
            {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3]
                }
            },
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3]
                }
            },
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3]
                }
            }
        ]
    });

    $("#tablaDownloadsReporte").DataTable({
        language: {
            "url": "https://cdn.datatables.net/plug-ins/1.10.12/i18n/Spanish.json",
            "processing": "Procesando... espere"
        },
        dom: "Bfrtip",
        buttons: [
            {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6]
                }
            },
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6]
                }
            },
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6]
                }
            }
        ]
    });

});



