using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Game_BusinessLogic_Contracts.Interfaces
{
    public interface IPlayer
    {
        string PlayerID { get; set; }
        string Color { get; }

        string GetName();
    }
}
