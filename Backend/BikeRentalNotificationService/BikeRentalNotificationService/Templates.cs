﻿namespace BikeRentalNotificationService;

public class Templates
{
    public static readonly string HtmlNotification = "<!doctypehtml><html lang=pt-br><meta charset=UTF-8><meta content=\"IE=edge\"http-equiv=X-UA-Compatible><meta content=\"width=device-width,initial-scale=1\"name=viewport><title>Confirmação de Alocação</title><body style=background-color:#f0f4f8;display:flex;justify-content:center;align-items:flex-start;padding:20px;font-family:Arial,sans-serif><div style=\"max-width:500px;width:100%;background-color:#fff;padding:20px;border-radius:10px;box-shadow:0 4px 15px rgba(0,0,0,.1);text-align:center;margin:auto\"><h1 style=color:#333;margin-bottom:20px;font-size:26px>BikeRentals</h1><div style=\"background-color:#f9f9f9;padding:20px;border-radius:8px;box-shadow:0 2px 6px rgba(0,0,0,.05)\"><h2 style=color:#4caf50;margin-top:20px;font-size:22px>Confirmação de Alocação</h2><p style=font-size:18px;color:#333;margin-top:15px>Olá <span style=font-weight:700>[fullName]</span>, muito obrigado pela sua escolha!<p style=font-size:16px;color:#666;margin-top:10px>A alocação da bike foi aprovada com sucesso.<div style=\"margin-top:20px;background-color:#e7f3fe;padding:15px;border-radius:8px;border:1px solid #b3d7ff\"><p style=font-size:16px;color:#333;margin:0>Código de locação:<p style=\"font-size:24px;color:#007bff;font-weight:700;margin:5px 0\">[rentalCode]</div></div><p style=font-size:14px;color:#565656;margin-top:20px>Caso você já tenha recebido um email de confirmação, por favor, desconsidere esta mensagem.</div><style>@media (max-width:768px){body{padding:10px}h1{font-size:22px}h2{font-size:18px}p{font-size:14px}.code{font-size:20px}}@media (max-width:480px){h1{font-size:18px}h2{font-size:16px}p{font-size:12px}.code{font-size:18px}}</style>";
}
