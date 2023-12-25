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


        public int AddMember(int customerId, string memberName, DateTime birthDate)
        {
            
            Member member = new Member(memberName, DateOnly.FromDateTime(birthDate));
           return _memberRepository.AddMember(customerId, member);
        }

        public void UpdateMember(int customerId, string oldName, DateTime oldBirthDate, string name, DateTime birthDate)
        {
            DateOnly dateOnly = DateOnly.FromDateTime(birthDate);     //Gaat dateTime omzetten naar DateOnly
            DateOnly oldDateOnly = DateOnly.FromDateTime(oldBirthDate);     //Gaat dateTime omzetten naar DateOnly

            //oldMember is degene die we zullen aanpassen
            Member oldMember = new Member(oldName, oldDateOnly);
            //Newember is degene die oldMember heeft vervangen
            Member newMember = new Member(name, dateOnly);

            _memberRepository.UpdateMember(customerId, oldMember, newMember);

        }

        public void DeleteMember(int customerId, string name, DateTime birthDate)
        {
            DateOnly dateOnly = DateOnly.FromDateTime(birthDate);
            Member member = new Member(name, dateOnly);

            _memberRepository.DeleteMember(customerId,member);
        }
    }
}
