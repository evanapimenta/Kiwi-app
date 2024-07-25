using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiwi_app
{
    internal class Genre(string genreName)
    {
        public string Name { get; set; } = genreName ?? throw new ArgumentNullException(nameof(genreName));
    }
}
