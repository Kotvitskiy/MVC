document.onkeyup = function (e) {
    e = e || window.event;
    if (e.keyCode === 13) {
        if ($("searchValue").val() != "") {
            search();
        }
    }
    return false;
}

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