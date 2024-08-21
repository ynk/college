let student1 = {
    voornaam : "Jan",
    familienaam : "Janssens",

    geboorteDatum : new Date("1993-12-31")
}

const initialize = () => {
    console.log(JSON.stringify(student1))
}
window.addEventListener("load", initialize);