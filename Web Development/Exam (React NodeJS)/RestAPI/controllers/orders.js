const Order = require("../modules/order");


exports.postOrder = (req, res) => {
    Order.postOrder(req.user).then(order => {
        let orderid = order["insertId"]
        req.products.forEach(element => {
            Order.insertOrderToLine(orderid, element)
        });

        
        //res.redirect(301, `/order/${orderid}`) Kunnen ook redirecten maar als response gaan we gwn de geposte data laten zien

        res.json({
        
              "user": {...req.user,id:orderid},  // Id terug appended aan user omdat we het user object uit de body sturen
              "products": req.products,
          })


        
    }).catch(err => console.log(err))

}



exports.getConfirmationById = (req, res) => {

    Order.fetchCustomer(req.params.id).then(customer => {

        if (customer.length == 0) {
            console.log(customer)
            res.status(404).send('Not found');
            console.log(req.params.id)
        } else {
            Order.fetchOrdersByCustomerIdDetail(req.params.id).then(
                orders => {
                    console.log(customer)
                    console.log(orders)
                    // ait dus wat hier de bedoeling was, was dus om de confirm pagina `leuk` te maken
               
                    res.json({
                      // fun fact, als je dit response noemt dan retourneert hij niks
                        "user": customer[0], // We krijgen een data packet dus gaan we de eerste ding eruit halen
                        "products": orders,
                    })

                }
            ).catch(err => res.send(err))
        }
    }).catch(err => res.send(err))
}

exports.fetchAllOrders = (req, res) => {
    Order.fetchAllOrders().then(orders => {
        if (orders.length == 0) {
            res.status(404).send('Orders not found ');
        } else {
            res.json(orders)
        }


    }).catch(err => res.send(err))

}


