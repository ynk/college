let express = require('express');
let app = express();
let bodyParser = require('body-parser')
let router = express.Router();

router.use((req,res,next) => {
    console.log("Tine: ", new Date().toISOString(), "\nIP:", req.ip);
    next();
    });
app.use(bodyParser.json());
app.get('/',router, (req,res) => {
    res.send("home page");
});
app.get('/test',router, (req,res) => {
    res.send("<p>test</p>");
});
app.post('/test',router, (req,res) => {
    req.body.server=true;
    res.json(body)
});
app.get('/test/:id([0-9]{3})',router, (req,res) => {
    res.send(`<p>ID: ${req.params.id}</p>`);
});
app.get('/search',router, (req,res) => {
    res.send(`<p>SEARCHED: ${req.query.q}</p>`);
  
});
app.use('/time', function (req, res, next) {
    req.requestTime = new Date().toISOString();
    res.send(req.requestTime)
   
  })
app.all('*', router,(req,res) => {
    res.status(404).send("<p>NIET GEVONDEN</p>");
});



app.listen(3000);