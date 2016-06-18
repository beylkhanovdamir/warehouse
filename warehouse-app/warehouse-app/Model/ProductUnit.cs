using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace warehouse_app.Model
{
	public enum ProductUnit
	{
		[EnumMember(Value = "Коробка")]
		Box,
		[EnumMember(Value = "Килограмм")]
		Kilogramm,
		[EnumMember(Value = "Грамм")]
		Gramm
	}
}
