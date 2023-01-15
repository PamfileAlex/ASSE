using ASSE.Core.Models;

namespace ASSE.DomainModel.Models;
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public readonly record struct UserRole(int UserId, int RoleId) : IRelationEntity;
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
