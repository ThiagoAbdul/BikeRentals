const amqp = require('amqplib');
const LogService = require('../services/logService');

const RABBITMQ_URL = process.env.QUEUE_URL

const QUEUE_NAME = 'logs';

async function consumeLogs() {
  try {
    
    const connection = await amqp.connect(RABBITMQ_URL);

    
    const channel = await connection.createChannel();

    
    await channel.assertQueue(QUEUE_NAME, {
      durable: true, 
    });

    console.log(`Waiting for messages in ${QUEUE_NAME}...`);

    
    channel.consume(QUEUE_NAME, async (msg) => {
      if (msg !== null) {

        const body = msg.content.toString()
        console.log("Received:", body);
        const { message, level, service, traceId, userId } = JSON.parse(body).message;
        const log = { message, level, timestamp: new Date(), service, traceId, userId };

        if(!message || !level){
            channel.ack(msg);
            return
        }

        try {
            await LogService.addLog(log);
            channel.ack(msg);
        } catch (error) {
            console.log(error)
        }

      }
    });

  } catch (error) {
    console.error("Error while consuming from RabbitMQ:", error);
  }
}

exports.consumeLogs = consumeLogs;
