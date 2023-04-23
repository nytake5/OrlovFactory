﻿using Entities;

namespace BLL.Interfaces;

public interface IUserLogic
{
    Task AddNewUser(User user);
    Task<bool> LoginUser(User user);
    Task<bool> TokenizeUser(string username, Guid token);
    Task<User> GetUserByLogin(string login, long chatId);
    Task<User> GetUserByChatId(long chatId);
}