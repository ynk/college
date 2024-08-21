const http = require("http");
const filemodule = require('./file.js')

filemodule.Write("file.txt","Yannick");

const readData = (filemodule.Read("file.txt"))
const person = {Voornaam: readData,Achternaam: "lol",Leeftijd: 21,Woonplaats:"Gent"}
const server = http.createServer(function (req, res) {
    if (req.url == "/") {
        res.writeHead(200, { "Content-Type": "text/html" });
        res.write("<html><body><p>Home page</p></body></html>");
        res.end();
    }
    else if (req.url == "/profile") {
        res.writeHead(200, { "Content-Type": "text/html" });
        res.write("<html><body><p>Profile page</p></body></html>");
        res.end();
    }
    else if (req.url == "/data") {
        res.writeHead(200, { "Content-Type": "text/html" });
        res.write('<html><body>' + JSON.stringify(person) + '</body></html>');
        res.end();
    }
});
server.listen(5000);
console.log("ok");