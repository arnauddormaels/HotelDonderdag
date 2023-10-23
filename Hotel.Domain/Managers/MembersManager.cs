using Hotel.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Managers
{
    public class MembersManager
    {
        public IMembersRepository MemberRepository { get; }
        public MembersManager(IMembersRepository memberRepository)
        {
            MemberRepository = memberRepository;
        }

    }
}
