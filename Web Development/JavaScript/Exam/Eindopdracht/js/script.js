
$(document).ready(function () {
    let weather = null; // DIY POINTERS 
    $(".card").hover(function (event) { 
        let weather = $(this);
        weather.find('#extrainfo').toggle();
        let dat = weather.find("#datum")
        let reg = weather.find("#regendata")
        let kmp = weather.find("#windsnelheid")
        dat.text(dat.data("datum"))
        reg.text(reg.data("chance"))
        kmp.text(kmp.data("windsnelheid"))

    });
    $("#gpsbutton").on("click", function () {
        getLocation();
    });
    $("#clear").on("click", function () {
        localStorage.clear();
        console.log("Local storage cleared");
        alert("Reloading page");
        location.reload();
    });
    $("#zoekknop").on("click", function () {
        clearInterval(weather);
        $("#displaycontainer").toggle(false);
        let textbalk = $("#zoekbalk").val();
        if (textbalk.length == 0) {
            alert("Textbox veld is leeg")
        } else {
            localStorage.setItem("search", textbalk);
            $("#displaycontainer").toggle(false);
            callApi(textbalk);
            weather = setInterval(function () { callApi(textbalk); }, 60000);
        }
    });
    const fetchCurrentWeather= (data) => {
        console.log("calling fetchCurrentWeather: " + data)
        $.ajax({
            url: "http://api.weatherapi.com/v1/current.json?key=pleaseuseactualkey&q=" + data,
            type: "GET",
            dataType: "json",
            cache: false,
            error: function (response) {
                alert("Er is iets missgegaan: " + response.status)
                clearInterval(weather);
            },
            success: function (response) {
    
                setHTMLCurrent(response)
            },
        });
    }
    const fetchFutureWeather = (data) => {
        $("#displaycontainer").toggle(false);
        console.log("calling fetchFutureWeather: " + data)
        $.ajax({
            url: "http://api.weatherapi.com/v1/forecast.json?key=pleaseuseactualkey&q=" + data + "&days=3",
            type: "GET",
            dataType: "json",
            cache: false,
            error: function (response) {
                alert("Er is iets missgegaan: " + response.status)
                clearInterval(weather);
            },
            success: function (response) {
                setHTMLFuture(response)
            },
        });
    }
    const setHTMLCurrent = (response) => {
        let currentweather = $("#currentweather");
        currentweather.find("#weatherpicture").attr("src", (response['current']['condition']["icon"]));
        console.log("currentweather fetch data at: "+(response['location']['localtime']))
        currentweather.find("#lastupdated").text((response['location']['localtime']));
        currentweather.find("#degree").text((response['current']['temp_c']) + "°C");
        currentweather.find('#title').text((response['location']['name']));
        localStorage.setItem("current",JSON.stringify(response));
    }
    const setHTMLFuture= (response) => {
        for (i = 0; i < 3; i++) {
            let weather = response["forecast"]['forecastday'][i];
            let currentweather = $("#" + i);
            if (i == 0) {
                currentweather.find('#title').text("Today");
            } else {
                currentweather.find('#title').text(getDayName(weather["date"]));
            }
            currentweather.find("#weatherpicture").attr("src", (weather["day"]["condition"]["icon"]));
            currentweather.find("#status").text(weather["day"]["condition"]["text"]);
            currentweather.find("#min").text(weather["day"]['mintemp_c'] + "°C");
            currentweather.find("#max").text(weather["day"]['maxtemp_c'] + "°C");
            console.log("currentweather fetch data at: " + (response['current']['last_updated']))
            let extrainfo = currentweather.find('#extrainfo');
            extrainfo.find('#datum').data("datum", weather['date']);
            extrainfo.find('#regendata').data("chance", weather["day"]['daily_chance_of_rain']);
            extrainfo.find('#windsnelheid').data("windsnelheid", weather["day"]['maxwind_kph'])
            localStorage.setItem("future", JSON.stringify(response));
        }
    }
    const getLocation= () => { 
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(showPosition);
        }
    }
    const showPosition= (gps) => { 
        const cords = gps.coords.latitude + "," + gps.coords.longitude;
        clearInterval(weather);
        callApi(cords);
        localStorage.setItem("search", cords)
        weather = setInterval(function () { callApi(cords); }, 60000);
    
    }
    const callApi= (text) => { 
        $("#displaycontainer").toggle(false); // hide
        $.when(fetchCurrentWeather(text), fetchFutureWeather(text)).done(function () {
            $("#displaycontainer").toggle(true);
            // show
        });
    
    }
    const getDayName = (dateStr) => {
        let date = new Date(dateStr);
        return date.toLocaleDateString("en-EN", { weekday: 'long' });
    }

    
    if (localStorage.getItem("search") === null && localStorage.getItem("current") === null && localStorage.getItem("future") === null ) {
        console.log("local storage empty!");
        
    } else {
        alert("Showing cached data!!!!")
        $("#displaycontainer").toggle(true);
        console.log(localStorage.getItem("search"))
        setHTMLCurrent(JSON.parse(localStorage.getItem("current")))
        setHTMLFuture(JSON.parse(localStorage.getItem("future")))
        $("#zoekbalk").val(localStorage.getItem("search"))
    //   callApi(localStorage.getItem("search")); // INn het geval dat het nodig is dat we willen refreshen als de pagine de cache bin
    //   weather = setInterval(function () { callApi(localStorage.getItem("search")); }, 60000);
    }
});



