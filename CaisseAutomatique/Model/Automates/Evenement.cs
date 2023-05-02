using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Automates
{
    internal enum Evenement
    {
        SCAN,
        PAYER,
        RESET,
        AJOUTER_BALANCE,
        RETIRER_BALANCE
    }
}
