'use strict';
const port = process.env.PORT || 3000;
const express = require('express');
const db = require('./db.js');
const app = express();

app.get('/api/top/confirmed', db.getConfirmedCases)

app.listen(port, () => {
    console.log(`Listening to http://localhost:${port}`)
})
