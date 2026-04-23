using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBot.Servicess
{
    public interface IBotService
    {
        Task StartBotAsync();
        Task StopBotAsync();
    }
}
