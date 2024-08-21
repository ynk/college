require('dotenv').config()
const mysql = require('mysql');

const pool = mysql.createPool({
  connectionLimit: 10,
  host: process.env.dbHost,
  port: process.env.dbPort,
  user: process.env.dbUsername,
  password: process.env.dbPassword,
  database: process.env.db,
});

module.exports = pool;