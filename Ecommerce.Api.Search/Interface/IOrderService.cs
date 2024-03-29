﻿using Ecommerce.Api.Search.Model;

namespace Ecommerce.Api.Search.Interface
{
    public interface IOrdersService
    {
        Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)>
            GetOrdersAsync(int customerID);
    }
}
