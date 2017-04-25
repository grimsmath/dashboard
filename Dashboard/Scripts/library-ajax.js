var LibraryAjax = {
    version: "1.0",
    author: "David King, Jr.",

    /*
     * Creates an HTML Table element that contains the JSON results
     */
    LoadApiDataTable: function (api, container, table, elementclass) {
        $.ajax({
            type: "GET",
            url: api,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: "{}",
            success: function (res) {
                container.append(ConvertJsonToTable(res.data, table, elementclass)).fadeIn();
            }
        });
    }
}