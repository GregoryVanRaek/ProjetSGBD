﻿using LocationVoiture.dal.Entities;

namespace LocationVoiture.bll.Services;

public interface IClientService : IService<int, Client>
{
    public Client? GetOneByName(string name);
}