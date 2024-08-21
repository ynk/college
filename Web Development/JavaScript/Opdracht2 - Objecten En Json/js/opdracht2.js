let student1 = JSON.parse(JSON.stringify({"voornaam":"Jan","familienaam":"Janssens","geboorteDatum":"1993-12-31T00:00:00.000Z"}))

const initialize = () => {
    console.log(student1.voornaam)
}
window.addEventListener("load", initialize);