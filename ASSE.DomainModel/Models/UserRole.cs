// --------------------------------------------------------------------------------------
// <copyright file="UserRole.cs" company="Transilvania University of Brasov">
// Student: Pamfile Alex
// Course: Arhitectura sistemelor software enterprise. Platforma .NET
// University: Universitatea Transilvania din Brasov
// </copyright>
// --------------------------------------------------------------------------------------

using ASSE.Core.Models;

namespace ASSE.DomainModel.Models;
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
#pragma warning disable SA1009 // Closing parenthesis should be spaced correctly

/// <summary>
/// Represents the relation between <see cref="User"/> and <see cref="Role"/>.
/// </summary>
public readonly record struct UserRole(int UserId, int RoleId) : IRelationEntity;
#pragma warning restore SA1009 // Closing parenthesis should be spaced correctly
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
