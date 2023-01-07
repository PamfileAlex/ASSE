using ASSE.DataMapper.Interfaces;

namespace ASSE.Service.Implementations;
public abstract class GenericService<D>
	where D : IDataAccess
{
	protected readonly D _dataAccess;

	protected GenericService(D dataAccess)
	{
		_dataAccess = dataAccess;
	}
}
