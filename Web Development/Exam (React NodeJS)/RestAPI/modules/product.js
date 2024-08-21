const db = require("../config/db");
const { post } = require("../routes");


const product = {
    fetchAllProducts: () => {
        return new Promise((resolve, reject) =>{
            db.query("SELECT * from product", (err,products, fields) =>{
                if(err) reject(err);
                resolve(products);
            })
        })
    },
}

module.exports = product;