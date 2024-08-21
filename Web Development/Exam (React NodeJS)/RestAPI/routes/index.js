
const express = require('express');
const router = express.Router();
const OrderController = require("../controllers/orders");
const ProductController = require("../controllers/products");

const { check, validationResult } = require("express-validator")


router.get('/', function (req, res, next) {
  res.send("<p>REST API</p>");
});

router.get("/products", ProductController.getProducts)



router.post('/order',[
check("user.firstName", "First name must at least have something in it").not().isEmpty(),
check("user.lastName", "last name must at least have something in it").not().isEmpty(),
check("user.street", "street must at least have something in it").not().isEmpty(),
check("user.number", "number must at least have something in it").not().isEmpty(),
check("user.postalCode", "postal code must at least have something in it").not().isEmpty(),
check("user.city", "city must at least have something in it").not().isEmpty(),
check("user.telephone", "city must at least have something in it").not().isEmpty(),
check("user.totalPrice", "totalprice is empty").not().isEmpty(),
check("user.email", "Email is not valid").isEmail().normalizeEmail().not().isEmpty(),
check("products", "Products are empty").not().isEmpty()

], (req, res) => {
  const error = validationResult(req)
  if (!error.isEmpty()) {
    console.log("request: "+JSON.stringify(req.body))
    console.log(error.array())
    return res.status(422).json({ error: error.array() })
  }else{
    OrderController.postOrder(req.body, res)
  }
  
});

router.get('/order/:id([0-9]{1,10})', OrderController.getConfirmationById);


router.get('/orders', OrderController.fetchAllOrders);


module.exports = router;
