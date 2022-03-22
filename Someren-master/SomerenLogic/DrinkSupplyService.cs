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

        //Baton passing method between UI and DAL.
        public List<Drink> GetAllDrinkSupply()
        {
            List<Drink> drinkSupply = DrinkSupplydb.GetAllDrinkSupply();
            return drinkSupply;
        }
      
        //Baton passing method between UI and DAL.
        public List<Drink> GetDrinkSupply()
        {
            List<Drink> drinkSupply = DrinkSupplydb.GetDrinkSupply();
            return drinkSupply;
        }
      
        //Baton passing method between UI and DAL.
        public void AddDrink(Drink drink)
        {
            DrinkSupplydb.AddDrink(drink);
        }
      
        //Baton passing method between UI and DAL.
        public int GetHighestDrinkID()
        {
            int highestDrinkID = DrinkSupplydb.GetHighestDrinkID();
            return highestDrinkID;
        }
      
        //Baton passing method between UI and DAL.
        public void UpdateDrink(string originalDrinkName, string newDrinkName, double salePrice, int quantity)
        {
            DrinkSupplydb.UpdateDrink(originalDrinkName, newDrinkName, salePrice, quantity);
        }

        //Baton passing method between UI and DAL.
        public void DeleteDrink(int drinkId)
        {
            DrinkSupplydb.DeleteDrink(drinkId);
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

        //Baton passing method between UI and DAL.
        public List<int> GetTransactionIds(int argument)
        {
            DrinkSupplyDao drinkSupply = new DrinkSupplyDao();
            List<int> transactions = drinkSupply.GetTransactionIds(argument);
            return transactions;
        }

        //Baton passing method between UI and DAL.
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
