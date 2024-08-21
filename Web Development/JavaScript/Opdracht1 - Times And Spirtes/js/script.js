let update_interval = 2500
let score = 0;


const initialize = () => {
    window.addEventListener("resize", updateSize);
    updateSize();
    moveSprite(); //We willen eerst een sprite 
    interval = setInterval(moveSprite, update_interval)
    let sprite = document.getElementsByClassName("sprite")[0];
    sprite.addEventListener("click", function () { onClick(sprite) });



}
function onClick(elm) {
    atr = elm.getAttribute('src')
    if (atr == 'img/0.png') {
        console.log("boom!")
        document.getElementById("score").textContent = "Game over! U score was: " + score;
        alert("game over! U score was: " + score)
        window.location.href = 'index.html';
    } else {
        score++;
        //Ik veronderstel dat het spel moet doorlopen tot als er iemand op een bom klikt, anders is het gewoon if score == 5 dan alert spel afgelopen, i guess.
    }
    elm.setAttribute('src', "")
    document.getElementById("score").textContent = "Score: " + score;
    clearInterval(interval)
    moveSprite(); //We willen eerst een sprite 
    interval = setInterval(moveSprite, update_interval)

}

const updateSize = () => {
    let speelveld = document.getElementById("speelveld");
    speelveld.style.width = window.innerWidth + "px";
    speelveld.style.height = window.innerHeight + "px";
}
const moveSprite = () => {
    let sprite = document.getElementsByClassName("sprite")[0];
    let speelveld = document.getElementById("speelveld");
    let maxLeft = speelveld.clientWidth - sprite.offsetWidth;
    let maxHeight = speelveld.clientHeight - sprite.offsetHeight;
    console.log("max: " + maxLeft + " height: " + maxHeight)

    let number = Math.floor(Math.random() * 4)
    sprite.setAttribute('src', "img/" + number + ".png");

    let left = Math.floor(Math.random() * maxLeft);
    let top = Math.floor(Math.random() * maxHeight);

    // verplaats de sprite
    sprite.style.left = left + "px";
    sprite.style.top = top + "px";
}

window.addEventListener("load", initialize);