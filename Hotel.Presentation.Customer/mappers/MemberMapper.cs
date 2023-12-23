using Hotel.Domain.Model;
using Hotel.Presentation.Customer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Customer.mappers
{
    public static class MemberMapper
    {
        public static MemberUI MapToMemberUI(Member member)
        {
            return new MemberUI(member.Id, member.Name, member.Birthday.ToString());
        }
    }
}
