'use strict';
const port = process.env.PORT || 3000;
const express = require('express');
const db = require('./db.js');
const cors = require('cors');
const app = express();

app.use(cors());
app.get('/api/top/confirmed', db.getConfirmedCases)
app.listen(port, () => {
    console.log(`Listening to http://localhost:${port}`)
})
