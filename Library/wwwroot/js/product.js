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
            { "data": "title", "width": "10%" },
            { "data": "isbn", "width": "10%" },
            { "data": "price", "width": "10%" },
            { "data": "author", "width": "10%" },
            { "data": "category.name", "width": "10%" },
            { "data": "coverType.name", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                         <div class="w-100 btn-group" role = "group">
                              <a href="/Admin/Product/Upsert?id=${data}"
                                 class="btn btn-secondary mx-2 rounded-2" > <i class="bi bi-pencil-square"></i>&nbsp;&nbsp;Update</a>
                              <a href="/Admin/Product/Delete?id=${data}"
                                 class="btn btn-danger mx-2 rounded-2" > <i class="bi bi-trash"></i>&nbsp;&nbsp;Delete</a>
                         </div>   
                           `                 
                },
                "width":"10%"
            }
        ]
    });
}