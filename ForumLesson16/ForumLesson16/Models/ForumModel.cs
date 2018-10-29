namespace ForumLesson16
{
    public class ForumModel : ForumValue
    {
        [ForumValidValue(typeof(int), true)]
        public ForumValue Prop1 { get; set; }

        [ForumValidValue(typeof(string), 128)]
        public ForumValue Prop2 { get; set; }
    }
}