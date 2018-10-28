using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ForumLesson16
{
    public class ForumValidValue : ValidationAttribute
    {
        Type[] _Types;
        bool State;

        public ForumValidValue(Type[] types)
        {
            _Types = types;
        }
        public override bool IsValid(object value)
        {
            if (_Types.Contains(value.GetType()))
            {
                State = true;
                return true;
            }
            State = false;
            return true;
        }
    }
}