
$(document).ready(function () {
    $("input[type='button']").click(function () {
        let radioValue = $("input[name='options']:checked").val();
        let textBox = $("#valueTextbox").val();
        switch (radioValue) {
            case "Achtergrond": {
                $('#canvas').css("background-color", textBox);
            }
                break;
            case "Voorgrond": {
                //ik snap niet echt wat voorgrond is maar we zullen dan de text kleur maar veranderen zeker?
                if ($('#text').length) {
                    $('#text').css("color", textBox);
                } else {
                    alert("Je hebt nog geen text aangemaakt")
                }
            }
                break;
            case "Tekst": {
                // check of er al text is
                // Ik ga er vanuit dat er niet meer dan 1 text element in het div element steekt, vandaar die stelin, anders is de code hieronder nutteloos
                if ($('#text').length) {
                    $('#text').text(textBox);
                } else {
                    $('#canvas').append("<p id='text'>" + textBox + "</p>");
                }
            }
                break;
            case "Hoogte": {
                $('#canvas').height(textBox);
            }
                break;
            case "Breedte": {
                $('#canvas').width(textBox);
            }
                break;
        }
    });
});
