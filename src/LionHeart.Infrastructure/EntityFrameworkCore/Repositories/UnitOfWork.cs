﻿using LionHeart.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace LionHeart.Infrastructure.EntityFrameworkCore.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private IDbContextTransaction? _transaction;

    public bool IsTransactionActive => _transaction is not null;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task BeginTransaction()
    {
        _transaction = await _dbContext.Database.BeginTransactionAsync();
    }
    public async Task Commit()
    {
        if (_transaction is null)
        {
            throw new NullReferenceException("Transaction has not been started");
        }
        await _transaction.CommitAsync();
        _transaction.Dispose();
        _transaction = null;
    }
    public async Task Rollback()
    {
        if (_transaction is null)
        {
            throw new NullReferenceException("Transaction has not been started");
        }

        await _transaction.RollbackAsync();
        _transaction.Dispose();
        _transaction = null;
    }
    public void Dispose()
    {
        _transaction?.Dispose();
        _dbContext.Dispose();
    }
}