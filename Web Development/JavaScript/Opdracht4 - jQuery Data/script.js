let personen = [
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



$(document).ready(function () {
    let personenSelection = $("#persoonSelect")

    fillSelection();

    $("#bewaar").on("click", bewaar); //bewaar handler
    $("#nieuw").on("click", clearInput); // nieuw handler


    let i = $("#index")
    let voornaam = $("#voorNaam")
    let achternaam = $("#familieNaam")
    let dob = $("#geboorteDatum")
    let email = $("#email")
    let ak = $("#aantalKinderen")


    personenSelection.change(function (e){
        let pos = $(this).children("option:selected").val()
        let index = personen[pos];
        voornaam.data("naam",index.voornaam)
        achternaam.data("famNaam",index.familienaam)
        dob.data("dobirth",index.geboorteDatum)
        email.data("email",index.email)
        ak.data("aantalKinderen",index.aantalKinderen)
        let dataNaam = voornaam.data("naam");
        let dataFam = achternaam.data("famNaam");
        let dataDob = dob.data("dobirth");
        let dataEmail = email.data("email");
        let dataAk = ak.data("aantalKinderen");
    
        i.text(pos)
        achternaam.val(dataFam)
        voornaam.val(dataNaam)
        dob.val(dataDob.toISOString().substr(0, 10))
        email.val(dataEmail)
        ak.val(dataAk)
    })
    
    function fillSelection(){
        jQuery.each(personen, function (i, val) {
            personenSelection.append($('<option>').val(i).text(val.voornaam + " "
                + val.familienaam).data("option",i))

        });
    }
    function emptySelection(){
        personenSelection.empty();
    }    
    function bewaar() {
        voornaam.data("naam",voornaam.val())
        achternaam.data("famNaam",achternaam.val())
        dob.data("dobirth",dob.val())
        email.data("email",email.val())
        ak.data("aantalKinderen",ak.val())
        let dataNaam = voornaam.data("naam");
        let dataFam = achternaam.data("famNaam");
        let dataDob = dob.data("dobirth");
        let dataEmail = email.data("email");
        let dataAk = ak.data("aantalKinderen");
        let index = i.text()
        if(validate()){
            if(index){
                let object = personen[index]
                object.voornaam = dataNaam
                object.familienaam = dataFam
                object.geboorteDatum = new Date(dataDob)
                object.email = dataEmail
                object.aantalKinderen = dataAk
                console.log("updated!")
            }else{
                //create new
                console.log("Added to array!")
                index = personen.length
                let p = {voornaam:dataNaam, familienaam:dataFam, geboorteDatum:new Date(dataDob), email:dataEmail,aantalKinderen:dataAk};
                personen.push(p)
            }
            //vergeet input niet te refreshen ;)
            refreshSeleciton();
    
        }else{
            alert("Check console voor de missende inputs. :(")
        }
    }
    function refreshSeleciton(){
    
        emptySelection()
        fillSelection()
    }
    
    function validate() {
        let okay = true
        $("input").each(function (e) { // In theorie, Kan het nog simpeler...
            var element = $(this);
            if (element.val() == "") {
                console.log("Box=" + element.attr("id") + " Is empty")
                okay = false
            }
        });
        return okay
    }
    function clearInput() {
        
        $("#index").text("");
        $('#inputForm').find('input').val('');
    }

});
