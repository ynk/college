let myArr = [0,5,10,18,30]

// opdracht 1: 
//myArr.forEach(e => console.log(e));
// opdracht 2: 
let map = myArr.map(x=> x*5)
let uitkomst = map.filter(x => x > 30 )
let reduce = uitkomst.reduce(function(value,i){
    if(i % 2 == 0) {
        return value + i
    }
})
console.log(reduce)
// opdracht 3:
let glue =  [...myArr,54,68]
console.log(glue)
let gebruiker = {
    voornaam: "Yannick",
    achternaam : "Lol",
    leeftijd: 21
}
let gebruiker2 = {...gebruiker, geboorteplaats: "Gent" }
console.log(gebruiker2)

const {voornaam} = gebruiker2
console.log(voornaam)