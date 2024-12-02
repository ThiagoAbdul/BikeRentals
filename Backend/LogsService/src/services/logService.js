const LogModel = require('../models/logModel');

const LogService = {
    getLogs: async () => {
        return new Promise((resolve, reject) => {
            LogModel.getAllLogs((err, results) => {
                if (err) reject(err);
                else resolve(results);
            });
        });
    },

    addLog: async (log) => {
        return new Promise((resolve, reject) => {
            LogModel.createLog(log, (err, results) => {
                if (err) reject(err);
                else resolve(results);
            });
        });
    }
};

module.exports = LogService;