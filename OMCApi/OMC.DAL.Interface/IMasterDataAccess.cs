﻿using OMC.Models;
using System.Collections.Generic;

namespace OMC.DAL.Interface
{
    public interface IMasterDataAccess
    {
        List<Role> GetRoles(bool? isActive, string roleName);
    }
}
