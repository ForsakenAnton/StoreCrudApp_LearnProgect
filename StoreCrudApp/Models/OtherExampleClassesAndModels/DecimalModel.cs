using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace StoreCrudApp.Models.OtherExampleClassesAndModels;

public class DecimalModel
{
    [DisplayFormat(DataFormatString = "{0:C2}"/*, ApplyFormatInEditMode = true)*/)]
    [DataType(DataType.Currency)]
    //[Column(TypeName = "decimal(18, 2)")]
    public decimal Value { get; set; }
}
