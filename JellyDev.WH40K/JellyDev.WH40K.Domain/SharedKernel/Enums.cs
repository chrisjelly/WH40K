using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Domain.SharedKernel
{
    public enum Phase
    {
        Unknown = 0,
        Command = 1,
        Movement = 2,
        Psychic = 3,
        Shooting = 4,
        Charge = 5,
        Fight = 6,
        Morale = 7
    }
}
