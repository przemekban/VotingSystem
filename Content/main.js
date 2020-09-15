$(document).ready(function () {
    $("#last-voting").hover(function () {
        var elementI = $(this).find("i").first();
        var h4 = $(this).find("h2").first();
        if (elementI.hasClass("hover")) {
            $(this).css("background-color", "");
            h4.css("color", "#333333");
            elementI.css("color", "#333333");
            elementI.removeClass("hover");
        }
        else {
            $(this).css("background-color", "#333333");
            h4.css("color", "#eeeeee");
            elementI.css("color", "#eeeeee");
            elementI.addClass("hover");
        }
    });

    $("#votings").hover(function () {
        var elementI = $(this).find("i").first();
        var h4 = $(this).find("h2").first();
        if (elementI.hasClass("hover")) {
            $(this).css("background-color", "");
            h4.css("color", "#333333");
            elementI.css("color", "#333333");
            elementI.removeClass("hover");
        }
        else {
            $(this).css("background-color", "#333333")
            h4.css("color", "#eeeeee");;
            elementI.css("color", "#eeeeee");
            elementI.addClass("hover");
        }
    });
});