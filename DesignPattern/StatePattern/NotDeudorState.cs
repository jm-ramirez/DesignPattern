using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.StatePattern
{
    public class NotDeudorState : IState
    {
        public void Action(CustomerContext customerContext, decimal amount)
        {
            if (amount <= customerContext.Residue)
            {
                customerContext.Discount(amount);
                Console.WriteLine($"Solicitud permitida, gasta {amount} y le queda de saldo {customerContext.Residue}");
                if (customerContext.Residue <= 0)
                    customerContext.SetState(new DeudorState());
            }
            else
            {
                Console.WriteLine($"No ajustas lo solicitado, ya que tienes {customerContext.Residue} y quieres gastar {amount}");
            }
        }
    }
}
