using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace expr2.Models;

public partial class ExprContext : DbContext
{
    public ExprContext()
    {
    }

    public ExprContext(DbContextOptions<ExprContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Friend> Friends { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<GroupMember> GroupMembers { get; set; }

    public virtual DbSet<GroupMessage> GroupMessages { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data source=.;Database=expr;Trusted_Connection=True;Encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admins__719FE4E8CBA1D440");

            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.AdminName).HasMaxLength(50);
            entity.Property(e => e.AdminPassword).HasMaxLength(50);
        });

        modelBuilder.Entity<Friend>(entity =>
        {
            entity.HasKey(e => new { e.UserId1, e.UserId2 }).HasName("PK__Friends__2847639553A576C0");

            entity.Property(e => e.UserId1).HasColumnName("UserID1");
            entity.Property(e => e.UserId2).HasColumnName("UserID2");
            entity.Property(e => e.RequestDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.UserId1Navigation).WithMany(p => p.FriendUserId1Navigations)
                .HasForeignKey(d => d.UserId1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Friends__UserID1__76969D2E");

            entity.HasOne(d => d.UserId2Navigation).WithMany(p => p.FriendUserId2Navigations)
                .HasForeignKey(d => d.UserId2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Friends__UserID2__778AC167");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__Groups__149AF30AF3EC3F9B");

            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.GroupName).HasMaxLength(50);
        });

        modelBuilder.Entity<GroupMember>(entity =>
        {
            entity.HasKey(e => new { e.GroupId, e.UserId }).HasName("PK__GroupMem__C5E27FC0784B1D75");

            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.JoinedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Group).WithMany(p => p.GroupMembers)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupMemb__Group__02FC7413");

            entity.HasOne(d => d.User).WithMany(p => p.GroupMembers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupMemb__UserI__03F0984C");
        });

        modelBuilder.Entity<GroupMessage>(entity =>
        {
            entity.HasKey(e => e.GroupMessageId).HasName("PK__GroupMes__F3E76344A0BC662D");

            entity.Property(e => e.GroupMessageId).HasColumnName("GroupMessageID");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.SenderId).HasColumnName("SenderID");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Group).WithMany(p => p.GroupMessages)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK__GroupMess__Group__07C12930");

            entity.HasOne(d => d.Sender).WithMany(p => p.GroupMessages)
                .HasForeignKey(d => d.SenderId)
                .HasConstraintName("FK__GroupMess__Sende__08B54D69");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Messages__C87C037CC1901331");

            entity.Property(e => e.MessageId).HasColumnName("MessageID");
            entity.Property(e => e.ReceiverId).HasColumnName("ReceiverID");
            entity.Property(e => e.SenderId).HasColumnName("SenderID");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Receiver).WithMany(p => p.MessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .HasConstraintName("FK__Messages__Receiv__7C4F7684");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.SenderId)
                .HasConstraintName("FK__Messages__Sender__7B5B524B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACD63A9749");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.IsApproved).HasDefaultValue(false);
            entity.Property(e => e.IsBanned).HasDefaultValue(false);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
