using System.ComponentModel.DataAnnotations.Schema;
using SqlQueryBuilder.Abstractions;

namespace SqlQueryBuilder.Test.Common.Models.Drop;

[Table("DropTestTableName")]
public class DropModelEntityWithTableAttribute : ISqlEntity;