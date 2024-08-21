let personen=[       
	{
        voornaam: 'Jan',
        familienaam: 'Janssens',
        geboorteDatum: new Date('2010-10-10'),
        email: 'jan@example.com',
        aantalKinderen: 0
    },
    {
        voornaam: 'Mieke',
        familienaam: 'Mickelsen',
        geboorteDatum: new Date('1980-01-01'),
        email: 'mieke@example.com',
        aantalKinderen: 1
    },
    {
        voornaam: 'Piet',
        familienaam: 'Pieters',
        geboorteDatum: new Date('1970-12-31'),
        email: 'piet@example.com',
        aantalKinderen: 2
    }
];



const initialize = () => {
    
    selectedElement = document.getElementById('persoonSelect');
    RefreshSelection()
    document.getElementById('bewaar').addEventListener('click',bewaar)
    document.getElementById('nieuw').addEventListener('click',ClearInputFields)
    
    selectedElement.addEventListener('change', (event) => {
        p = personen[event.target.value]
        document.getElementById("Index").innerText = event.target.value
        document.getElementById("Voornaam").value = p.voornaam
        document.getElementById("Familienaam").value = p.familienaam;
        console.log(p.geboorteDatum)
        document.getElementById("Geboortedatum").value = p.geboorteDatum.toISOString().substr(0, 10)
        document.getElementById("Email").value = p.email;
        document.getElementById("aantalKinderen").value = p.aantalKinderen
      });


}

function RefreshSelection(){

     selectedElement.innerHTML = ""; // Reset option fields
    personen.forEach((element, index) => {
        var opt = document.createElement('option');
        opt.appendChild( document.createTextNode(element.voornaam+" "+element.familienaam) );
        opt.value = index; 
        selectedElement.appendChild(opt); 

    });
}

function Validate(){
    var inputElementen = Array.from(document.getElementsByTagName("input"));
    let okay = true
    inputElementen.forEach((element) => {
        if(element.value == ""){
            alert("Je hebt de input box van: "+ element.id+ " Niet correct ingevuld")
            okay = false
        }
    });
    return okay
}

function bewaar(){

    if(Validate()){
        var Voornaam = document.getElementById("Voornaam").value;
        var Familienaam = document.getElementById("Familienaam").value;
        var Geboortedatum = new Date(document.getElementById("Geboortedatum").value);
        var Email = document.getElementById("Email").value;
        var AantalKinderen = document.getElementById("aantalKinderen").value;
        var i = document.getElementById("Index").innerText;
        console.log(i);
        if(i){
            // oud updaten
           let object = personen[i]
           object.voornaam = Voornaam
           object.familienaam = Familienaam
           object.geboorteDatum = new Date(Geboortedatum)
           object.email = Email
           object.aantalKinderen = AantalKinderen
        }else{
            index = personen.length
            let p = {voornaam:Voornaam, familienaam:Familienaam, geboorteDatum:new Date(Geboortedatum), email:Email,aantalKinderen:AantalKinderen};
            personen.push(p)
        }
        RefreshSelection()
        ClearInputFields()
    }else{
        alert("Check input")
    }
}
function ClearInputFields(){
    document.getElementById("Index").innerText = ''
    let inputElementen = Array.from(document.getElementsByTagName("input"));
    inputElementen.forEach((element) => {
        element.value = ""//Reset all elements met input tag
    });

}
window.addEventListener("load", initialize);