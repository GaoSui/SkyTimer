const express = require('express');
const cookieParser = require('cookie-parser');
const randomstring = require("randomstring");
const app = express();

app.use(cookieParser());

app.get('/', (req, res, next) => {
    // let id = randomstring.generate();
    // res.cookie('id', id);
    next();
});

app.get('/scramble*', (req, res) => {
    res.redirect(`http://127.0.0.1:2014${req.originalUrl}`);
});

app.use(express.static('build'));

app.listen(3000);