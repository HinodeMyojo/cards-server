﻿using CardsServer.BLL.Dto.Module;
using CardsServer.BLL.Entity;
using System.Linq.Expressions;

namespace CardsServer.BLL.Abstractions
{
    public interface IModuleRepository
    {
        Task AddModuleToUsed(UserEntity user, CancellationToken cancellationToken);
        Task<int> CreateModule(ModuleEntity entity, CancellationToken cancellationToken);
        Task<ModuleEntity?> GetModule(int id, CancellationToken cancellationToken);
        Task<ICollection<ModuleEntity>> GetModules(int userId, Expression<Func<ModuleEntity, bool>> expression, CancellationToken cancellationToken);
    }
}