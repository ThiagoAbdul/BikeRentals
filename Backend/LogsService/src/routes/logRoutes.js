const express = require('express');
const router = express.Router();
const logController = require('../controllers/logController');

router.get('/logs', logController.getLogs);
router.post('/logs', logController.createLog);

module.exports = router;