﻿using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Entity;
using CardsServer.BLL.Infrastructure.Auth;
using Microsoft.EntityFrameworkCore;

namespace CardsServer.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<UserEntity?> GetUser(int userId, CancellationToken cancellationToken)
        {
            UserEntity? user = await _context.Users.FindAsync(userId, cancellationToken);

            return user;
        }

        public async Task<UserEntity?> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            UserEntity? user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }

        //public async Task<HashSet<Pe>> GetUserPermissions(int id)
        //{
        //    List<RoleEntity?> roles = await _context.Users
        //        .AsNoTracking()
        //        .Include(x => x.Role)
        //        .ThenInclude(x => x.Permissions)
        //        .Where(x => x.Id == id)
        //        .Select(x => x.Role)
        //        .ToListAsync();

        //    return roles
        //        .SelectMany(x => x.Permissions)
        //        .Select(d => d.(Permission)d.Id)
        //        .ToHashSet();
        //}
}
    }
