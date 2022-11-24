using System;

namespace FileReaderLibrary
{
    public interface IConfiguration
    {
        string Name { get; set; }
        string Description { get; set; }
    }
    
    public class Configuration : IConfiguration
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Configuration()
        {
            Name = "";
            Description = "";
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (!(obj is Configuration config)) return false;
            return Name==config.Name && Description == config.Description;
        }

        public override int GetHashCode()
        {
            return (int) (Name.GetHashCode()*Math.Pow(2, Description.GetHashCode()));
        }

        public override string ToString()
        {
            return "{\n\t"+nameof(Name)+": "+Name+",\n\t"+nameof(Description)+": "+Description+",\r\n}";
        }
    }
}