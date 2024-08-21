const User = require("../modules/user");
const bcrypt = require("bcrypt")

exports.getAllUsers = (req, res) => {
    User.fetchAllUsers().then(users => res.json(users)).catch(err => res.send(err))
    
}

exports.getUserById = (req, res) => {
    User.fetchUserById(req.params.id).then(user => res.json(user)).catch(err => res.send(err))
}
exports.postNewUser = (req,res) => {
    User.postNewUser(req).then(user => res.json(user)).catch(err => res.send(err))
}
exports.getUser = (req,res) => {
    User.getUser(req.body.email).then((user) =>{
        console.log("git")
        console.log(user[0].password)
        bcrypt.compare(req.body.password, user[0].password).then((result)=>{
            console.log("git1")
            if(result){
                res.send("Password correct!")
            }else{
                res.send("Password incorrect!")
            }
        })
    }).catch(err => res.send(err))
}
