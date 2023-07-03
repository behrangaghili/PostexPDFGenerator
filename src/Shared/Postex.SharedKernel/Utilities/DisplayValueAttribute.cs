namespace Postex.SharedKernel.Utilities
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DisplayValueAttribute : Attribute
    {
        public string Value { get; set; }
        public int Id { get; set; }
    }


}
