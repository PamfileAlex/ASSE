using ASSE.Core.Models;

namespace ASSE.DomainModel.Models;
public readonly record struct UserRole(int UserId, int RoleId) : IRelationEntity;
