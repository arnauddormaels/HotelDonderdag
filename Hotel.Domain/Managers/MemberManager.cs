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
        private readonly IMembersRepository _memberRepository;
        public MemberManager(IMembersRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }
        public List<Member> GetMembers(int customerId) {
            return _memberRepository.GetMembers(customerId);
        }


        public void AddMember(int customerId, string memberName, DateTime birthDate)
        {
            //TODO: Add member
            //Mogen managers elkaar kennen?
            Member member = new Member(memberName, DateOnly.FromDateTime(birthDate));
            _memberRepository.AddMember(customerId, member);
        }

        public void UpdateMember(int customerId, int memberId, string name, DateTime birthDate)
        {
            DateOnly dateOnly = DateOnly.FromDateTime(birthDate);     //Gaat dateTime omzetten naar DateOnly

            Member member = new Member(memberId, name, dateOnly);

            _memberRepository.UpdateMember(customerId, member);

        }

        public void DeleteMember(int customerId, int memberId, string name, DateTime birthDate)
        {
            DateOnly dateOnly = DateOnly.FromDateTime(birthDate);
            Member member = new Member(memberId, name, dateOnly);

            _memberRepository.DeleteMember(customerId,member);
        }
    }
}
