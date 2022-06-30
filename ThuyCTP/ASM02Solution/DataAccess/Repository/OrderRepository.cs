﻿using DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public IEnumerable<Order> GetOrders() => OrderDAO.Instance.GetOrderList();
        public Order GetOrderByID(int OrderID) => OrderDAO.Instance.GetOrderByID(OrderID);
        public void InsertOrder(Order Order) => OrderDAO.Instance.AddNew(Order);
        public void DeleteOrder(int OrderID) => OrderDAO.Instance.Remove(OrderID);
        public void UpdateOrder(Order Order) => OrderDAO.Instance.Update(Order);

      
    }
}
