namespace server.Attributes 
{
    public class FuckAttribute : System.Attribute 
    {
        public string Name;

        public FuckAttribute(string name)
        {
            Name = name;
        }
    }
}