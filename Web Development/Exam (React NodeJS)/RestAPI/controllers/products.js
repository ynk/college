

const product = require("../modules/product");

exports.getProducts = (req, res) => {
    product.fetchAllProducts().then(products => res.json(products)).catch(err => res.send(err))
    
}

