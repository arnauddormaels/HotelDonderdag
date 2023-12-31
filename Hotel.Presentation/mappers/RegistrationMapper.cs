﻿using Hotel.Domain.Model;
using Hotel.Presentation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.mappers
{
    public static class RegistrationMapper
    {
        public static RegistrationUI MapToRegistrationUI(Registration registration)
        {
            Dictionary<int, MemberUI> membersUI = new Dictionary<int, MemberUI>();
            if (registration.Members.Count != null)
            {
                foreach (var keyValuePair in registration.Members)
                {
                    membersUI.Add(keyValuePair.Key, MemberMapper.MapToMemberUI(keyValuePair.Value)); 
                }
            }
            else
            {
                membersUI = new Dictionary<int, MemberUI>();
            }

            EventUI eventUI = EventMapper.MapToEventUI(registration.Event);
            return new RegistrationUI(registration.Id, membersUI, eventUI, registration.CalculateTotalPrice());
        }

    }
}
