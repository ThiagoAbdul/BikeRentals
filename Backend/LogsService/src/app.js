const express = require('express');
const logRoutes = require('./routes/logRoutes');
const consumer = require("./messaging/logConsumer")
require('dotenv').config();

const app = express();
app.use(express.json());

app.use('/api', logRoutes);

app.get('/', (req, res) => {
    res.send('API is running!');
});

consumer.consumeLogs()

module.exports = app;
