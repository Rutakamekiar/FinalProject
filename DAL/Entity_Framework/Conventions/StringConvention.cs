using System.Data.Entity.ModelConfiguration.Conventions;

namespace DAL.Entity_Framework.Conventions
{
    public sealed class StringConvention : Convention
    {
        public StringConvention()
        {
            Properties<string>().Configure(s => s.HasMaxLength(100));
        }
    }
}
