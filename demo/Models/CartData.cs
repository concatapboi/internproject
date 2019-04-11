using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo.Models
{
    public class CartData
    {
        public ArrayList arrCart = new ArrayList();
        public int amounts = 0;
        public int cost = 0;

        public CartData()
        {
            this.amounts = 0;
            this.cost = 0;
        }
        public CartData(CartData oldCart)
        {
            if (oldCart != null)
            {
                this.arrCart = oldCart.arrCart;
                this.amounts = oldCart.amounts;
                this.cost = oldCart.cost;
            }
        }
        public int getID(tbl_Product p)
        {
            int num = -1;
            if (this.arrCart.Count != 0)
            {
                for (int i = 0; i < this.arrCart.Count; i++)
                {
                    Cart item = (Cart)this.arrCart[i];
                    if (item.item.id == p.id)
                    {
                        num = i;
                        break;
                    }
                }
            }
            return num;
        }

        public void add(tbl_Product item)
        {
            Cart c = new Cart(item);
            int num = getID(item);
            if (this.arrCart.Count==0)
            {
                this.arrCart.Add(c);
                this.amounts ++;
                this.cost += c.cost;
            }
            else
            {
                if (num < 0)
                {
                    this.arrCart.Add(c);
                    this.amounts ++;
                    this.cost += c.cost;
                }
                else
                {
                    c.amounts++;
                    this.arrCart[num] = c ;
                    this.amounts ++;
                    this.cost += c.cost;
                }
            }
        }
        public void addWithNum(tbl_Product item, int no)
        {
            Cart c = new Cart(item,no);
            int num = getID(item);
            if (this.arrCart.Count == 0)
            {
                this.arrCart.Add(c);
                this.amounts+=no;
                this.cost += c.cost;
            }
            else
            {
                if (num < 0)
                {
                    this.arrCart.Add(c);
                    this.amounts+=no;
                    this.cost += c.cost;
                }
                else
                {
                    c.amounts++;
                    this.arrCart[num] = c;
                    this.amounts+=no;
                    this.cost += c.cost;
                }
            }
        }
        public void remove(tbl_Product item, int no)
        {
            int num = getID(item);
            Cart cart = (Cart)this.arrCart[num];
            this.amounts -= no;
            this.cost -= cart.cost*no;
            this.arrCart.RemoveAt(num);
        }
    }
}