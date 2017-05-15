const express = require('express');
const cookieParser = require('cookie-parser');
const randomstring = require("randomstring");
const request = require('request');
const app = express();

app.use(cookieParser());

app.get('/', (req, res, next) => {
    // let id = randomstring.generate();
    // res.cookie('id', id);
    next();
});

app.get('/scramble/:type', (req, res) => {
    let url = `http://127.0.0.1:2014/scramble/.txt?=${req.params.type}`;
    request(url, (error, response, body) => {
        res.set('Access-Control-Allow-Origin', '*');
        res.send(body);
    });
});

app.use(express.static('build'));

app.listen(5000);