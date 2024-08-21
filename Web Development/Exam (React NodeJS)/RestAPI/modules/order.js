const db = require("../config/db");
const { post } = require("../routes");


const Order = {
    postOrder: (user) => {
        return new Promise((resolve, reject) =>{
            db.query(`INSERT INTO web3.order(created, firstName,lastName,street,number,postalCode,city,telephone,email,totalPrice) Values (?,?,?,?,?,?,?,?,?,?)`,
            [new Date().toISOString(), user.firstName,user.lastName,user.street,user.number,user.postalCode,user.city,user.telephone,user.email,user.totalPrice],(err,order, fields) =>{ // TODO: add total price
                if(err) reject(err);
                resolve(order);

            })
        })
    },
    fetchAllOrders: () => {
        return new Promise((resolve, reject) =>{
            db.query(`select id from web3.order`, (err,orders, fields) =>{
                if(err) reject(err);
                resolve(orders);
            })
        })
    },
    fetchOrdersByCustomerId: (id) => {
        return new Promise((resolve, reject) =>{
            db.query(`select product.id, product.name, product.price,o.qty,o.price from web3.product inner join orderline o on product.id = o.productId where o.orderId = ${id}`, (err,users, fields) =>{
                if(err) reject(err);
                resolve(users);
            })
        })
    },
    fetchCustomer: (id) => {
        return new Promise((resolve, reject) =>{
            db.query(`select * from web3.order where id = ${id}`, (err,users, fields) =>{
                if(err) reject(err);
                
                resolve(users);
         
            })
        })
    },
    fetchOrdersByCustomerIdDetail: (id) => {
        return new Promise((resolve, reject) =>{
            db.query(`SELECT p.id as prodctId, p.name,p.description,p.price,qty from web3.orderline inner join product p on orderline.productId = p.id inner join web3.order on orderline.orderId = web3.order.id where orderline.orderId  = ${id}`, (err,users, fields) =>{
                if(err) reject(err);
                resolve(users);
            })
        })
    },

  
    insertOrderToLine: (orderid, product) => {
        return new Promise((resolve, reject) =>{
            db.query(`INSERT INTO web3.orderline(OrderId, productId,qty,price) Values (?,?,?,?)`,
            [orderid,product.id,product.quantity,product.price],(err,order, fields) =>{ 
                if(err) reject(err);
                resolve(order);
            })
        })
    },
}

module.exports = Order;