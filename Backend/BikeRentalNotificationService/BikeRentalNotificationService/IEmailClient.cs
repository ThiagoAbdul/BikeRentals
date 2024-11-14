using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRentalNotificationService;

public interface IEmailClient
{
    Task SendAsync(string sendFrom, string sendTo, string subject, string content);
}
