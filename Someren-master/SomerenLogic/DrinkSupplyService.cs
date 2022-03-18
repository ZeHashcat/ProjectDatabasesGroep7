using SomerenDAL;
using SomerenModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenLogic
{
    public class DrinkSupplyService
    {
        DrinkSupplyDao DrinkSupplydb;

        public DrinkSupplyService()
        {
            DrinkSupplydb = new DrinkSupplyDao();
        }

        public List<Drink> GetDrinkSupply()
        {
            List<Drink> drinkSupply = DrinkSupplydb.GetAllDrinkSupply();
            return drinkSupply;
        }

        //Baton passing method between UI and DAL.
        public int GetPersonId(string argument)
        {
            //Makes sure that a valid PersonID( associated with a StudentID is retrieved, prolly only really usefull when connection breaks.
            int personId = 0;
            personId = DrinkSupplydb.GetPersonId(argument);
            if (personId == 0)
            {
                throw new Exception("Could not find this person!");
            }
            return personId;
        }

        public List<int> GetTransactionIds(int argument)
        {
            DrinkSupplyDao drinkSupply = new DrinkSupplyDao();
            List<int> transactions = drinkSupply.GetTransactionIds(argument);
            return transactions;
        }

        public void WriteVoucher(string argument, string argumentTwo)
        {
            DrinkSupplyDao drinkSupply = new DrinkSupplyDao();
            drinkSupply.WriteVoucher(argument, argumentTwo);
        }

        //Baton passing method between UI and DAL.
        public void WriteTransaction(string argument, int argumentTwo)
        {
            DrinkSupplyDao drinkSupplyDao = new DrinkSupplyDao();
            DrinkSupplydb.WriteTransaction(argument, argumentTwo.ToString());
        }
    }
}
