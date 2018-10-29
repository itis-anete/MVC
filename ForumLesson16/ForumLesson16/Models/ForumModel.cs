using Microsoft.AspNetCore.Mvc;

namespace ForumLesson16
{
    public class ForumModel : ForumValue
    {
        [ForumValidValue(typeof(int), true)]
        public ForumValue Prop1 { get; set; }

        [ForumValidValue(typeof(string), 128)]
        public ForumValue Prop2 { get; set; }

        public override string ToString() => $"Prop1: {Prop1}\n\rProp2: {Prop2}";
    }
}