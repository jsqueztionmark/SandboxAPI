using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Business.Mapping
{
    public class EntityMappingException : Exception
    {
        public EntityMappingException(string msg, Exception baseException)
            : base(msg, baseException) {}
    }
}
