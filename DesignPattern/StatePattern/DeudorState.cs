using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.StatePattern
{
    public class DeudorState : IState
    {
        public void Action(CustomerContext customerContext, decimal amount)
        {
            Console.WriteLine("No puedes comprar");
        }
    }
}
