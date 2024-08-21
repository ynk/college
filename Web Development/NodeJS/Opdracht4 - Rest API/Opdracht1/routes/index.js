const e = require('express');
const express = require('express');
const router = express.Router();
const UserController = require("../controllers/users");
const User = require('../modules/user');
const bcrypt = require("bcrypt")
const saltrounds = 10
const { check, validationResult } = require("express-validator")


router.get('/', function (req, res, next) {
  res.send("<p>Home</p>");
});



router.post('/users',[check("password", "password moet 6 characters lang zijn.").isLength({ min: 6 }),check("email", "Email is not valid").isEmail().normalizeEmail().not().isEmpty(),check("firstName", "First name cannot be empty").not().isEmpty(),check("lastName", "Last name cannot be empty").not().isEmpty()],(req,res) =>{
  const error = validationResult(req)
  if (!error.isEmpty()) {
    res.send(error)
  } else {
    bcrypt.hash(req.body.password, saltrounds).then((hash) => {
      const user = {
        firstName: req.body.firstName,
        lastName : req.body.lastName,
        email: req.body.email,
        password : hash
      }
      UserController.postNewUser(user,res)
    })
  }
});

router.post("/users/login", UserController.getUser)

router.get("/users", UserController.getAllUsers);

router.get('/user/:id', UserController.getUserById);


module.exports = router;
