<<<<<<<< HEAD:Domain/Entities/Security/Users.cs
﻿using Domain.Entities;
========
﻿using DataRepository;
>>>>>>>> 374eb4af2575c8659ad3d8e8e4de3e6f4efa6188:Domain/Entities/Schema/Security/Users.cs
using System;
using System.Collections.Generic;
using System.Text;

<<<<<<<< HEAD:Domain/Entities/Security/Users.cs
namespace Domain.DomainEntities.Security
{
    public class Users:Model
========
namespace Domain.Entities.Schema.Security
{
   public class Users:IRepository
>>>>>>>> 374eb4af2575c8659ad3d8e8e4de3e6f4efa6188:Domain/Entities/Schema/Security/Users.cs
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
