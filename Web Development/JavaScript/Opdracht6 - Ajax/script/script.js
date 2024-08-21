$(document).ready(function () {
    $("#zoek").click(function () {
        let username = $("#username").val();
        if (username.length == 0) {
            alert("Er moet een username worden ingegeven")// Ik stuur niet graag lege usernames
        } else {
            $.ajax({
                url: "https://api.github.com/users/" + username,
                type: "GET",
                dataType: "json",
                error: function (response) {
                    alert("Er is iets missgegaan: " + response.status)
                },
                success: function (response) {
                    console.log(response)
                    let img = $("#gitImg")
                    img.attr('src', response.avatar_url);
                    img.width(128);
                    img.height(128);
                    $("#gitNaam").text(response.name)
                    $("#gitUrl").attr("href", response.html_url).text(response.html_url)
                },
            });
        }
    });
});