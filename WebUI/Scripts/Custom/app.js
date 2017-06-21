$(document).ready(function () {

    $

    $(".empty-stars").click(function () {
        console.log("111");
        $.post("/Movie/AddRating",
            {
                movieID: this.id,
                rating: this.value
            },
            function (data, status) {
                console.log("Status: " + status);
            });
    });

});