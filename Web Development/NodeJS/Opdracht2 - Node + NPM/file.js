const fs = require("fs");

const writefile=(fileName, data)=>{
    fs.writeFile(fileName, data,(err) =>
    {
        if(err) throw err;
        else console.log("ok write!");
    });
}
const readfile=(fileName)=>{
    fs.readFile(fileName, 'utf8',(err,data)=>{
        if(err) throw err;
        console.log(data);
    });
}
module.exports.Write=writefile;
module.exports.Read=readfile;