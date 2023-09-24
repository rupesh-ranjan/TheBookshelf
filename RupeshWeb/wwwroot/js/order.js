var datatable;

$(document).ready(function () {
    var url = window.location.search;


    if (url.includes("inprocess")) {
        loadDataTable("inprocess");
    } else if (url.includes("pending")) {
        loadDataTable("pending");
    } else if (url.includes("completed")) {
        loadDataTable("completed");
    } else if (url.includes("approved")) {
        loadDataTable("approved");
    } else 
        loadDataTable("all");
});

function loadDataTable(status) {
    datatable = $('#tblData').DataTable({
        "ajax": { url: '/admin/order/getall?status=' + status },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'name', "width": "13%" },
            { data: 'phoneNumber', "width": "12%" },
            { data: 'applicationUser.email', "width": "23%" },
            { data: 'orderStatus', "width": "12%" },
            { data: 'orderTotal', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div>
                        <a style="width:90%" href="/admin/order/details?orderId=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i> Edit</a>
                    </div>`
                },
                "width": "25%"
            }
        ]
    });
}
