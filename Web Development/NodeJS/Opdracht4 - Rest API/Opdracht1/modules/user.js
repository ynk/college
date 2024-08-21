const db = require("../config/db");
const { post } = require("../routes");


const User = {
    fetchAllUsers: () => {
        return new Promise((resolve, reject) =>{
            db.query("SELECT * from users", (err,users, fields) =>{
                if(err) reject(err);
                resolve(users);
            })
        })
    },
    fetchUserById: (id) => {
        return new Promise((resolve, reject) =>{
            db.query(`SELECT * from users where id = ${id}`, (err,users, fields) =>{
                if(err) reject(err);
                resolve(users);
            })
        })
    },
    postNewUser:(user)=>{
        return new Promise((resolve, reject) =>{
            db.query(`INSERT INTO users(firstName, lastName,email,password) Values (?,?,?,?)`,[user.firstName, user.lastName, user.email,user.password],(err,user, fields) =>{
                if(err) reject(err);
                resolve(user);
            })
        })
    },
    getUser:(user)=>{
        return new Promise((resolve, reject) =>{
      
            db.query(`SELECT * FROM users WHERE email = ?`,[user],(err,user, fields) =>{
                if(err) reject(err);
                resolve(user);
            })
        })
    },
}

module.exports = User;