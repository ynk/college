const setup = () => {
	let btnVerhoog=document.getElementById("btnVerhoog");
	btnVerhoog.addEventListener("click", verhoog);
}

const verhoog = () => {
	let txtResult=document.getElementById("txtResult");
	if(typeof(Storage)=="undefined") {
	  txtResult.innerHTML="Sorry, uw browser ondersteunt geen web storage...";
	} else {
		let clickCount=localStorage.getItem("demo.clickCount");
		if (clickCount) {
			clickCount=parseInt(clickCount)+1;
		} else {
			clickCount=1;
			
		}
		localStorage.setItem("demo.clickCount", clickCount.toString());
		txtResult.innerHTML="De waarde van de teller is " + clickCount;
	}
}
window.addEventListener("load", setup);