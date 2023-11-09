using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Managers
{
    public class MemberManager
    {
        public IMembersRepository MemberRepository { get; }
        public MemberManager(IMembersRepository memberRepository)
        {
            MemberRepository = memberRepository;
        }

        public void AddMember(int customerId, string memberName, DateTime birthDate)
        {
            //TODO: Add member
            //Mogen managers elkaar kennen?
            Member member = new Member(memberName, DateOnly.FromDateTime(birthDate));
            MemberRepository.AddMember(customerId, member);
        }
    }
}
