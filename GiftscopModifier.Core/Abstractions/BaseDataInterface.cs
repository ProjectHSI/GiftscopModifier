using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftscopModifier.Core.Abstractions
{
	internal interface IBaseDataInterface
	{
		public void fillInData(List<byte> data);

		public List<byte> rebuildData();
	}
}
