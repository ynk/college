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
        i.text(pos)
        achternaam.val(index.familienaam)
        voornaam.val(index.voornaam)
        dob.val(index.geboorteDatum.toISOString().substr(0, 10))
        email.val(index.email)
        ak.val(index.aantalKinderen)
    })
    function fillSelection(){
        jQuery.each(personen, function (i, val) {
            personenSelection.append($('<option>').val(i).text(val.voornaam + " "
                + val.familienaam))
        });
    }
    function emptySelection(){
        personenSelection.empty();
    }
    
    function bewaar() {
        let index = i.text()
        console.log("index:"+index)
        if(validate()){
            if(index){
                let object = personen[index]
                object.voornaam = voornaam.val()
                object.familienaam = achternaam.val()
                object.geboorteDatum = new Date(dob.val())
                object.email = email.val()
                object.aantalKinderen = ak.val()
                console.log("updated!")
            }else{
                //create new
                console.log("Added to array!")
                index = personen.length
                let p = {voornaam:voornaam.val(), familienaam:achternaam.val(), geboorteDatum:new Date(dob.val()), email:email.val(),aantalKinderen:ak.val()};
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
