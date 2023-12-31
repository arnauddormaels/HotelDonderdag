﻿using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Interfaces
{
    public interface IMembersRepository
    {
        List<Member> GetMembers(int customerId);
        int AddMember(int customerId, Member member);
        void UpdateMember(int customerId, Member oldMember, Member newMember);
        void DeleteMember(int customerId, Member member);
    }
}
