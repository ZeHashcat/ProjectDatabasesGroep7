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

        public List<Drink> GetAllDrinkSupply()
        {
            List<Drink> drinkSupply = DrinkSupplydb.GetAllDrinkSupply();
            return drinkSupply;
        }
        public List<Drink> GetDrinkSupply()
        {
            List<Drink> drinkSupply = DrinkSupplydb.GetDrinkSupply();
            return drinkSupply;
        }
        public void AddDrink(Drink drink)
        {
            DrinkSupplydb.AddDrink(drink);
        }
        public int GetHighestDrinkID()
        {
            int highestDrinkID = DrinkSupplydb.GetHighestDrinkID();
            return highestDrinkID;
        }
        public void UpdateDrink(string originalDrinkName, string newDrinkName, double salePrice, int quantity)
        {
            DrinkSupplydb.UpdateDrink(originalDrinkName, newDrinkName, salePrice, quantity);
        }
        public void DeleteDrink(int drinkId)
        {
            DrinkSupplydb.DeleteDrink(drinkId);
        }
    }
}
