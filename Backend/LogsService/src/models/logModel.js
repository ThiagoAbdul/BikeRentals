const db = require('../config/db');

const LogModel = {
    getAllLogs: (callback) => {
        const query = 'SELECT * FROM logs';
        db.query(query, callback);
    },

    createLog: (log, callback) => {
        const query = 'INSERT INTO logs (message, level, timestamp, service, traceId, userId ) VALUES (?, ?, ?, ?, ?, ?)';
        db.query(query, [log.message, log.level, log.timestamp, log.service, log.traceId, log.userId], callback);
    }
};

module.exports = LogModel;