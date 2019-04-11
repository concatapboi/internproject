using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo.Models
{
    public class Cart
    {
        public tbl_Product item = new tbl_Product();
        public int amounts = 0;
        public int cost = 0;

        public Cart()
        {
            this.item = null;
            this.amounts = 0;
            this.cost = 0;
        }
        public Cart(Cart oldCart)
        {
            if(oldCart!=null)
            {
                this.item = oldCart.item;
                this.amounts = oldCart.amounts;
                this.cost = oldCart.cost;
            }
        }
         public Cart(tbl_Product item)
        {
            this.amounts++;
            if (item.discount > 0) this.cost = item.discount; else this.cost = item.price;
            this.item = item;
        }
        public Cart(tbl_Product item, int num)
        {
            this.amounts+=num;
            if (item.discount > 0) this.cost = item.discount; else this.cost = item.price;
            this.item = item;
        }
    }
}