var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tblData").DataTable({
        "ajax": {
            "url":"/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "title", "width": "25%" },
            { "data": "isbn", "width": "25%" },
            { "data": "price", "width": "25%" },
            { "data": "author", "width": "25%" }
        ]
    });
}