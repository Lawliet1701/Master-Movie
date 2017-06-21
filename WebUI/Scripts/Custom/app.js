$(document).ready(function () {

    $("#rate").click(function () {
        $.post("/Movie/AddRating",
            {
                movieID: 162,
                rating: 7
            },
            function (data, status) {
                alert("Status: " + status);
            });
    });

});