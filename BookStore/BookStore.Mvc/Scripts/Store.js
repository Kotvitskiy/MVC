function search() {
    $.ajax({
        type: "POST",
        url: "[Route:Search]",
        data: { searchString: $("#searchValue").val() },
        success: onSearchSuccess
    });
}


function onSearchSuccess(info) {
    $("#contentBox").html("");
    $("#contentBox").append(info);
}

function searchMovie()
{
    $.ajax({
        type: "POST",
        url: "[Route:SearchMovie]",
        data: { searchString: $("#searchValue").val() },
        success: onSearchSuccess
    });
}