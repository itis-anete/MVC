using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumLesson16
{
    public class ForumModel : ForumValue
    {
        [ForumValidValue(typeof(int), 1024, true)]
        public ForumValue Prop1 { get; set; }

        [ForumValidValue(typeof(string), 128, true)]
        public ForumValue Prop2 { get; set; }
    }
}