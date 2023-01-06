using System.Data;
using ASSE.Core.Services;

namespace ASSE.DataMapper.Services;
public interface IDbConnectionProvider : IService
{
	IDbConnection GetNewConnection();
}
