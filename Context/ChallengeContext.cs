using Microsoft.EntityFrameworkCore;
using REVOPS.DevChallenge.Context.Entities;
using System.Collections.Generic;

namespace REVOPS.DevChallenge.Context;

public class ChallengeContext : DbContext
{
    public DbSet<ChatInfo> ChatInfos { get; private set; }

    public ChallengeContext(DbContextOptions<ChallengeContext> options) : base(options) { }
}
