﻿using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Entity;
using Microsoft.EntityFrameworkCore;

namespace CardsServer.DAL.Repository
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task EditUser(UserEntity user, CancellationToken cancellationToken)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        //public async Task<UserEntity> GetListUser(ICollection<int> UserIds, CancellationToken cancellationToken)
        //{

        //}

        public async Task<UserEntity?> GetUser(int userId, CancellationToken cancellationToken)
        {
            UserEntity? user = await _context
                .Users
                .Include(x => x.UserModules)
                .Include(x => x.Avatar)
                .Include(x => x.RefreshTokens)
                .Include(x => x.Role)
                .Include(x => x.Avatar)
                .FirstOrDefaultAsync(x => x.Id == userId);

            return user;
        }

        public async Task<UserEntity?> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            UserEntity? user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }

        public async Task<UserEntity?> GetUserByUserName(string userName, CancellationToken cancellationToken)
        {
            UserEntity? user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);

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
