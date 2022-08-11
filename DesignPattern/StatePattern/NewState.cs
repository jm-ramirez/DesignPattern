using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.StatePattern
{
    public class NewState : IState
    {
        public void Action(CustomerContext customerContext, decimal amount)
        {
            Console.WriteLine($"Se le pone dinero a su saldo {amount}");
            customerContext.Residue = amount;
            customerContext.SetState(new NotDeudorState());
        }
    }
}
