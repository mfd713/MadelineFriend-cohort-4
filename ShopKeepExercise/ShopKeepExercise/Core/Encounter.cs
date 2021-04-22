using System;
using System.Collections.Generic;
using System.Text;

namespace ShopKeepExercise
{
    public abstract class Encounter
    {
        public abstract void InteractWithShopkeep(Shopkeeper protag);
        public abstract string NoItems { get; }

    }
}