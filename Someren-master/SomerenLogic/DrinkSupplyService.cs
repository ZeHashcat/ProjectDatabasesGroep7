﻿using SomerenDAL;
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
    }
}
