using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accessories.Interfaces
{
	public interface IMapper<F,D>
	{
		D Map(F type);
	}
}
