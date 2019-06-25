﻿using Northwind.Repositories;

namespace Northwind.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customer { get; }
        IUserRepository User { get; }
    }
}
